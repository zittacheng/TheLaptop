using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LAP
{
    public class Process : MonoBehaviour {
        public App app;
        public float MaxTime;
        public float CurrentTime;
        public bool AutoActive;
        public bool Processing;
        [HideInInspector]
        public bool AlreadyFinished;
        [Space]
        public float StartSanityChange;
        public float StartExistenceChange;
        public float FinishSanityChange;
        public float FinishExistenceChange;
        public float SanityChange;
        public float ExistenceChange;
        public float SanityScaling;
        public float ExistenceScaling;
        [Space]
        public List<string> ProcessingTexts;
        public string ButtonText;
        public string FinishedText;
        [Space]
        public Process RequiredProcess;
        public string RequiredText;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Processing)
            {
                CurrentTime -= Time.deltaTime;
                CombatControl.Main.ChangeSanity(SanityChange * Time.deltaTime);
                CombatControl.Main.ChangeExistence(ExistenceChange * Time.deltaTime);
                SanityChange += SanityScaling * Time.deltaTime;
                ExistenceChange += ExistenceScaling * Time.deltaTime;
            }

            if (CurrentTime <= 0 && !AlreadyFinished)
                Finish();

            if (RequiredProcess && RequiredProcess.AlreadyFinished)
                RequiredProcess = null;
        }

        public void StartProcess()
        {
            if (Processing || AlreadyFinished)
                return;
            Processing = true;
            CurrentTime = MaxTime;
            CombatControl.Main.ChangeSanity(StartSanityChange);
            CombatControl.Main.ChangeExistence(StartExistenceChange);
        }

        public void StopProcess()
        {
            if (AlreadyFinished)
            {
                StopCoroutine("FinishIE");
                app.RemoveProcess(this);
                return;
            }
            Processing = false;
            CurrentTime = MaxTime;
        }

        public void Finish()
        {
            AlreadyFinished = true;
            Processing = false;
            CombatControl.Main.ChangeSanity(FinishSanityChange);
            CombatControl.Main.ChangeExistence(FinishExistenceChange);
            StartCoroutine("FinishIE");
        }

        public IEnumerator FinishIE()
        {
            yield return new WaitForSeconds(1f);
            app.RemoveProcess(this);
        }
    }
}