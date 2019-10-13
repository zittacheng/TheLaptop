using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LAP
{
    public class VictoryCube : MonoBehaviour {
        [HideInInspector]
        public static VictoryCube Main;

        private void Awake()
        {
            Main = this;
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}