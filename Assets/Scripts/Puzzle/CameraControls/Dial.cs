using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Dial : MonoBehaviour
{
    public UnityEvent<float> OnTurnDial;

    public void Drag(BaseEventData bdata)
    {
        PointerEventData data = (PointerEventData)bdata;
        Vector2 mousePos = data.position;
        Vector2 objectPos = Camera.main.WorldToScreenPoint(transform.position);

        Vector2 vector = mousePos - objectPos;

        float angle = -Vector2.SignedAngle(vector, vector + data.delta);

        Debug.Log(angle);
        transform.Rotate(Vector3.up * angle, Space.Self);
        OnTurnDial.Invoke(angle);
    }
}
