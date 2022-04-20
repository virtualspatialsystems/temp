using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CreateMultipleLines))]
public class CreateMultipleLinesEditor : Editor
{
    public override void OnInspectorGUI()
    {
        CreateMultipleLines _createMultipleLines = (CreateMultipleLines)target;

        DrawDefaultInspector();

        if (GUILayout.Button("Convert"))
        {
            _createMultipleLines.Convert();
        }
    }
}
