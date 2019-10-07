using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LAP
{
    public class Button_RemoveScript : Button {
        public ScriptRenderer SR;

        public override void Effect()
        {
            if (MainCharacterControl.Main.SelectingCube)
                MainCharacterControl.Main.SelectingCube.RemoveScript(SR.Index);
        }
    }
}