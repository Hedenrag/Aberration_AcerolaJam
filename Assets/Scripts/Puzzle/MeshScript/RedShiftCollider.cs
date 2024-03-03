using LibCSG;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RedShiftCollider : MonoBehaviour
{
    [SerializeField] GameObject BooleanPrefab;
    public CSGBrush brush;

    Dictionary<GameObject, BooleanOperator> booleanOperators = new();

    private void Awake()
    {
        brush = new(gameObject);
        brush.build_from_mesh(gameObject.GetComponent<MeshFilter>().mesh);
    }

    private void OnCollisionEnter(Collision collision)
    {
        BlueShiftCollider blueShiftCollider = collision.gameObject.GetComponent<BlueShiftCollider>();
        BooleanOperator op = new(brush, blueShiftCollider.brush, Instantiate(BooleanPrefab, transform.parent), transform);
        blueShiftCollider.booleanOperators.Add(op);
        booleanOperators.Add(collision.gameObject, op);
    }
    private void OnCollisionExit(Collision collision)
    {
        booleanOperators[collision.gameObject].Destroy();
        collision.gameObject.GetComponent<BlueShiftCollider>().booleanOperators.Remove(booleanOperators[collision.gameObject]);
        booleanOperators.Remove(collision.gameObject);
    }

    private void Update()
    {
        if (transform.hasChanged)
        {
            transform.hasChanged = false;

            foreach (var value in booleanOperators.Values)
            {
                value.PerformBool();
            }
        }
    }

    private void LateUpdate()
    {
        foreach (var value in booleanOperators.Values)
        {
            value.updated = false;
        }
    }

    public class BooleanOperator
    {
        public bool updated = false;

        GameObject gameObject;
        Transform target;

        CSGBrush brushA;
        CSGBrush brushB;

        CSGBrush result;

        Mesh mesh;

        CSGBrushOperation operation;

        MeshCollider collider;
        MeshFilter filter;

        public BooleanOperator(CSGBrush brush_a, CSGBrush brush_b, GameObject g, Transform target)
        {
            gameObject = g;
            brushA = brush_a;
            brushB = brush_b;

            operation = new();

            result = new(g);

            mesh = new Mesh();

            collider = g.GetComponent<MeshCollider>();
            filter = g.GetComponent<MeshFilter>();
            this.target = target;
        }

        public void PerformBool()
        {
            if (updated) return;
            updated = true;

            brushA.UpdateMatrix();
            brushB.UpdateMatrix();
            result.UpdateMatrix();

            operation.merge_brushes(Operation.OPERATION_INTERSECTION, brushA, brushB, ref result);

            mesh = result.getMesh();
            mesh.name = "bool result";
            filter.sharedMesh = mesh;
            if(mesh.vertexCount > 0)
            {
                collider.sharedMesh = mesh;
            }

            gameObject.transform.SetPositionAndRotation(target.position, target.rotation);
        }

        public void Destroy()
        {
            GameObject.Destroy(gameObject);
        }

    }
}