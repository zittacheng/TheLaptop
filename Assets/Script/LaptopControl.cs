using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LAP
{
    public class LaptopControl : MonoBehaviour {
        public bool Active;
        [Space]
        public GameObject CameraPivot;
        public Vector2 OriAngle;
        public Vector2 HorizontalCursorLimit;
        public Vector2 HorizontalAngleLimit;
        public Vector2 VerticalCursorLimit;
        public Vector2 VerticalAngleLimit;

        private void Awake()
        {
            OriAngle = new Vector2(CameraPivot.transform.localEulerAngles.x, CameraPivot.transform.localEulerAngles.y);
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (!Active)
                return;
            RotationUpdate();
        }

        public void FixedUpdate()
        {
            if (!Active)
                return;
            RotationUpdate();
        }

        public void RotationUpdate()
        {
            if (!Cursor.Main)
                return;
            float y = OriAngle.y;
            float yScale = (Cursor.Main.transform.position.x - HorizontalCursorLimit.x) / (HorizontalCursorLimit.y - HorizontalCursorLimit.x);
            y += HorizontalAngleLimit.x + (HorizontalAngleLimit.y - HorizontalAngleLimit.x) * yScale;
            float x = OriAngle.x;
            float xScale = (Cursor.Main.transform.position.y - VerticalCursorLimit.x) / (VerticalCursorLimit.y - VerticalCursorLimit.x);
            x -= VerticalAngleLimit.x + (VerticalAngleLimit.y - VerticalAngleLimit.x) * xScale;
            CameraPivot.transform.localEulerAngles = new Vector3(x, y, 0);
        }

        public void LaptopOn()
        {
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
            StartCoroutine("LaptopOnIE");
        }

        public IEnumerator LaptopOnIE()
        {
            yield return 0;
            UnityEngine.Cursor.lockState = CursorLockMode.None;
            Active = true;
        }

        public void LaptopOff()
        {
            StartCoroutine("LaptopOffIE");
        }

        public IEnumerator LaptopOffIE()
        {
            Active = false;
            yield return 0;
            /*float OriX = AbsAngle(CameraPivot.transform.localEulerAngles.x);
            float OriY = AbsAngle(CameraPivot.transform.localEulerAngles.y);
            float x = OriX;
            float y = OriY;
            float A = MainCharacterControl.Main.AnimValue;
            while (A > 0)
            {
                x = OriX * A;
                y = OriY * A;
                print(x + " , " + y);
                yield return 0;
                CameraPivot.transform.localEulerAngles = new Vector3(x, y, 0);
                A = MainCharacterControl.Main.AnimValue;
            }
            CameraPivot.transform.localEulerAngles = new Vector3(0, 0, 0);
            yield return 0;*/
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