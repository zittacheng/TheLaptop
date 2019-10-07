using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LAP
{
    public class Cursor : MonoBehaviour {
        public static Cursor Main;
        public Camera PositionCamera;
        public GameObject AnimBase;

        public void Awake()
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
            PositionUpdate();
        }

        public void FixedUpdate()
        {
            PositionUpdate();
        }

        public void PositionUpdate()
        {
            Vector3 a = PositionCamera.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(a.x, a.y, transform.position.z);
        }

        public void OnTriggerEnter2D(Collider2D C)
        {
            if (C.GetComponent<Button>())
                C.GetComponent<Button>().Active = true;
        }

        public void OnTriggerExit2D(Collider2D C)
        {
            if (C.GetComponent<Button>())
                C.GetComponent<Button>().Active = false;
        }

        public void SetAnim(bool Active)
        {
            AnimBase.SetActive(Active);
        }
    }
}