using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace LAP
{
    [CustomEditor(typeof(LineControl))]
    [CanEditMultipleObjects]
    public class Editor_LineControl : Editor {

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            LineControl LC = (LineControl)target;
            if (GUILayout.Button("Apply"))
                LC.EditorProcess();
        }
    }
}