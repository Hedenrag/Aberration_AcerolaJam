using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Dial : MonoBehaviour
{
    [SerializeField] float maxRotation;
    public UnityEvent<float> OnTurnDial;
    float currentAngle;
    public void Drag(BaseEventData bdata)
    {
        PointerEventData data = (PointerEventData)bdata;
        Vector2 mousePos = data.position;
        Vector2 objectPos = Camera.main.WorldToScreenPoint(transform.position);

        Vector2 vector = mousePos - objectPos;

        float angle = -Vector2.SignedAngle(vector, vector + data.delta);

        if(Mathf.Abs(currentAngle + angle) > maxRotation)
        {
            var temp = Mathf.Clamp(currentAngle + angle, -maxRotation, maxRotation);
            angle = temp - currentAngle;
        }
        currentAngle += angle;
        Debug.Log(angle);
        transform.Rotate(Vector3.forward * angle, Space.Self);
        OnTurnDial.Invoke(angle);
    }
}
