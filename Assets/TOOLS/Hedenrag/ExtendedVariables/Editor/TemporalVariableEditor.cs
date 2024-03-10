using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Hedenrag.ExVar;

namespace Hedenrag
{
    namespace myEditor
    {
        [CustomPropertyDrawer(typeof(TemporalVariable<>))]
        public class TemporalVariableEditor : PropertyDrawer
        {
            public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
            {
                var valueProperty = property.FindPropertyRelative("value");
                var timeProperty = property.FindPropertyRelative("time");

                EditorGUIUtility.labelWidth = 10f;
                EditorGUIUtility.fieldWidth = 25f;
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(label, EditorStyles.boldLabel);
                EditorGUIUtility.labelWidth = 50f;
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PropertyField(valueProperty, new("variable"), true);
                EditorGUILayout.PropertyField(timeProperty, new("time"), true);
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.EndHorizontal();
            }
        }
    }
}
