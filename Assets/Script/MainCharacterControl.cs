using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LAP
{
    public class MainCharacterControl : MonoBehaviour {
        [HideInInspector]
        public static MainCharacterControl Main;
        public MovementControl MovControl;
        public LaptopControl LapControl;
        public Rigidbody Rig;
        public Animator Anim;
        [Space]
        public GameObject RotationBase;
        public GameObject RotationObject;
        public CameraPoint MovementPoint;
        public CameraPoint LaptopPoint;
        [Space]
        public bool LaptopActive;
        [Space]
        public float AnimValue;
        public bool Animating;

        public void Awake()
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
            RotationUpdate();

            if (!Animating && Input.GetKeyDown(KeyCode.Q))
                LaptopMode(!LaptopActive);
        }

        public void FixedUpdate()
        {
            RotationUpdate();
        }

        public void RotationUpdate()
        {
            RotationBase.transform.eulerAngles = RotationObject.transform.eulerAngles;
        }

        public void LaptopMode(bool On)
        {
            Anim.SetBool("LaptopMode", On);
            LaptopActive = On;
            if (On)
            {
                MovControl.LaptopOn();
                LapControl.LaptopOn();
                CameraControl.Main.SetPoint(LaptopPoint, 0.8f);
            }
            else
            {
                MovControl.LaptopOff();
                LapControl.LaptopOff();
                CameraControl.Main.SetPoint(MovementPoint, 0.8f);
            }
        }
    }
}