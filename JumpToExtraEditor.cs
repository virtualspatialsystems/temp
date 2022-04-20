using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(JumpToExtra))]
[CanEditMultipleObjects]
public class JumpToExtraEditor : Editor
{
        public override void OnInspectorGUI()
        {
        JumpToExtra t = (target as JumpToExtra);
            DrawDefaultInspector();

            if( GUILayout.Button("JumpToNextTarget"))
            {

                t.JumpToNextTarget();
            }
        }
}
