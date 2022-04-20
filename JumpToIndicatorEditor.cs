using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(JumpToIndicator))]
[CanEditMultipleObjects]
public class JumpToIndicatorEditor : Editor
{
        public override void OnInspectorGUI()
        {
            JumpToIndicator t = (target as JumpToIndicator);
            DrawDefaultInspector();

            if( GUILayout.Button("JumpToNextTarget"))
            {

                t.JumpToNextTarget();
            }
        }
}
