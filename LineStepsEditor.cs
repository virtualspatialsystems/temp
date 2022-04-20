using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LineSteps))]
public class LineStepsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        LineSteps _LineSteps = (LineSteps)target;

        DrawDefaultInspector();

        if (GUILayout.Button("Animate"))
        {
            _LineSteps.BeginAnimation();
        }
    }
}
