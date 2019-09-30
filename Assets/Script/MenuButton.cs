using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LAP
{
    public class MenuButton : Button {
        public App app;

        public override void Effect()
        {
            base.Effect();
            CombatControl.Main.CurrentApp = app;
            UIControl.Main.AppMode = true;
        }
    }
}