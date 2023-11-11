using UnityEngine;
using UnityEngine.Events;

namespace Vmaya.Collections.Utils
{
    public class CheckEmptyList : MonoBehaviour
    {

        [System.Serializable]
        public class OnChangeSource: UnityEvent<bool> { }

        public OnChangeSource onChangeList;
        protected IListSource _list => GetComponent<IListSource>();

        private void Start()
        {
            if (_list != null) _list.onAfterChange(OnChangeList);
        }

        private void OnChangeList()
        {
            onChangeList.Invoke(_list.getCount() == 0);
        }
    }
}
