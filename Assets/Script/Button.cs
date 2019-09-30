using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LAP
{
    public class Button : MonoBehaviour {
        public bool Active;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0) && Active)
                Effect();
        }

        public virtual void Effect()
        {

        }
    }
}