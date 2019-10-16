using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LAP
{
    public class Cube : MonoBehaviour {
        public GameObject SelectingBase;
        public Rigidbody Rig;
        public List<Script> Scripts;
        [HideInInspector]
        public int ScriptIndex;
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
        public float LoopTime;
        public float CurrentLoopTime;
        [Space]
        public Collider Col;
        public CubeCharacterDetection CharacterDetection;
        public GameObject SolidObject;
        public GameObject TriggerObject;
        [Space]
        public bool Locked;
        public bool Solid;
        public bool Colliding;
        public bool Processing;
        public bool Animating;
        public bool AlreadyEdited;
        public bool Death;

        public void Awake()
        {
            Ini();
            //Exe(false);
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
            if (Scripts.Count > 0 && !Empty() && !AlreadyEdited)
                AlreadyEdited = true;

            if (!Solid)
                Col.isTrigger = true;
            else if (!Colliding)
                Col.isTrigger = false;

            if (TriggerObject)
            {
                if (Col.isTrigger)
                {
                    TriggerObject.SetActive(true);
                    SolidObject.SetActive(false);
                }
                else
                {
                    TriggerObject.SetActive(false);
                    SolidObject.SetActive(true);
                }
            }

            if (MainCharacterControl.Main.Stasis() || Animating)
            {
                Rig.velocity = new Vector3();
                return;
            }

            CurrentRestTime -= new Vector3(1, 1, 1) * Time.deltaTime;
            CurrentLoopTime -= Time.deltaTime;
            if (LoopTime > 0 && CurrentLoopTime <= 0)
                Exe(true);

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

        public void OnSelect()
        {
            foreach (Script S in Scripts)
                SelectScript(S, S.Command, S.Value);
        }

        public void SelectScript(Script S, string Command, string Value)
        {
            if (Command == "MoveX" || Command == "MoveY" || Command == "MoveZ")
            {
                if (int.TryParse(Value, out int a))
                    S.Value = "";
            }
        }

        public void Exe(bool Auto)
        {
            Processing = true;
            PreExe();
            StartCoroutine(ExeIE(Auto));
        }

        public void PreExe()
        {
            MoveTime = new Vector3();
            LoopTime = 0;
            ScriptIndex = 0;
        }

        public IEnumerator ExeIE(bool Auto)
        {
            int StartLoopIndex = 0;
            int LoopIndex = 0;
            foreach (Script S in Scripts)
            {
                if (S)
                    yield return ExeScript(S, S.Command, S.Value, Auto);
                if (S.StartLoop())
                    StartLoopIndex = ScriptIndex;
                else if (S.Loop())
                {
                    LoopIndex = ScriptIndex;
                    for (int i = Scripts.Count - 1; i >= LoopIndex; i--)
                        RemoveScript(i);
                    for (int i = StartLoopIndex - 1; i >= 0; i--)
                        RemoveScript(i);
                    break;
                }
            }
            yield return ExeFinal();
        }

        public IEnumerator ExeScript(Script S, string Command, string Value, bool Auto)
        {
            ScriptIndex++;
            if (Command == "MoveX")
            {
                if (int.TryParse(Value, out int a))
                    yield return PositionChangeIE(transform.position + new Vector3(a * 2, 0, 0), this);
            }
            else if (Command == "MoveY")
            {
                if (int.TryParse(Value, out int a))
                    yield return PositionChangeIE(transform.position + new Vector3(0, a * 2, 0), this);
            }
            else if (Command == "MoveZ")
            {
                if (int.TryParse(Value, out int a))
                {
                    Animating = true;
                    yield return PositionChangeIE(transform.position + new Vector3(0, 0, a * 2), this);
                    Animating = false;
                }
            }
            else if (Command == "Delay")
            {
                if (int.TryParse(Value, out int a))
                {
                    float t = 0;
                    while (t < a)
                    {
                        yield return 0;
                        if (!MainCharacterControl.Main.Stasis())
                            t += Time.deltaTime;
                    }
                }
            }
            else if (Command == "Loop")
            {
                LoopTime = 0.1f;
                CurrentLoopTime = LoopTime;
                yield return 0;
            }
            else if (Command == "LoopDelay")
            {
                if (int.TryParse(Value, out int a))
                    LoopTime = a;
                else
                    LoopTime = 0.1f;
                if (LoopTime < 0.1f)
                    LoopTime = 0.1f;
                CurrentLoopTime = LoopTime;
                yield return 0;
            }
            else if (Command == "Instantiate")
            {
                GameObject G = Instantiate(ThatControl.Main.CubePrefab);
                G.transform.position = transform.position;
                yield return new WaitForSeconds(0.1f);
            }
            if (Command == "InstantiateX")
            {
                GameObject G = Instantiate(ThatControl.Main.CubePrefab);
                G.transform.position = transform.position;
                if (int.TryParse(Value, out int a))
                    StartCoroutine(PositionChangeIE(transform.position + new Vector3(a * 2, 0, 0), G.GetComponent<Cube>()));
                yield return new WaitForSeconds(0.1f);
            }
            else if (Command == "InstantiateY")
            {
                GameObject G = Instantiate(ThatControl.Main.CubePrefab);
                G.transform.position = transform.position;
                if (int.TryParse(Value, out int a))
                    StartCoroutine(PositionChangeIE(transform.position + new Vector3(0, a * 2, 0), G.GetComponent<Cube>()));
                yield return new WaitForSeconds(0.1f);
            }
            else if (Command == "InstantiateZ")
            {
                GameObject G = Instantiate(ThatControl.Main.CubePrefab);
                G.transform.position = transform.position;
                if (int.TryParse(Value, out int a))
                    StartCoroutine(PositionChangeIE(transform.position + new Vector3(0, 0, a * 2), G.GetComponent<Cube>()));
                yield return new WaitForSeconds(0.1f);
            }
            else if (Command == "Destroy")
            {
                if (Value == "Myself")
                {
                    ThatControl.Main.Finale();
                    yield break;
                }
            }
            else if (Command == "Move_LegacyX")
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
            else if (Command == "Move_LegacyY")
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
            else if (Command == "Move_LegacyZ")
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

        public IEnumerator PositionChangeIE(Vector3 Position, Cube C)
        {
            C.Animating = true;
            C.Solid = false;
            if ((Position - C.transform.position).magnitude <= 0.1f)
            {
                C.transform.position = Position;
            }
            else
            {
                Vector3 OriPosition = C.transform.position;
                C.APTime = C.MaxAPTime;
                while (C.APTime > 0)
                {
                    C.transform.position = OriPosition + (Position - OriPosition) * C.APCurve.Evaluate(1 - C.APTime / C.MaxAPTime);
                    C.APTime -= Time.deltaTime;
                    yield return 0;
                }
                C.transform.position = Position;
            }
            C.Solid = true;
            yield return new WaitForSeconds(0.2f);
            C.Animating = false;
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
            return !Processing && !Animating;
        }

        public bool Empty()
        {
            bool a = true;
            for (int i = Scripts.Count - 1; i >= 0; i--)
            {
                if (Scripts[i].Command != null && Scripts[i].Command != "")
                    a = false;
            }
            return a;
        }
    }
}