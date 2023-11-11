using UnityEngine;

namespace Vmaya.Scene3D
{
    public class BaseLog : MonoBehaviour
    {
        public enum logType { Notify, Message, Warning, Error };

        private static BaseLog _instance;
        private static bool _isQuit;

        public static BaseLog instance
        {
            get
            {
                if (!_instance)
                {
                    _instance = FindObjectOfType<BaseLog>(true);
                    if (!_instance && !_isQuit) _instance = createDefault();
                }
                return _instance;
            }
        }

        private static BaseLog createDefault()
        {
            GameObject ga = new GameObject("Log");
            return ga.AddComponent<BaseLog>();
        }

        private void OnApplicationQuit()
        {
            _isQuit = true;
        }

        protected virtual void Awake()
        {
            _instance = this;
        }

        public static void Add(logType type, string text)
        {
            instance.AddLog(type, text);
        }
        protected virtual void AddLog(logType type, string text)
        {

        }

        public virtual void ClearLog()
        {

        }
    }
}