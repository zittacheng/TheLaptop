using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LAP
{
    public class Script : MonoBehaviour {
        public string Command;
        public string Value;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public bool Loop()
        {
            return Command == "Loop" || Command == "LoopDelay";
        }

        public bool StartLoop()
        {
            return Command == "StartLoop";
        }
    }
}