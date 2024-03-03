using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PuzzleItem : MonoBehaviour
{

    [SerializeField] Transform RedObject;
    [SerializeField] Transform BlueObject;

    [SerializeField] float shiftMultiplier = 1f;

    [SerializeField] Vector3 maxShift = Vector3.one * 5f;

    void OnEnable()
    {
        GenerateMesh.puzzleItems.Add(this);
    }

    Vector3 currentShift;

    public void VerticalShift(Transform shifter, float amount)
    {
        currentShift += shifter.up * amount * shiftMultiplier;
        ApplyShift();
    }
    public void HorizontalShift(Transform shifter, float amount)
    {
        currentShift += shifter.right * amount * shiftMultiplier;
        ApplyShift();
    }

    void ApplyShift()
    {
        Vector3 objectShift = new Vector3(Mathf.Clamp(currentShift.x, 0f, maxShift.x), Mathf.Clamp(currentShift.y, 0f, maxShift.y), Mathf.Clamp(currentShift.z, 0f, maxShift.z));
        RedObject.transform.position = transform.position - currentShift;
        BlueObject.transform.position = transform.position + currentShift;
    }

    private void OnDisable()
    {
        GenerateMesh.puzzleItems.Remove(this);
    }
}
