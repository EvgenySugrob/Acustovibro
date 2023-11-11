using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using Vmaya.Command;
using Vmaya.Scene3D;
using Vmaya.UI.Commands;

namespace Vmaya.UI
{
    public class EditorMode : MonoBehaviour
    {
        public static string ComponentInsert = "insert";
        //Режимы все с маленькой буквы. Зарезервированны - edit, play, remove

        public UnityEvent onChange;
        private string _mode;

        [SerializeField]
        private bool _useCommand;

        public string mode {
            get { return _mode; } set { setMode(value, _useCommand); }
        }

        private static List<Action> _afterStart = new List<Action>();

        private static EditorMode _instance;
        public static EditorMode instance
        {
            get
            {
                if (!_instance) _instance = createDefault();
                return _instance;
            }
        }

        private static EditorMode createDefault()
        {
            GameObject ga = new GameObject();
            EditorMode em = ga.AddComponent<EditorMode>();
            ga.name = "EditorMode";
            em.onChange = new UnityEvent();

            Debug.Log("Create default EditorMode");
            return em;
        }

        internal bool EqualsMode(string a_mode)
        {
            return !string.IsNullOrEmpty(mode) && mode.Equals(a_mode);
        }

        private void Awake()
        {
            if (_instance) Debug.LogError("There should only be an instance");
            _instance = this;
        }

        private void Start()
        {
            foreach (Action action in _afterStart) action();
        }

        public static void afterStart(Action action)
        {
            if (_instance) action();
            else _afterStart.Add(action);
        }

        static public bool Equal(string a_mode)
        {
            if (instance) return instance._mode == a_mode;
            else return false;
        }

        static public void setMode(string a_mode, bool useCommand)
        {
            if (instance && (instance._mode != a_mode))
            {
                if (useCommand && CommandManager.instance)
                    CommandManager.instance.executeCmd(new EditorModeCommand(a_mode));
                else
                {
                    /*
                    if (string.IsNullOrEmpty(a_mode))
                    {
                        Debug.Log(a_mode);
                    }*/
                    instance._mode = a_mode;
                    instance.onChange.Invoke();
                }
            }
        }

        public static bool isEditMode
        {
            get
            {
                return instance && (instance.mode == "edit");
            }
        }

        public static bool isPlayMode
        {
            get
            {
                return instance && (instance.mode == "play");
            }
        }
    }
}