using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MoveCameraByDirector))]
public class MoveCameraByDirectorEditor : Editor
{

    private MoveCameraByDirector mcbd
    {
        get
        {
            return target as MoveCameraByDirector;
        }
    }


    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();


        GUILayout.Space(20);
        GUILayout.Label("Springe von POI zu POI");
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("<<"))
        {
            mcbd.JumpToPrevPOI();
        }
        if (GUILayout.Button(">>"))
        {
            mcbd.JumpToNextPOI();
        }

        GUILayout.EndHorizontal();
        GUILayout.Space(20);

        if (GUILayout.Button("Start Transition"))
        {
            mcbd.TransitionToPOI();
        }

    }

}
