using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hedenrag.ExVar;

public class ScriptableObjectInstanceCreateCam : SingletonScriptableObject<ScriptableObjectInstanceCreateCam>, ICallOnAll
{
    [SerializeField] GameObject CameraPrefab;

    public GameObject CreateCam(Transform parent = null)
    {
        return Instantiate(CameraPrefab, parent);
    }
}
