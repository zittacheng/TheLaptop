using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LAP
{
    public class EditCountRenderer : MonoBehaviour {
        public List<GameObject> Points;
        public List<GameObject> ChallengePoints;

        // Start is called before the first frame update
        void Start()
        {
            for (int i = 0; i < ChallengePoints.Count; i++)
            {
                if (i < Level.Main.ChallengeCount)
                    ChallengePoints[i].SetActive(true);
                else
                    ChallengePoints[i].SetActive(false);
            }
        }

        // Update is called once per frame
        void Update()
        {
            for (int i = 0; i < Points.Count; i++)
            {
                if (i < MainCharacterControl.Main.EditCount)
                    Points[i].SetActive(true);
                else
                    Points[i].SetActive(false);
            }
        }
    }
}