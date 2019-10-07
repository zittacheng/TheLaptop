using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LAP
{
    public class Button_AddScript : Button {

        public override void Effect()
        {
            if (MainCharacterControl.Main.SelectingCube)
                MainCharacterControl.Main.SelectingCube.AddScript();
        }
    }
}