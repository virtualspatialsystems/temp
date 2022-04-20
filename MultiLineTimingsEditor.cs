using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MutliLineStepTimings))]
public class MutliLineStepTimingsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        MutliLineStepTimings _MutliLineStepTimings = (MutliLineStepTimings)target;

        DrawDefaultInspector();

        if (GUILayout.Button("Animate"))
        {
            _MutliLineStepTimings.Animate();
        }
    }
}
