using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LAP
{
    public class CombatControl : MonoBehaviour {
        [HideInInspector]
        public static CombatControl Main;
        public float MaxSanity;
        public float Sanity;
        public float MaxExistence;
        public float Existence;
        [Space]
        public float SanityDecay;
        public float ExistenceDecay;
        [Space]
        public App CurrentApp;
        public EnergyBar SanityBar;
        public EnergyBar ExistenceBar;

        public void Awake()
        {
            Main = this;
            UnityEngine.Cursor.visible = false;
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            ChangeSanity(-SanityDecay * Time.deltaTime);
            ChangeExistence(-ExistenceDecay * Time.deltaTime);

            if (CurrentApp && CurrentApp.Processes.Count > 0 && !CurrentApp.Processes[0].Processing && CurrentApp.Processes[0].AutoActive)
                CurrentApp.StartProcess();

            SanityBar.SetValue(Sanity);
            ExistenceBar.SetValue(Existence);
        }

        public void StartProcess()
        {
            if (!CurrentApp)
                return;
            CurrentApp.StartProcess();
        }

        public void StopProcess()
        {
            if (!CurrentApp)
                return;
            CurrentApp.StopProcess();
        }

        public void ChangeSanity(float Value)
        {
            Sanity += Value;
            if (Sanity > MaxSanity)
                Sanity = MaxSanity;
        }

        public void ChangeExistence(float Value)
        {
            Existence += Value;
            if (Existence > MaxExistence)
                Existence = MaxExistence;
        }
    }
}