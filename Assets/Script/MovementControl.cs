using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LAP
{
    public class MovementControl : MonoBehaviour {
        public bool Active;
        public bool AlreadyDead;
        [Space]
        public Rigidbody Rig;
        public float MovementSpeed;
        public float JumpSpeed;
        [Space]
        public GameObject HorizontalPivot;
        public GameObject VerticalPivot;
        public float RotationSpeed;
        public Vector2 VerticalAngleLimit;

        public void Awake()
        {
            if (Level.Main.OverrideSpawn)
            {
                transform.position = Level.Main.OverrideSpawn.transform.position;
                HorizontalPivot.transform.eulerAngles = new Vector3(0, Level.Main.OverrideSpawn.transform.eulerAngles.y, 0);
            }
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (!Active || AlreadyDead)
            {
                MainCharacterControl.Main.SetSpeed(new Vector3(0, Rig.velocity.y, 0));
                return;
            }

            Vector3 Speed = Rig.velocity;
            float y = Rig.velocity.y;
            Speed = HorizontalPivot.transform.TransformDirection(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            Speed.Normalize();
            Speed *= MovementSpeed;
            if (Input.GetKeyDown(KeyCode.Space) && MainCharacterControl.Main.OnGround)
                Speed = new Vector3(Speed.x, JumpSpeed, Speed.z);
            else
                Speed = new Vector3(Speed.x, y, Speed.z);
            MainCharacterControl.Main.SetSpeed(Speed);
        }

        public void FixedUpdate()
        {
            HorizontalPivot.transform.eulerAngles = new Vector3(0, HorizontalPivot.transform.eulerAngles.y, 0);

            if (Rig.velocity.y < -50f)
                Rig.velocity = new Vector3(Rig.velocity.x, -50f, Rig.velocity.z);

            if (transform.position.y <= -100f)
            {
                ThatControl.Main.CharacterReset();
            }

            if (!Active || AlreadyDead)
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

        public void RealReset()
        {
            transform.position = ThatControl.Main.RespawnPoint.transform.position;
            Rig.velocity = new Vector3(0, Rig.velocity.y, 0);
        }

        public void LaptopOn()
        {
            Active = false;
        }

        public void LaptopOff()
        {
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