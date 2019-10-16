using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LAP
{
    public class Button_Retry : Button {

        public override void Effect()
        {
            ThatControl.Main.Retry();
        }
    }
}