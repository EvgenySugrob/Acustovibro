using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vmaya.UI.Menu
{
    public class AvailableActionMap : MonoBehaviour, IAvailableProvider, IActionCaller
    {

        [SerializeField]
        private List<AvailableActionItem> items;

        private void OnValidate()
        {
            for (int i = 0; i < items.Count; i++)
                items[i].execComponent = items[i].Executable as Component;
        }

        protected IExecutable findAction(string name)
        {
            int index = indexOf(name);
            if (index > -1)
            {
                if (items[index].Executable != null)
                    return items[index].Executable;
                else Debug.LogError("Action must not be null " + items[index].name);
            }
            return null;
        }

        public int indexOf(string actionName)
        {
            for (int i = 0; i < items.Count; i++)
                if (items[i].name.Equals(actionName))
                    return i;

            return -1;
        }

        public void Call(string actionName)
        {
            IExecutable exec = findAction(actionName);
            if (exec != null) exec.Execute();
        }

        public bool GetAvailable(string actionName)
        {
            IExecutable exec = findAction(actionName);
            if (exec != null) 
                return !exec.getPerformed();

            return false;
        }
    }
}
