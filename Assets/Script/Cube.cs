using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LAP
{
    public class Cube : MonoBehaviour {
        public GameObject SelectingBase;
        public Rigidbody Rig;
        public List<Script> Scripts;
        [Space]
        public List<Vector3> AlterPositions;
        public float MaxAPTime;
        [HideInInspector]
        public float APTime;
        public AnimationCurve APCurve;
        [Space]
        public float MovementSpeed;
        public Vector3 MoveTime;
        [HideInInspector]
        public Vector3 CurrentMoveTime;
        public Vector3 CurrentMoveDirection;
        public float MaxRestTime;
        public Vector3 CurrentRestTime;
        [Space]
        public CubeCharacterDetection CharacterDetection;
        [Space]
        public bool Processing;
        public bool Animating;

        public void Awake()
        {
            Ini();
            Exe();
        }

        public void Ini()
        {

        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (MainCharacterControl.Main.Stasis() || Animating)
            {
                Rig.velocity = new Vector3();
                return;
            }

            CurrentRestTime -= new Vector3(1, 1, 1) * Time.deltaTime;
            if (MoveTime.x != 0 && CurrentRestTime.x <= 0)
            {
                if (CurrentMoveDirection.x == 1)
                {
                    float s = 0;
                    if (MoveTime.x > 0)
                        s = 1;
                    else
                        s = -1;
                    Rig.velocity = new Vector3(MovementSpeed * s, Rig.velocity.y, Rig.velocity.z);
                    CurrentMoveTime.x -= Time.deltaTime * s;
                    if (CurrentMoveTime.x * s <= 0)
                    {
                        CurrentMoveDirection.x = -1;
                        CurrentRestTime.x = MaxRestTime;
                    }
                }
                else
                {
                    float s = 0;
                    if (MoveTime.x > 0)
                        s = -1;
                    else
                        s = 1;
                    Rig.velocity = new Vector3(MovementSpeed * s, Rig.velocity.y, Rig.velocity.z);
                    CurrentMoveTime.x -= Time.deltaTime * s;
                    if (Mathf.Abs(CurrentMoveTime.x) >= Mathf.Abs(MoveTime.x))
                    {
                        CurrentMoveDirection.x = 1;
                        CurrentRestTime.x = MaxRestTime;
                    }
                }
            }
            else
                Rig.velocity = new Vector3(0, Rig.velocity.y, Rig.velocity.z);

            if (MoveTime.y != 0 && !CharacterDetection.Detected && CurrentRestTime.y <= 0)
            {
                if (CurrentMoveDirection.y == 1)
                {
                    float s = 0;
                    if (MoveTime.y > 0)
                        s = 1;
                    else
                        s = -1;
                    Rig.velocity = new Vector3(Rig.velocity.x, MovementSpeed * s, Rig.velocity.z);
                    CurrentMoveTime.y -= Time.deltaTime * s;
                    if (CurrentMoveTime.y * s <= 0)
                    {
                        CurrentMoveDirection.y = -1;
                        CurrentRestTime.y = MaxRestTime;
                    }
                }
                else
                {
                    float s = 0;
                    if (MoveTime.y > 0)
                        s = -1;
                    else
                        s = 1;
                    Rig.velocity = new Vector3(Rig.velocity.x, MovementSpeed * s, Rig.velocity.z);
                    CurrentMoveTime.y -= Time.deltaTime * s;
                    if (Mathf.Abs(CurrentMoveTime.y) >= Mathf.Abs(MoveTime.y))
                    {
                        CurrentMoveDirection.y = 1;
                        CurrentRestTime.y = MaxRestTime;
                    }
                }
            }
            else
                Rig.velocity = new Vector3(Rig.velocity.x, 0, Rig.velocity.z);

            if (MoveTime.z != 0 && CurrentRestTime.z <= 0)
            {
                if (CurrentMoveDirection.z == 1)
                {
                    float s = 0;
                    if (MoveTime.z > 0)
                        s = 1;
                    else
                        s = -1;
                    Rig.velocity = new Vector3(Rig.velocity.x, Rig.velocity.y, MovementSpeed * s);
                    CurrentMoveTime.z -= Time.deltaTime * s;
                    if (CurrentMoveTime.z * s <= 0)
                    {
                        CurrentMoveDirection.z = -1;
                        CurrentRestTime.z = MaxRestTime;
                    }
                }
                else
                {
                    float s = 0;
                    if (MoveTime.z > 0)
                        s = -1;
                    else
                        s = 1;
                    Rig.velocity = new Vector3(Rig.velocity.x, Rig.velocity.y, MovementSpeed * s);
                    CurrentMoveTime.z -= Time.deltaTime * s;
                    if (Mathf.Abs(CurrentMoveTime.z) >= Mathf.Abs(MoveTime.z))
                    {
                        CurrentMoveDirection.z = 1;
                        CurrentRestTime.z = MaxRestTime;
                    }
                }
            }
            else
                Rig.velocity = new Vector3(Rig.velocity.x, Rig.velocity.y, 0);
        }

        public void FixedUpdate()
        {
            transform.Translate(Rig.velocity * Time.fixedDeltaTime);
        }

        public void Exe()
        {
            Processing = true;
            PreExe();
            StartCoroutine("ExeIE");
        }

        public void PreExe()
        {
            MoveTime = new Vector3();
        }

        public IEnumerator ExeIE()
        {
            foreach (Script S in Scripts)
            {
                if (S)
                    yield return ExeScript(S.Command, S.Value);
            }
            yield return ExeFinal();
        }

        public IEnumerator ExeScript(string Command, string Value)
        {
            if (Command == "PositionX")
            {
                if (float.TryParse(Value, out float a))
                    yield return PositionChangeIE(new Vector3(a * 5, transform.position.y, transform.position.z));
            }
            else if (Command == "PositionY")
            {
                if (float.TryParse(Value, out float a))
                    yield return PositionChangeIE(new Vector3(transform.position.x, a * 5, transform.position.z));
            }
            else if (Command == "PositionZ")
            {
                if (float.TryParse(Value, out float a))
                    yield return PositionChangeIE(new Vector3(transform.position.x, transform.position.y, a * 5));
            }
            else if (Command == "MoveX")
            {
                if (float.TryParse(Value, out float a))
                {
                    MoveTime.x = a;
                    if (a != 0)
                    {
                        CurrentMoveTime.x = MoveTime.x;
                        CurrentMoveDirection.x = 1;
                    }
                }
            }
            else if (Command == "MoveY")
            {
                if (float.TryParse(Value, out float a))
                {
                    MoveTime.y = a;
                    if (a != 0)
                    {
                        CurrentMoveTime.y = MoveTime.y;
                        CurrentMoveDirection.y = 1;
                    }
                }
            }
            else if (Command == "MoveZ")
            {
                if (float.TryParse(Value, out float a))
                {
                    MoveTime.z = a;
                    if (a != 0)
                    {
                        CurrentMoveTime.z = MoveTime.z;
                        CurrentMoveDirection.z = 1;
                    }
                }
            }
        }

        public IEnumerator ExeFinal()
        {
            yield return 0;
            Processing = false;
        }

        public IEnumerator PositionChangeIE(Vector3 Position)
        {
            Animating = true;
            if ((Position - transform.position).magnitude <= 0.1f)
            {
                transform.position = Position;
            }
            else
            {
                Vector3 OriPosition = transform.position;
                APTime = MaxAPTime;
                while (APTime > 0)
                {
                    transform.position = OriPosition + (Position - OriPosition) * APCurve.Evaluate(1 - APTime / MaxAPTime);
                    APTime -= Time.deltaTime;
                    yield return 0;
                }
                transform.position = Position;
            }
            Animating = false;
        }

        public void AddScript()
        {
            Script S = gameObject.AddComponent<Script>();
            Scripts.Add(S);
        }

        public void RemoveScript(int Index)
        {
            if (Scripts.Count <= Index)
                return;
            Script S = Scripts[Index];
            Scripts.RemoveAt(Index);
            Destroy(S);
        }

        public void Select()
        {
            SelectingBase.SetActive(true);
        }

        public void UnSelect()
        {
            SelectingBase.SetActive(false);
        }

        public void EditorApply()
        {
            Scripts = new List<Script>();
            foreach (Script S in GetComponents<Script>())
                Scripts.Add(S);
        }

        public bool CanEdit()
        {
            return !Processing;
        }
    }
}