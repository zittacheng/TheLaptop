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
        public Animator FadeAnim;
        public GameObject AimDot;
        public GameObject GroundRayPoint;
        public LayerMask GroundRayMask;
        [Space]
        public Cube SelectingCube;
        public Cube LastCube;
        public float CubeRange;
        public LayerMask CubeRayMask;
        [Space]
        public Cube PlatformCube;
        public bool LaptopActive;
        public bool OnGround;
        [Space]
        public int EditCount;
        [Space]
        public float AnimValue;
        public bool Animating;
        [HideInInspector]
        public bool AlreadyWin;

        public void Awake()
        {
            Main = this;
            UnityEngine.Cursor.visible = false;
            UnityEngine.Cursor.lockState = CursorLockMode.None;
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            RotationUpdate();

            if (!LaptopActive)
            {
                Ray CR = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
                Debug.DrawRay(CR.origin, CR.direction, Color.green);
                if (Physics.Raycast(CR, out RaycastHit SHit, CubeRange, CubeRayMask) && SHit.transform.GetComponent<Cube>() != PlatformCube)
                    SelectingCube = SHit.transform.GetComponent<Cube>();
                else
                    SelectingCube = null;
            }

            if (SelectingCube != LastCube)
            {
                if (SelectingCube)
                    SelectingCube.Select();
                if (LastCube)
                    LastCube.UnSelect();
                LastCube = SelectingCube;
            }

            if (OnGround && !Animating && !AlreadyWin)
            {
                if (!LaptopActive && (Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(0)) && (!SelectingCube || SelectingCube.CanEdit()))
                    LaptopMode(true);
                else if (LaptopActive && (Input.GetMouseButtonDown(1) || !OnGround))
                    LaptopMode(false);
            }

            Ray R = new Ray(GroundRayPoint.transform.position, Vector3.down);
            Debug.DrawRay(R.origin, R.direction * 0.1f, Color.green);
            if (Physics.Raycast(R, out RaycastHit Hit, 0.1f, GroundRayMask))
            {
                OnGround = true;
                if (Hit.transform.GetComponent<Cube>())
                    PlatformCube = Hit.transform.GetComponent<Cube>();
                if (Hit.transform.GetComponent<VictoryCube>())
                    ThatControl.Main.Victory();
            }
            else
            {
                OnGround = false;
                PlatformCube = null;
            }

            AimDot.SetActive(!Stasis());
        }

        public void FixedUpdate()
        {
            RotationUpdate();
        }

        public void SetSpeed(Vector3 Value)
        {
            if (PlatformCube)
                Value += PlatformCube.Rig.velocity;
            Rig.velocity = Value;
        }

        public void OnTriggerEnter(Collider C)
        {
            if (C.GetComponent<CubeCharacterDetection>())
                C.GetComponent<CubeCharacterDetection>().Detected = true;
            if (C.GetComponent<CheckPoint>())
                C.GetComponent<CheckPoint>().SetActive();
            if (C.GetComponent<Cube>())
                C.GetComponent<Cube>().Colliding = true;
        }

        public void OnTriggerExit(Collider C)
        {
            if (C.GetComponent<CubeCharacterDetection>())
                C.GetComponent<CubeCharacterDetection>().Detected = false;
            if (C.GetComponent<Cube>())
                C.GetComponent<Cube>().Colliding = false;
        }

        public void RotationUpdate()
        {
            RotationBase.transform.eulerAngles = RotationObject.transform.eulerAngles;
        }

        public void LaptopMode(bool On)
        {
            Anim.SetBool("LaptopMode", On);
            if (On)
            {
                if (SelectingCube && SelectingCube.CanEdit() && !SelectingCube.Locked)
                    SelectingCube.OnSelect();
                MovControl.LaptopOn();
                UnityEngine.Cursor.lockState = CursorLockMode.None;
                Cursor.Main.SetAnim(true);
                StartCoroutine("DelayLaptopOn");
            }
            else
            {
                LaptopActive = false;
                Cursor.Main.SetAnim(false);
                UnityEngine.Cursor.lockState = CursorLockMode.Locked;
                if (SelectingCube && SelectingCube.CanEdit() && !SelectingCube.Locked)
                    SelectingCube.Exe(false);
                if (SelectingCube && !SelectingCube.Locked && ((SelectingCube.Scripts.Count > 0 && !SelectingCube.Empty()) || SelectingCube.AlreadyEdited))
                    ChangeEditCount(1);
                MovControl.LaptopOff();
            }
        }

        public IEnumerator DelayLaptopOn()
        {
            yield return 0;
            yield return 0;
            yield return 0;
            LaptopActive = true;
        }

        public IEnumerator CursorActivate()
        {
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
            yield return 0;
            UnityEngine.Cursor.lockState = CursorLockMode.None;
            Cursor.Main.SetAnim(true);
        }

        public void ChangeEditCount(int Value)
        {
            EditCount += Value;
        }

        public void SetPosition(Vector3 Value)
        {
            transform.position = Value;
        }

        public bool Stasis()
        {
            return LaptopActive || Animating;
        }
    }
}