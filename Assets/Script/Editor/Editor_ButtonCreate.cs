using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace LAP
{
    [CustomEditor(typeof(ButtonCreator))]
    [CanEditMultipleObjects]
    public class Editor_ButtonCreate : Editor {

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            ButtonCreator BC = (ButtonCreator)target;
            if (GUILayout.Button("ASD"))
                BC.Process();
        }
    }
}