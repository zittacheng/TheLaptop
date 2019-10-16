using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LAP
{
    public class Level : MonoBehaviour {
        [HideInInspector]
        public static Level Main;
        public string Key;
        public string NextLevelKey;
        public GameObject Base;
        [Space]
        public GameObject OverrideSpawn;
        public int ChallengeCount;
        [TextArea]
        public string OverrideHelp;
        public bool HelpHightlight;

        private void Awake()
        {
            if (SaveControl.GetString("Level") == Key)
            {
                Main = this;
                Base.SetActive(true);
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
    }
}