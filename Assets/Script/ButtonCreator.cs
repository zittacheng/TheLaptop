using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LAP
{
    public class ButtonCreator : MonoBehaviour {
        public GameObject Prefab;
        public float Count;
        public Vector3 StartPosition;
        public float Distance;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Process()
        {
            for (int i = transform.childCount - 1; i > 0; i--)
            {
                DestroyImmediate(transform.GetChild(i).gameObject);
            }
            Vector3 a = StartPosition + transform.position;
            for (int i = 1; i < Count; i++)
            {
                a += new Vector3(Distance, 0, 0);
                GameObject G = Instantiate(Prefab,transform);
                G.transform.position = a;
            }
        }
    }
}