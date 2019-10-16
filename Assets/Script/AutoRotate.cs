using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LAP
{
    public class AutoRotate : MonoBehaviour {
        public Transform Base;
        public Vector3 Speed;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void FixedUpdate()
        {
            Base.transform.Rotate(Speed * Time.fixedDeltaTime);
        }
    }
}