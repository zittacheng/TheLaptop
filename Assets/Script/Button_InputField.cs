using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace LAP
{
    public class Button_InputField : Button {
        public TMP_InputField Field;

        public override void Effect()
        {
            Field.ActivateInputField();
        }
    }
}