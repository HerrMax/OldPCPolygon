using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Debug1))]
public class DebugEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Debug1 debug = (Debug1)target;
        if (GUILayout.Button("Test"))
        {
            debug.Cake();
        }
    }
}
