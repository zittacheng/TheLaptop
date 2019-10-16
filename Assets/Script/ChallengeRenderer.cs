using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LAP
{
    public class ChallengeRenderer : MonoBehaviour {
        public GameObject AchievedBase;
        public GameObject FailedBase;

        public void Awake()
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
            if (MainCharacterControl.Main.EditCount <= Level.Main.ChallengeCount)
            {
                AchievedBase.SetActive(true);
                FailedBase.SetActive(false);
            }
            else
            {
                FailedBase.SetActive(true);
                AchievedBase.SetActive(false);
            }
        }
    }
}