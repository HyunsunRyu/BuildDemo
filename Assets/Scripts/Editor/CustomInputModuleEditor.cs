using System.Collections;
using System.Collections.Generic;
using UnityEditor;

namespace UnityEngine.EventSystems
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(CustomInputModule))]
    public class CustomInputModuleEditor : Editor
    {
        SerializedProperty maxTouchCount;

        private void OnEnable()
        {
            maxTouchCount = serializedObject.FindProperty("maxTouchCount");
        }

        public override void OnInspectorGUI()
        {
            int value = maxTouchCount.intValue;
            EditorGUILayout.PropertyField(maxTouchCount);
            if (value != maxTouchCount.intValue)
            {
                serializedObject.ApplyModifiedProperties();
            }
        }
    }
}