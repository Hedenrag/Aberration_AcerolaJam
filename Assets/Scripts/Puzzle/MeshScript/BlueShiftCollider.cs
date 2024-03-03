using LibCSG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueShiftCollider : MonoBehaviour
{
    public CSGBrush brush;

    public List<RedShiftCollider.BooleanOperator> booleanOperators = new();

    void Awake()
    {
        brush = new(gameObject);
        brush.build_from_mesh(gameObject.GetComponent<MeshFilter>().mesh);
    }

    private void Update()
    {
        if (transform.hasChanged)
        {
            transform.hasChanged = false;

            foreach (var value in booleanOperators)
            {
                value.PerformBool();
            }
        }
    }

}
