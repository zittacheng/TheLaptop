using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace LAP
{
    [CustomEditor(typeof(Cube))]
    [CanEditMultipleObjects]
    public class Editor_Cube : Editor {

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            /*Cube C = (Cube)target;
            if (GUILayout.Button("Apply"))
                C.EditorApply();*/
        }
    }
}