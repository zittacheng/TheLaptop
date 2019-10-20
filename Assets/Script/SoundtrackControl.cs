using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LAP
{
    public class SoundtrackControl : MonoBehaviour {
        [HideInInspector]
        public static SoundtrackControl Main;
        public GameObject Base;
        public Animator Anim;
        [Space]
        public AudioSource BaseSource;
        public List<AudioClip> BaseClips;
        public int BaseIndex;
        [Space]
        public AudioSource TopSource;
        public List<AudioClip> TopClips;
        public int TopIndex;
        public bool TopActive;
        [Space]
        public Vector2 Delay;
        public float CurrentDelay;

        public void Awake()
        {
            if (Main)
                Destroy(gameObject);
            else
            {
                Main = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (ThatControl.Main && ThatControl.Main.AlreadyOpen)
            {
                Anim.SetBool("Fade", true);
                return;
            }

            if (CurrentDelay <= 0)
            {
                BaseSource.PlayOneShot(BaseClips[BaseIndex % BaseClips.Count]);
                BaseIndex++;
                if (TopActive)
                {
                    TopSource.PlayOneShot(TopClips[TopIndex % TopClips.Count]);
                    TopIndex++;
                    TopActive = false;
                }
                CurrentDelay = Random.Range(Delay.x, Delay.y);
            }

            CurrentDelay -= Time.deltaTime;
        }

        private void FixedUpdate()
        {
            if (Camera.main)
                Base.transform.position = Camera.main.transform.position;
        }
    }
}