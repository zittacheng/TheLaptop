using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LAP
{
    public class Button_ObjectChange : Button {
        public GameObject Activate;
        public GameObject Deactivate;
        public HelpRenderer HR;

        public override void Effect()
        {
            if (HR)
                HR.Asd();
            Activate.SetActive(true);
            Deactivate.SetActive(false);
        }
    }
}