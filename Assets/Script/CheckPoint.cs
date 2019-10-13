using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LAP
{
    public class CheckPoint : MonoBehaviour {
        public GameObject ResetPoint;
        public string Key;

        public void Awake()
        {
            if (SaveControl.GetString("CheckPoint") == Key)
                MainCharacterControl.Main.SetPosition(ResetPoint.transform.position);
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SetActive()
        {
            SaveControl.SetString("CheckPoint", Key);
        }
    }
}