using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Rotator))]
public class RotatorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Rotator _Rotator = (Rotator)target;

        DrawDefaultInspector();

        if (GUILayout.Button("Create"))
        {
            _Rotator.Create();
        }
    }
}
