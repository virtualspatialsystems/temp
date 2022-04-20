using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(AutoAgentFollower))]
[CanEditMultipleObjects]
public class AutoAgentFollowerEditor : Editor
{
        public override void OnInspectorGUI()
        {
        AutoAgentFollower t = (target as AutoAgentFollower);
            DrawDefaultInspector();

            if( GUILayout.Button("JumpToNextTarget"))
            {

                t.Go();
            }
        }
}
