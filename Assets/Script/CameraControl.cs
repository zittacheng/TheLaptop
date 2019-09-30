using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LAP
{
    public class CameraControl : MonoBehaviour {
        public GameObject CameraPivot;
        public Vector2 OriAngle;
        public Vector2 HorizontalCursorLimit;
        public Vector2 HorizontalAngleLimit;
        public Vector2 VerticalCursorLimit;
        public Vector2 VerticalAngleLimit;

        private void Awake()
        {
            OriAngle = new Vector2(CameraPivot.transform.eulerAngles.x, CameraPivot.transform.eulerAngles.y);
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            RotationUpdate();
        }

        public void FixedUpdate()
        {
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
            CameraPivot.transform.eulerAngles = new Vector3(x, y, 0);
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