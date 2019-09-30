using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace LAP
{
    public class AppRenderer : MonoBehaviour {
        public Image ICON;
        public TextMeshProUGUI NameText;
        public TextMeshProUGUI DescriptionText;
        public TextMeshProUGUI ProcessText;
        public float LastIndex;
        public float IndexSpeed;
        public App CurrentApp;
        public Process CurrentProcess;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        public void Update()
        {
            LastIndex += IndexSpeed * Time.deltaTime;

            if (CurrentApp)
            {
                NameText.text = CurrentApp.Name;
                DescriptionText.text = CurrentApp.Description;
                ICON.sprite = CurrentApp.Icon;
                ICON.gameObject.SetActive(true);
                if (CurrentApp.Processes.Count > 0)
                    RenderProcess(CurrentApp.Processes[0]);
                else
                    RenderProcess(null);
            }
            else
            {
                NameText.text = "";
                DescriptionText.text = "";
                ICON.gameObject.SetActive(false);
                RenderProcess(null);
            }

            if (CurrentProcess)
            {
                if (CurrentProcess.Processing)
                {
                    while ((int)LastIndex > CurrentProcess.ProcessingTexts.Count - 1)
                        LastIndex -= CurrentProcess.ProcessingTexts.Count;
                    ProcessText.text = CurrentProcess.ProcessingTexts[(int)LastIndex];
                }
                else
                    LastIndex = 0;
                if (!CurrentProcess.Processing)
                {
                    if (CurrentProcess.AlreadyFinished)
                        ProcessText.text = CurrentProcess.FinishedText;
                    else if (CurrentProcess.RequiredProcess)
                        ProcessText.text = CurrentProcess.RequiredText;
                    else
                        ProcessText.text = CurrentProcess.ButtonText;
                }
            }
            else
            {
                ProcessText.text = "";
                LastIndex = 0;
            }
        }

        public void RenderApp(App app)
        {
            CurrentApp = app;
        }

        public void RenderProcess(Process P)
        {
            CurrentProcess = P;
        }
    }
}