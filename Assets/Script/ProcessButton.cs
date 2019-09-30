using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LAP
{
    public class ProcessButton : Button {

        public override void Effect()
        {
            base.Effect();
            App A = CombatControl.Main.CurrentApp;
            if (!A || A.Processes.Count <= 0 || A.Processes[0].Processing || A.Processes[0].AlreadyFinished || A.Processes[0].RequiredProcess)
                return;
            CombatControl.Main.StartProcess();
        }
    }
}