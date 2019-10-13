using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LAP
{
    public class SaveControl : MonoBehaviour {
        public static SaveControl Main;
        [Space]
        public List<string> StringKeys;
        public List<string> StringValues;
        [Space]
        public List<string> IntKeys;
        public List<int> IntValues;
        [Space]
        public List<string> FloatKeys;
        public List<float> FloatValues;

        public void Awake()
        {
            if (Main && Main != this)
            {
                Destroy(gameObject);
                return;
            }
            Main = this;
            DontDestroyOnLoad(gameObject);
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public static string GetString(string Key)
        {
            if (Main)
            {
                if (Main.StringKeys.Contains(Key))
                    return Main.StringValues[Main.StringKeys.IndexOf(Key)];
                else
                    return "";
            }
            else
                return PlayerPrefs.GetString(Key);
        }

        public static void SetString(string Key, string Value)
        {
            if (Main)
            {
                if (Main.StringKeys.Contains(Key))
                {
                    Main.StringValues[Main.StringKeys.IndexOf(Key)] = Value;
                }
                else
                {
                    Main.StringKeys.Add(Key);
                    Main.StringValues.Add(Value);
                }
            }
            else
                PlayerPrefs.SetString(Key, Value);
        }

        public static int GetInt(string Key)
        {
            if (Main)
            {
                if (Main.IntKeys.Contains(Key))
                    return Main.IntValues[Main.IntKeys.IndexOf(Key)];
                else
                    return 0;
            }
            else
                return PlayerPrefs.GetInt(Key);
        }

        public static void SetInt(string Key, int Value)
        {
            if (Main)
            {
                if (Main.IntKeys.Contains(Key))
                {
                    Main.IntValues[Main.IntKeys.IndexOf(Key)] = Value;
                }
                else
                {
                    Main.IntKeys.Add(Key);
                    Main.IntValues.Add(Value);
                }
            }
            else
                PlayerPrefs.SetInt(Key, Value);
        }

        public static float GetFloat(string Key)
        {
            if (Main)
            {
                if (Main.FloatKeys.Contains(Key))
                    return Main.FloatValues[Main.FloatKeys.IndexOf(Key)];
                else
                    return 0f;
            }
            else
                return PlayerPrefs.GetFloat(Key);
        }

        public static void SetFloat(string Key, float Value)
        {
            if (Main)
            {
                if (Main.FloatKeys.Contains(Key))
                {
                    Main.FloatValues[Main.FloatKeys.IndexOf(Key)] = Value;
                }
                else
                {
                    Main.FloatKeys.Add(Key);
                    Main.FloatValues.Add(Value);
                }
            }
            else
                PlayerPrefs.SetFloat(Key, Value);
        }
    }
}