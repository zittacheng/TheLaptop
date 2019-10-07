using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LAP
{
    public class UIControl : MonoBehaviour {
        [HideInInspector]
        public static UIControl Main;
        [Space]
        [HideInInspector] public bool AppMode;
        [HideInInspector] public AppRenderer AR;
        [HideInInspector] public GameObject Menu;

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

        }

        public void LegacyUpdate()
        {
            if (!AppMode)
            {
                if (AR.gameObject.activeInHierarchy)
                {
                    AR.RenderApp(null);
                    AR.Update();
                    AR.gameObject.SetActive(false);
                }
                Menu.gameObject.SetActive(true);
            }
            else
            {
                Menu.gameObject.SetActive(false);
                AR.gameObject.SetActive(true);
                AR.RenderApp(CombatControl.Main.CurrentApp);
            }
        }

        public void Back()
        {
            CombatControl.Main.StopProcess();
            AppMode = false;
        }
    }
}