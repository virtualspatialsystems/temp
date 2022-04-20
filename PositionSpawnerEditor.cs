using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;



[CustomEditor(typeof(PositionSpawner))]
public class PositionSpawnerEditor : Editor
{
    // OnInspector GUI
    public override void OnInspectorGUI() //2
    {
        PositionSpawner spawnScript = (PositionSpawner)target; //1

        DrawDefaultInspector();

        if (GUILayout.Button("spawn"))
        {
            spawnScript.SpawnObjects();
        }
    }
}
