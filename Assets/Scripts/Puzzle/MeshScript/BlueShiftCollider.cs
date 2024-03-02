using LibCSG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueShiftCollider : MonoBehaviour
{
    public CSGBrush brush;
    void Start()
    {
        brush = new();
    }
}
