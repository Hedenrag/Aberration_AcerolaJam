using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Hedenrag
{
    namespace ExVar
    {
        [CustomPropertyDrawer(typeof(ExtraLayer))]
        public class ExtraLayerEditor : PropertyDrawer
        {
            public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
            {
                var layerProperty = property.FindPropertyRelative("_layer");
                var valueProperty = property.FindPropertyRelative("_value");

                int layerIndex = valueProperty.intValue;
                for (int i = 0; i < 32; i++) //TODO bad layer identification
                {
                    if(layerIndex == 1 << i)
                    {
                        layerIndex = 1<<i;
                        break;
                    }
                }
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PropertyField(layerProperty);
                if (layerProperty.objectReferenceValue != null)
                {
                    string[] strings = ((ExLayer)layerProperty.objectReferenceValue).LayerNames;

                    for (int i = 0; i < strings.Length; i++)
                    {
                        if (string.IsNullOrWhiteSpace(strings[i])) { strings[i] = null; }
                    }

                    EditorGUI.BeginChangeCheck();
                    layerIndex = EditorGUILayout.Popup(layerIndex, ((ExLayer)layerProperty.objectReferenceValue).LayerNames);
                    if (EditorGUI.EndChangeCheck())
                    {
                        Debug.Log(layerIndex + " " + ((1<<layerIndex)-1));
                        valueProperty.intValue = ((1 << layerIndex) - 1);
                    }
                    
                }
                EditorGUILayout.EndHorizontal();
            }
        }
    }
}