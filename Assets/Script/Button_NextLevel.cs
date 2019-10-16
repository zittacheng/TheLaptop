using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LAP
{
    public class Button_NextLevel : Button {

        public override void Effect()
        {
            ThatControl.Main.NextLevel();
        }
    }
}