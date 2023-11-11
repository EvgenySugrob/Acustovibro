using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vmaya.Collections;

namespace Vmaya.UI
{
    [RequireComponent(typeof(Dropdown))]
    public class DropdownSourceList : MonoBehaviour
    {
        public Component source;
        private IListSource _source;
        public IListSource Source { get { return _source; } }
        private Dropdown list;

        private void OnValidate()
        {
            checkSource();
        }

        private void checkSource()
        {
            _source = source ? source.GetComponent<IListSource>() : null;
            source = _source as Component;
        }

        private void Awake()
        {
            checkSource();
            list = GetComponent<Dropdown>();
            list.onValueChanged.AddListener(onChange);
            if (_source != null) _source.onAfterChange(onLoaded);
        }

        private void onLoaded()
        {
            List<string> doptions = new List<string>();

            int count = _source.getCount();
            for (int i=0; i<count; i++)
                doptions.Add(_source.getName(i));

            list.AddOptions(doptions);
            onChange(list.value);
        }

        virtual protected void onChange(int index)
        {
        }
    }
}