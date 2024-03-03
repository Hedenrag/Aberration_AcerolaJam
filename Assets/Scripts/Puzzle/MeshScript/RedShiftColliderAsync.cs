using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Mathematics;
using UnityEngine;
using LibCSG;
using System;

public class RedShiftColliderAsync : MonoBehaviour
{
    [SerializeField] GameObject BooleanPrefab;
    public CSGBrush brush;

    Dictionary<GameObject, BooleanOperator> booleanOperators = new();
    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("EnterCollision", this);
        var blueBrush = collision.transform.GetComponent<BlueShiftCollider>().brush;
        var meshFilter = Instantiate(BooleanPrefab, transform).GetComponent<MeshFilter>();
        booleanOperators.Add(collision.gameObject, new BooleanOperator(brush, blueBrush, meshFilter));
    }

    private void OnCollisionExit(Collision collision)
    {
        //Debug.Log("ExitCollision", this);
        booleanOperators[collision.gameObject].Destroy();
        booleanOperators[collision.gameObject] = null;
        booleanOperators.Remove(collision.gameObject);
    }

    void Start()
    {
        brush = new(gameObject);
        brush.build_from_mesh(gameObject.GetComponent<MeshFilter>().mesh);
        StartCoroutine(DoCalculation());
    }

    IEnumerator DoCalculation()
    {
        while (true)
        {
            foreach (var boolOp in booleanOperators.Values)
            {
                //Debug.Log($"{boolOp} status is: {boolOp.MyTask.Status}");
                if (boolOp.MyTask.IsCompleted)
                {
                    boolOp.AssignMesh();
                    //Debug.Log("OperationFinished");
                    boolOp.UpdateBrushes();
                    boolOp.Calculate();
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

        MeshFilter meshFilter;

        public Task<CSGBrush.FakeMesh> MyTask => myTask;
        Task<CSGBrush.FakeMesh> myTask;

        public BooleanOperator(CSGBrush redBrush, CSGBrush blueBrush, MeshFilter result)
        {
            redBox = redBrush;
            blueBox = blueBrush;
            operationFinished = true;
            CSGOp = new();
            this.result = new CSGBrush(result.gameObject);
            meshFilter = result;
            myTask = DoCalculations();
        }

        public void AssignMesh()
        {
            meshFilter.mesh = myTask.Result.ToMesh();
            var item = myTask.Result;
            //Debug.Log($"Task result: {item.vertices.Length}");
        }

        public void Destroy()
        {
            GameObject.Destroy(meshFilter.gameObject);
        }

        public void Calculate()
        {
            if(myTask != null) myTask.Dispose();
            myTask = DoCalculations();
        }

        async Task<CSGBrush.FakeMesh> DoCalculations()
        {
            var task = await Task.Run(CSGOperation);
            operationFinished = true;
            return task;
        }

        CSGBrush.FakeMesh CSGOperation()
        {
            CSGOp.merge_brushes(Operation.OPERATION_INTERSECTION, redBox, blueBox, ref result, 0.005f);
            return result.getAsyncMesh();
        }

        internal void UpdateBrushes()
        {
            redBox.UpdateMatrix(); 
            blueBox.UpdateMatrix();
            result.UpdateMatrix();
        }

        ~BooleanOperator() 
        {
            Debug.Log("Destroying");
        }
    }

}


