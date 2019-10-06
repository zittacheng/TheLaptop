using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LAP
{
    public class CameraControl : MonoBehaviour {
        [HideInInspector]
        public static CameraControl Main;
        public Camera MainCamera;
        public CameraPoint CurrentPoint;
        public float TransitTime;
        public bool Animating;

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
            if (Animating)
                return;
            PositionUpdate();
        }

        private void FixedUpdate()
        {
            if (Animating)
                return;
            PositionUpdate();
        }

        public void PositionUpdate()
        {
            if (!CurrentPoint)
                return;
            transform.position = CurrentPoint.transform.position;
            transform.eulerAngles = CurrentPoint.transform.eulerAngles;
            MainCamera.fieldOfView = CurrentPoint.ViewField;
        }

        public void SetPoint(CameraPoint Point, float T)
        {
            Animating = true;
            CurrentPoint = Point;
            TransitTime = T;
            StopCoroutine("TransitToPoint");
            StartCoroutine("TransitToPoint");
        }

        public IEnumerator TransitToPoint()
        {
            Vector3 OriPosition = transform.position;
            Vector3 OriRotation = transform.eulerAngles;
            Vector3 TargetPosition = CurrentPoint.transform.position;
            Vector3 TargetRotation = CurrentPoint.transform.eulerAngles;
            float Orix = AbsAngle(OriRotation.x);
            float Oriy = AbsAngle(OriRotation.y);
            float Oriz = AbsAngle(OriRotation.z);
            float Targetx = AbsAngle(TargetRotation.x);
            float Targety = AbsAngle(TargetRotation.y);
            float Targetz = AbsAngle(TargetRotation.z);
            float t = TransitTime;
            while (t > Time.deltaTime + 0.01f)
            {
                float Scale = t / TransitTime;
                transform.position = OriPosition + (TargetPosition - OriPosition) * Scale;
                
                transform.eulerAngles = new Vector3(Mathf.LerpAngle(transform.eulerAngles.x, CurrentPoint.transform.eulerAngles.x, t * Time.deltaTime),
                    Mathf.LerpAngle(transform.eulerAngles.y, CurrentPoint.transform.eulerAngles.y, t * Time.deltaTime),
                    Mathf.LerpAngle(transform.eulerAngles.z, CurrentPoint.transform.eulerAngles.z, t * Time.deltaTime));
                MainCamera.fieldOfView = Mathf.Lerp(MainCamera.fieldOfView, CurrentPoint.ViewField, t * Time.deltaTime);
                t -= Time.deltaTime;
                yield return 0;
            }
            Animating = false;
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