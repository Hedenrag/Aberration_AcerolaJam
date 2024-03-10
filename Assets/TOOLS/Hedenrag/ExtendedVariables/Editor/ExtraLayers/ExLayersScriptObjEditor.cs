using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(ExLayer))]
public class ExLayersScriptObjEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var layerNamesVar = serializedObject.FindProperty("layerNames");
        
        if(layerNamesVar.arraySize != 32) { layerNamesVar.arraySize = 32; }

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.Separator();
        EditorGUILayout.BeginVertical();
        for (int i = 0; i < 32; ++i)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Layer " + i);
            layerNamesVar.GetArrayElementAtIndex(i).stringValue = EditorGUILayout.TextField(layerNamesVar.GetArrayElementAtIndex(i).stringValue);
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndHorizontal();
        layerNamesVar.serializedObject.ApplyModifiedProperties();
        SaveChanges();
    }
}
