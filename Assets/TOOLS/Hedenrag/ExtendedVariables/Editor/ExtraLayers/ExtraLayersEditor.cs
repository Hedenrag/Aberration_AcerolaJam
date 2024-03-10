using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace Hedenrag
{
    namespace ExVar
    {
        [CustomPropertyDrawer(typeof(ExtraLayers))]
        public class ExtraLayersEditor : PropertyDrawer
        {
            public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
            {
                var layerProperty = property.FindPropertyRelative("_layer");
                var valueProperty = property.FindPropertyRelative("_value");

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PropertyField(layerProperty);
                if (layerProperty.objectReferenceValue != null)
                {
                    string[] strings = ((ExLayer)layerProperty.objectReferenceValue).LayerNames;

                    for (int i = 0; i < strings.Length; i++)
                    {
                        if (string.IsNullOrWhiteSpace(strings[i])) { strings[i] = null; }
                    }

                    valueProperty.intValue = EditorGUILayout.MaskField(valueProperty.intValue, ((ExLayer)layerProperty.objectReferenceValue).LayerNames);
                }
                EditorGUILayout.EndHorizontal();
            }
        }
    }
}