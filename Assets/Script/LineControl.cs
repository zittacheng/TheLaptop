using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LAP
{
    public class LineControl : MonoBehaviour {
        public LineRenderer LR;
        public GameObject Point0;
        public GameObject Point1;

        private void Awake()
        {
            EditorProcess();
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            Process();
        }

        public void FixedUpdate()
        {
            Process();
        }

        public void LateUpdate()
        {
            Process();
        }

        public void Process()
        {
            LR.SetPosition(0, Point0.transform.position);
            LR.SetPosition(1, Point1.transform.position);
        }

        public void EditorProcess()
        {
            Vector3[] P = new Vector3[2];
            P[0] = Point0.transform.position;
            P[1] = Point1.transform.position;
            LR.SetPositions(P);
        }
    }
}