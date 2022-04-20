using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(ZuschauerContainer))]
public class ZuschauerContainerEditor : Editor
{
    // OnInspector GUI
    public override void OnInspectorGUI() //2
    {
        ZuschauerContainer zuschauerContainer = (ZuschauerContainer)target; //1

        DrawDefaultInspector();

        if (GUILayout.Button("RotateToTarget"))
        {
            zuschauerContainer.SetLookAt();
        }

        if (GUILayout.Button("SetTextures"))
        {
            zuschauerContainer.SetTextures();
        }

        if (GUILayout.Button("SetApplaus"))
        {
            zuschauerContainer.Applaus();
        }

        if (GUILayout.Button("SetCalmMovement"))
        {
            zuschauerContainer.CalmMovement();
        }
    }
}
