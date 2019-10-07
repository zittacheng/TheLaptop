using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace LAP
{
    public class ScriptRenderer : MonoBehaviour {
        public int Index;
        public GameObject Mark;
        public GameObject Base;
        public TMP_InputField CommandField;
        public TMP_InputField ValueField;
        public TextMeshProUGUI CommandPlaceholder;
        public TextMeshProUGUI ValuePlaceholder;
        public Button AddButton;
        [Space]
        [HideInInspector]
        public Script CurrentScript;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Base.activeInHierarchy)
                Mark.SetActive(true);
            else
                Mark.SetActive(false);

            if (!MainCharacterControl.Main.LaptopActive)
            {
                CommandField.DeactivateInputField();
                ValueField.DeactivateInputField();
            }


            Cube C = MainCharacterControl.Main.SelectingCube;
            if (!C)
                return;

            if (C.Scripts.Count <= Index || !C.Scripts[Index])
            {
                if (Index <= 0 || (C.Scripts.Count > Index - 1 && C.Scripts[Index - 1]))
                    AddButton.gameObject.SetActive(true);
                else
                    AddButton.gameObject.SetActive(false);
                Base.SetActive(false);
                CurrentScript = null;
                return;
            }
            else
            {
                Base.SetActive(true);
                AddButton.gameObject.SetActive(false);
            }

            Script S = C.Scripts[Index];
            if (S != CurrentScript)
                Render(S);
            CurrentScript = S;

            if (CommandField.text == "")
                CommandPlaceholder.gameObject.SetActive(true);
            else
                CommandPlaceholder.gameObject.SetActive(false);
            if (ValueField.text == "")
                ValuePlaceholder.gameObject.SetActive(true);
            else
                ValuePlaceholder.gameObject.SetActive(false);
        }

        public void Render(Script S)
        {
            CommandField.text = S.Command;
            ValueField.text = S.Value;
        }

        public void ChangeCommand()
        {
            if (!CurrentScript)
                return;
            CurrentScript.Command = CommandField.text;
            Render(CurrentScript);
        }

        public void ChangeValue()
        {
            if (!CurrentScript)
                return;
            CurrentScript.Value = ValueField.text;
            Render(CurrentScript);
        }
    }
}