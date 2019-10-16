using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LAP
{
    public class Button_Open : Button {

        public override void Effect()
        {
            ThatControl.Main.Open();
        }
    }
}