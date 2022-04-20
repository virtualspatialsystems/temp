using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GeometryFromLine)) , CanEditMultipleObjects ]
public class GeometryFromLineEditor : Editor
{
    public override void OnInspectorGUI()
    {
        GeometryFromLine _creator = (GeometryFromLine)target;

        DrawDefaultInspector();

        if (GUILayout.Button("Create Geometry from Line"))
        {
            _creator.ConvertToMesh();
        }
    }
}
