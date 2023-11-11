using System;
using System.Collections.Generic;
using UnityEngine;

namespace Vmaya.Util
{
    public class DeferredExecuter : MonoBehaviour
    {
        private struct ActionData
        {
            public Action action;
            public int group;
        }
        private List<ActionData> _actions = new List<ActionData>();
        private static DeferredExecuter _instance;
        public static DeferredExecuter instance => getInstance();
        private List<Action> _passed = new List<Action>();

        private bool _isQuit;

        private static DeferredExecuter getInstance()
        {
            if (!_instance)
            {
                _instance = FindObjectOfType<DeferredExecuter>();
                if (!_instance) (_instance = (new GameObject()).AddComponent<DeferredExecuter>()).name = "DeferredExecuter";
            }
            return _instance;
        }

        public bool Contains(Action action)
        {
            foreach (ActionData item in _actions)
                if (item.action.Equals(action)) return true;
            return false;
        }

        public static void addDelayCall(Action action, int group)
        {
            if (!instance._isQuit)
            {
                //Debug.Log(action);
                if (Application.isPlaying)
                {
                    if (!instance.Contains(action))
                    {
                        ActionData actionData;
                        actionData.action = action;
                        actionData.group = group;
                        instance._actions.Add(actionData);
                    }
                }
                else action();
            }
        }

        protected void runList()
        {
            int cmd(ActionData x, ActionData y)
            {
                return x.group - y.group;
            }

            List<ActionData> tmp = new List<ActionData>(_actions);
            _actions.Clear();

            tmp.Sort(cmd);

            foreach (ActionData item in tmp)
            {
                Component target = item.action.Target as Component;
                if (!_passed.Contains(item.action) && ((target == null) || !Utils.IsDestroyed(target)))
                {
                    _passed.Add(item.action);
                    item.action();
                }
            }

            if (_actions.Count > 0) runList();
        }

        public void Clear()
        {
            _actions.Clear();
        }

        private void OnApplicationQuit()
        {
            _isQuit = true;
        }

        private void FixedUpdate()
        {
            if (!_isQuit && (_actions.Count > 0))
            {
                _passed = new List<Action>();
                runList();
            }
        }
    }
}
