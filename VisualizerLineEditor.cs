using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(VisualizerLine))]
[CanEditMultipleObjects]
public class VisualizerLineEditor : Editor
{
        public override void OnInspectorGUI()
        {
        VisualizerLine t = (target as VisualizerLine);
        DrawDefaultInspector();

            if( GUILayout.Button("Go To Next Target"))
            {

                t.GoToNextTarget();
            }
        }
}
