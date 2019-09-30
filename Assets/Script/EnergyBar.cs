using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LAP
{
    public class EnergyBar : MonoBehaviour {
        public Image Bar;
        [Space]
        public Vector2 ValueLimit;
        public Vector2 PositionLimit;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SetValue(float Value)
        {
            float scale = (Value - ValueLimit.x) / (ValueLimit.y - ValueLimit.x);
            float x = PositionLimit.x + (PositionLimit.y - PositionLimit.x) * scale;
            Bar.fillAmount = x;
        }
    }
}