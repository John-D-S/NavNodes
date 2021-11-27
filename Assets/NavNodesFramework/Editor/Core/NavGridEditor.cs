using NavNodesFramework.Core;

using System;
using System.Collections;
using System.Collections.Generic;

using UnityEditor;
using UnityEditor.AnimatedValues;

using UnityEngine;

namespace NavNodesFramework.Editor.Core
{    
    [CustomEditor(typeof(NavGrid))]
    public class NavGridEditor : UnityEditor.Editor
    {
        private NavGrid navGrid;

        private SerializedProperty maxNodesProperty;
        private SerializedProperty allowedNodeLayersProperty;
        private SerializedProperty pathBlockingLayersProperty;
        private SerializedProperty nodeCheckHeightProperty;
        private SerializedProperty nodeCheckRayDistanceProperty;
        private SerializedProperty gridSizeProperty;
        private SerializedProperty nodePrefabProperty;
        private SerializedProperty showNodesInEditModeProperty;

        private AnimBool showWarning = new AnimBool();
        
        private void OnEnable()
        {
            navGrid = target as NavGrid;

            maxNodesProperty = serializedObject.FindProperty("maxNodes");
            allowedNodeLayersProperty = serializedObject.FindProperty("allowedNodeLayers");
            pathBlockingLayersProperty = serializedObject.FindProperty("pathBlockingLayers");
            nodeCheckHeightProperty = serializedObject.FindProperty("nodeCheckHeight");
            nodeCheckRayDistanceProperty = serializedObject.FindProperty("nodeCheckRayDistance");
            gridSizeProperty = serializedObject.FindProperty("gridSize");
            nodePrefabProperty = serializedObject.FindProperty("nodePrefab");
            showNodesInEditModeProperty = serializedObject.FindProperty("showNodesInEditMode");

            showWarning.value = showNodesInEditModeProperty.boolValue;
            showWarning.valueChanged.AddListener(Repaint);
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.BeginVertical(GUI.skin.box);
            {
                EditorGUILayout.PropertyField(allowedNodeLayersProperty);
                EditorGUILayout.PropertyField(pathBlockingLayersProperty);
                EditorGUILayout.Space();

                EditorGUILayout.PropertyField(nodeCheckHeightProperty);
                EditorGUILayout.PropertyField(nodeCheckRayDistanceProperty);
                EditorGUILayout.Space();

                EditorGUILayout.PropertyField(maxNodesProperty);
                EditorGUILayout.PropertyField(gridSizeProperty);
                EditorGUILayout.PropertyField(nodePrefabProperty);
                EditorGUILayout.PropertyField(showNodesInEditModeProperty);

                showWarning.target = showNodesInEditModeProperty.boolValue;
                if(EditorGUILayout.BeginFadeGroup(showWarning.faded))
                {
                    EditorGUI.indentLevel++;
                    {
                        EditorGUILayout.LabelField("Warning: Turn Show Nodes In Edit Mode off before entering play mode.");
                    }
                    EditorGUI.indentLevel--;
                }
                EditorGUILayout.EndFadeGroup();
                
            }
            EditorGUILayout.EndVertical();

            serializedObject.ApplyModifiedProperties();
        }
    }
}
