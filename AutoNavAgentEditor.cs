using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(AutoNavAgent))]
[CanEditMultipleObjects]
public class AutoNavAgentEditor : Editor
{
        public override void OnInspectorGUI()
        {
            AutoNavAgent t = (target as AutoNavAgent);
            DrawDefaultInspector();

            if( GUILayout.Button("JumpToNextTarget"))
            {

                t.Go();
            }
        }
}
