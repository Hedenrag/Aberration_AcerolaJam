using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Mathematics;
using UnityEngine;
using LibCSG;
using UnityEngine.Rendering.Universal;

public class RedShiftCollider : MonoBehaviour
{
    [SerializeField] GameObject BooleanPrefab;
    public CSGBrush brush;

    Dictionary<Collider, BooleanOperator> booleanOperators = new();

    private void OnCollisionEnter(Collision collision)
    {
        var blueBrush = collision.transform.GetComponent<BlueShiftCollider>().brush;
        var meshFilter = Instantiate(BooleanPrefab).GetComponent<MeshFilter>();
        booleanOperators.Add(collision.collider, new BooleanOperator(brush, blueBrush, meshFilter));
    }

    private void OnCollisionExit(Collision collision)
    {
        booleanOperators.Remove(collision.collider);
    }

    void Start()
    {
        brush = new();
        StartCoroutine(DoCalculation());
    }



    IEnumerator DoCalculation()
    {
        while (true)
        {
            foreach (var boolOp in booleanOperators.Values)
            {
                if (boolOp.OperationFinished)
                {
                    boolOp.AssignMesh();
#pragma warning disable CS4014 
                    boolOp.DoCalculations();
#pragma warning restore CS4014
                }
            }
            yield return new WaitForEndOfFrame();
        }
    }






    class BooleanOperator
    {
        public bool OperationFinished => operationFinished;
        bool operationFinished;

        CSGBrush redBox;
        CSGBrush blueBox;

        CSGBrushOperation CSGOp;

        CSGBrush result;

        Mesh mesh;
        MeshFilter meshFilter;

        public BooleanOperator(CSGBrush redBrush, CSGBrush blueBrush, MeshFilter result)
        {
            redBox = redBrush;
            blueBox = blueBrush;
            operationFinished = false;
            CSGOp = new();
            this.result = new CSGBrush();
            mesh = new Mesh();
            DoCalculations();
            meshFilter = result;
        }

        public void AssignMesh()
        {
            meshFilter.mesh = mesh;
        }

        public async Task<Mesh> DoCalculations()
        {
            var task = await Task.Run(CSGOperation);
            operationFinished = true;
            return task;
        }

        Mesh CSGOperation()
        {
            CSGOp.merge_brushes(Operation.OPERATION_INTERSECTION, redBox, blueBox, ref result, 0.005f);
            return result.getMesh(mesh);
        }
    }

}


