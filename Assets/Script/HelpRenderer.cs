using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LAP
{
    public class HelpRenderer : MonoBehaviour {
        public GameObject NormalText;
        public GameObject HighlightText;

        public void Awake()
        {
            if (Level.Main.HelpHightlight)
            {
                HighlightText.SetActive(true);
                NormalText.SetActive(false);
            }
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Asd()
        {
            NormalText.SetActive(true);
            HighlightText.SetActive(false);
        }
    }
}