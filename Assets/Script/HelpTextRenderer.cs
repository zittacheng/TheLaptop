using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace LAP
{
    public class HelpTextRenderer : MonoBehaviour {
        public TextMeshProUGUI TEXT;
        [TextArea]
        public string DefaultText;

        private void Awake()
        {
            Update();
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Level.Main.OverrideHelp != "")
                TEXT.text = Level.Main.OverrideHelp;
            else
                TEXT.text = DefaultText;
        }
    }
}