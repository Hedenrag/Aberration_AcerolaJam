using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPositions : MonoBehaviour
{
    [SerializeField] Transform NormalPos;
    [SerializeField] Transform HoverPos;
    [SerializeField] Transform PressPos;

    private void OnEnable()
    {
        transform.position = NormalPos.position;
    }

    public void GoToHoverPosition()
    {
        transform.DOMove(HoverPos.position, 0.15f).SetEase(Ease.InCubic);
    }
    public void GoToNormalPosition()
    {
        transform.DOMove(NormalPos.position, 0.15f).SetEase(Ease.InCubic);
        transform.position = NormalPos.position;
    }
    public void GoToPressPosition()
    {
        transform.DOMove(PressPos.position, 0.1f).SetEase(Ease.InCubic);
    }
}
