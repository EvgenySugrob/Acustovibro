using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vmaya;

namespace TaskAction
{
    public class ElementAvailableProvider : MonoBehaviour, IAvailableProvider
    {
        [System.Serializable]
        public class availableItem
        {
            public string action;
            public Component element;
            public bool invert;
        }

        public List<availableItem> Items;

        private void OnValidate()
        {
            for (int i = 0; i < Items.Count; i++)
                if (Items[i].element)
                    Items[i].element = Items[i].element.GetComponent<IExecutable>() as Component;
        }

        public bool GetAvailable(string activity)
        {
            for (int i = 0; i < Items.Count; i++)
                if (Items[i].element && Items[i].action.Equals(activity))
                {
                    IExecutable executable = Items[i].element.GetComponent<IExecutable>();
                    return Items[i].invert ? !executable.getPerformed() : executable.getPerformed();
                }

            return true;
        }
    }
}
