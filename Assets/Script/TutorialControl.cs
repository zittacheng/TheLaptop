using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LAP
{
    public class TutorialControl : MonoBehaviour {
        public List<Cube> JumpCubes;
        public List<Cube> EditCubes;
        [Space]
        public Animator Jump;
        public Animator Edit;
        public Animator Finish;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (MainCharacterControl.Main.LaptopActive && !MainCharacterControl.Main.AlreadyWin
                && (!MainCharacterControl.Main.SelectingCube || MainCharacterControl.Main.SelectingCube.AlreadyEdited))
            {
                SetTutorial(Finish);
            }
            else if (JumpCubes.Contains(MainCharacterControl.Main.PlatformCube))
            {
                SetTutorial(Jump);
            }
            else if (EditCubes.Contains(MainCharacterControl.Main.PlatformCube) && MainCharacterControl.Main.EditCount < 1)
            {
                SetTutorial(Edit);
            }
            else
            {
                SetTutorial(null);
            }
        }

        public void SetTutorial(Animator Anim)
        {
            if (Jump != Anim)
                Jump.SetBool("Active", false);
            else
                Jump.SetBool("Active", true);
            if (Edit != Anim)
                Edit.SetBool("Active", false);
            else
                Edit.SetBool("Active", true);
            if (Finish != Anim)
                Finish.SetBool("Active", false);
            else
                Finish.SetBool("Active", true);
        }
    }
}