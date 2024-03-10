using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "LayerType", menuName = "Scriptable Objects/Extra variables/New Layer Type")]
public class ExLayer : ScriptableObject
{
#if UNITY_EDITOR
    [SerializeField, HideInInspector] string[] layerNames;
    public string[] LayerNames => layerNames;
#endif
    [SerializeField, HideInInspector] int layerValues;
    public int LayerValues => layerValues;
}
