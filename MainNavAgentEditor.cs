using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(MainNavAgent))]
[CanEditMultipleObjects]
public class MainNavAgentEditor : Editor
{
        public override void OnInspectorGUI()
        {
        MainNavAgent t = (target as MainNavAgent);
            DrawDefaultInspector();

            if( GUILayout.Button("JumpToNextTarget"))
            {

                t.Go();
            }
        }
}
