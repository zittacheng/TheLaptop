using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LAP
{
    public class BackButton : Button {

        public override void Effect()
        {
            base.Effect();
            UIControl.Main.Back();
        }
    }
}