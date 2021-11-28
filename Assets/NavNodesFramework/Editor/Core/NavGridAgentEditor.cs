using NavNodesFramework.Core;

using System;
using System.Collections;
using System.Collections.Generic;

using UnityEditor;

using UnityEngine;

namespace NavNodesFramework.Editor.Core
{    
    [CustomEditor(typeof(NavGridAgent))]
    public class NavGridAgentEditor : UnityEditor.Editor
    {
        private NavGridAgent navGridAgent;

        private SerializedProperty navGridProperty;
        private SerializedProperty walkSpeedProperty;
        private SerializedProperty lerpAmountProperty;
        private SerializedProperty turnInDirectionOfTravelProperty;
        private SerializedProperty turnLerpSpeedProperty;

        private void OnEnable()
        {
            navGridAgent = target as NavGridAgent;

            navGridProperty = serializedObject.FindProperty("navGrid");
            walkSpeedProperty = serializedObject.FindProperty("walkSpeed");
            lerpAmountProperty = serializedObject.FindProperty("lerpAmount");
            turnInDirectionOfTravelProperty = serializedObject.FindProperty("turnInDirectionOfTravel");
            turnLerpSpeedProperty = serializedObject.FindProperty("turnLerpSpeed");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.BeginVertical(GUI.skin.box);
            {
                EditorGUILayout.PropertyField(navGridProperty);
                EditorGUILayout.PropertyField(walkSpeedProperty);
                EditorGUILayout.PropertyField(lerpAmountProperty);
                EditorGUILayout.PropertyField(turnInDirectionOfTravelProperty);
                EditorGUILayout.PropertyField(turnLerpSpeedProperty);
            }
            EditorGUILayout.EndVertical();

            serializedObject.ApplyModifiedProperties();
        }
    }
}
