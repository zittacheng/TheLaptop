using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LAP
{
    public class MovementControl : MonoBehaviour {
        public bool Active;
        [Space]
        public Rigidbody Rig;
        public float MovementSpeed;
        public float JumpSpeed;
        [Space]
        public GameObject HorizontalPivot;
        public GameObject VerticalPivot;
        public float RotationSpeed;
        public Vector2 VerticalAngleLimit;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (!Active)
            {
                Rig.velocity = new Vector3(0, 0, 0);
                return;
            }

            float y = Rig.velocity.y;
            Rig.velocity = HorizontalPivot.transform.TransformDirection(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            Rig.velocity.Normalize();
            Rig.velocity *= MovementSpeed;
            if (Input.GetKeyDown(KeyCode.Space))
                Rig.velocity = new Vector3(Rig.velocity.x, JumpSpeed, Rig.velocity.z);
            else
                Rig.velocity = new Vector3(Rig.velocity.x, y, Rig.velocity.z);
        }

        public void FixedUpdate()
        {
            HorizontalPivot.transform.eulerAngles = new Vector3(0, HorizontalPivot.transform.eulerAngles.y, 0);
            if (!Active)
                return;

            float x = -Input.GetAxis("Mouse Y") * RotationSpeed * Time.fixedDeltaTime;
            float y = Input.GetAxis("Mouse X") * RotationSpeed * Time.fixedDeltaTime;
            HorizontalPivot.transform.eulerAngles = new Vector3(0, HorizontalPivot.transform.eulerAngles.y + y, 0);
            VerticalPivot.transform.localEulerAngles = new Vector3(VerticalPivot.transform.localEulerAngles.x + x, 0, 0);
            if (AbsAngle(VerticalPivot.transform.localEulerAngles.x) > VerticalAngleLimit.y)
            {
                VerticalPivot.transform.localEulerAngles = 
                    new Vector3(VerticalAngleLimit.y, VerticalPivot.transform.localEulerAngles.y, VerticalPivot.transform.localEulerAngles.z);
            }
            else if (AbsAngle(VerticalPivot.transform.localEulerAngles.x) < VerticalAngleLimit.x)
            {
                VerticalPivot.transform.localEulerAngles = 
                    new Vector3(VerticalAngleLimit.x, VerticalPivot.transform.localEulerAngles.y, VerticalPivot.transform.localEulerAngles.z);
            }
        }

        public void LaptopOn()
        {
            StartCoroutine("LaptopOnIE");
        }

        public IEnumerator LaptopOnIE()
        {
            Active = false;
            yield return 0;
            /*float OriX = AbsAngle(VerticalPivot.transform.localEulerAngles.x);
            float x = OriX;
            float A = MainCharacterControl.Main.AnimValue;
            while (A < 1)
            {
                x = OriX * (1 - A);
                yield return 0;
                VerticalPivot.transform.localEulerAngles = new Vector3(x, 0, 0);
                A = MainCharacterControl.Main.AnimValue;
            }
            VerticalPivot.transform.localEulerAngles = new Vector3(0, 0, 0);*/
        }

        public void LaptopOff()
        {
            StartCoroutine("LaptopOffIE");
        }

        public IEnumerator LaptopOffIE()
        {
            yield return 0;
            Active = true;
        }

        public static float AbsAngle(float Value)
        {
            if (Value > 180)
                return Value - 360;
            else if (Value <= -180)
                return Value + 360;
            else
                return Value;
        }
    }
}