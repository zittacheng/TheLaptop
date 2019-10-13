using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LAP
{
    public class AxisRenderer : MonoBehaviour {
        public GameObject X;
        public GameObject Y;
        public GameObject Z;
        public GameObject V;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            Render();
        }

        public void FixedUpdate()
        {
            Render();
        }

        public void Render()
        {
            X.transform.forward = Vector3.right;
            Y.transform.forward = Vector3.up;
            Z.transform.forward = Vector3.forward;
            if (VictoryCube.Main)
                V.transform.forward = VictoryCube.Main.transform.position - V.transform.position;
        }
    }
}