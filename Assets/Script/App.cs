using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LAP
{
    public class App : MonoBehaviour {
        public string Name;
        public string Description;
        public Sprite Icon;
        public List<Process> Processes;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void StartProcess()
        {
            if (Processes.Count <= 0)
                return;
            Processes[0].StartProcess();
        }

        public void StopProcess()
        {
            if (Processes.Count <= 0)
                return;
            Processes[0].StopProcess();
        }

        public void RemoveProcess(Process P)
        {
            if (Processes.Contains(P))
                Processes.Remove(P);
        }
    }
}