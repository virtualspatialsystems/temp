using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(JumpTo))]
[CanEditMultipleObjects]
public class JumpToEditor : Editor
{
        public override void OnInspectorGUI()
        {
            JumpTo t = (target as JumpTo);
            DrawDefaultInspector();

            if( GUILayout.Button("JumpToNextTarget"))
            {

                t.JumpToNextTarget();
            }
        }
}
