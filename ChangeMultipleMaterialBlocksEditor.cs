using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ChangeMultipleMaterialBlocks)), CanEditMultipleObjects]
public class ChangeMultipleMaterialBlocksEditor : Editor
{
    public override void OnInspectorGUI()
    {
        ChangeMultipleMaterialBlocks _matBlock = (ChangeMultipleMaterialBlocks)target;

        DrawDefaultInspector();

        if (GUILayout.Button("Animate per Segment"))
        {
            _matBlock.Animate();
        }
        
        if (GUILayout.Button("Animate all at once"))
        {
            _matBlock.AnimateAllAtOnce();
        }
    }
}
