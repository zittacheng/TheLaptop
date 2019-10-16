using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LAP
{
    public class ThatControl : MonoBehaviour {
        [HideInInspector]
        public static ThatControl Main;
        public GameObject CubePrefab;
        public GameObject LaptopBase;
        public GameObject VictoryBase;
        public GameObject OpenBase;
        public GameObject AxisRenderer;
        public List<Cube> Opens;
        public Animator ResetAnim;
        public GameObject RespawnPoint;
        public Animator FinaleAnim;
        [Space]
        public Animator SceneFade;
        public string CurrentSceneName;
        [Space]
        public bool AlreadyLoading;
        public bool AlreadyWin;
        public bool AlreadyResetting;

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

        public void Victory()
        {
            if (AlreadyWin)
                return;
            AlreadyWin = true;
            if (Level.Main.NextLevelKey != "")
            {
                LaptopBase.SetActive(false);
                VictoryBase.SetActive(true);
                MainCharacterControl.Main.AlreadyWin = true;
                MainCharacterControl.Main.LaptopMode(true);
            }
            else
            {
                LaptopBase.SetActive(false);
                OpenBase.SetActive(true);
                MainCharacterControl.Main.LaptopMode(true);
            }
        }

        public void Open()
        {
            MainCharacterControl.Main.LaptopMode(false);
            LaptopBase.SetActive(true);
            OpenBase.SetActive(false);
            AxisRenderer.SetActive(false);
            foreach (Cube C in Opens)
                C.Exe(true);
        }

        public void Retry()
        {
            if (AlreadyLoading)
                return;
            AlreadyLoading = true;
            StartCoroutine("LoadScene");
        }

        public void CharacterReset()
        {
            if (AlreadyResetting)
                return;
            AlreadyResetting = true;
            StartCoroutine("CharacterResetIE");
        }

        public IEnumerator CharacterResetIE()
        {
            ResetAnim.SetTrigger("Play");
            yield return new WaitForSeconds(0.48f);
            MainCharacterControl.Main.MovControl.RealReset();
            AlreadyResetting = false;
        }

        public void Finale()
        {
            MainCharacterControl.Main.AlreadyWin = true;
            MainCharacterControl.Main.MovControl.AlreadyDead = true;
            MainCharacterControl.Main.LapControl.Active = false;
            FinaleAnim.SetTrigger("Play");
        }

        public void NextLevel()
        {
            if (AlreadyLoading)
                return;
            AlreadyLoading = true;
            if (Level.Main.NextLevelKey != "")
                SaveControl.SetString("Level", Level.Main.NextLevelKey);
            StartCoroutine("LoadScene");
        }

        public IEnumerator LoadScene()
        {
            SceneFade.SetTrigger("Play");
            yield return new WaitForSeconds(0.52f);
            UnityEngine.SceneManagement.SceneManager.LoadScene(CurrentSceneName);
        }
    }
}