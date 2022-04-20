using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[CustomEditor(typeof(SwitchLanguageTextures))]
public class SwitchLanguageTexturesEditor : Editor {

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        
        serializedObject.Update();
    }
}