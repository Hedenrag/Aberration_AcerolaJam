using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Hedenrag.ExVar;

namespace Hedenrag.myEditor
{
    [CustomPropertyDrawer(typeof(ScriptableObjectReferencer<>))]
    public class ScriptableObjectReferencerEditor : PropertyDrawer
    {
        bool toogleOpen = false;
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var valueProperty = property.FindPropertyRelative("_scriptableObject");

            EditorGUI.PropertyField(position, valueProperty, label, true);

            if(valueProperty.boxedValue != null)
            {
                toogleOpen = EditorGUILayout.BeginFoldoutHeaderGroup(toogleOpen, "");
                if (toogleOpen)
                {
                    EditorGUI.indentLevel++;
                    EditorGUI.indentLevel++;
                    //property.exposedReferenceValue;
                    //EditorGUILayout.
                    Editor.CreateEditor((ScriptableObject)valueProperty.boxedValue).OnInspectorGUI();
                    EditorGUI.indentLevel--;
                    EditorGUI.indentLevel--;
                }
                EditorGUILayout.EndFoldoutHeaderGroup();
            }
        }
    }
}

