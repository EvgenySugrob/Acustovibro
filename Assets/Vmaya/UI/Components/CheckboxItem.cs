using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Vmaya.UI
{
    [RequireComponent(typeof(Toggle))]
    public class CheckboxItem : MonoBehaviour
    {
        [System.Serializable]
        public class CBEvent : UnityEvent<bool, CheckboxItem>
        {
        }

        private int index;

        [HideInInspector]
        public CBEvent onCBChangeValue;

        private CheckboxList list;

        public CheckboxList List
        {
            get
            {
                return list;
            }
        }

        public int Index
        {
            get
            {
                return index;
            }
        }

        private void Start()
        {
            GetComponent<Toggle>().onValueChanged.AddListener(onValueChanged);
        }

        public void initialize(CheckboxList a_list, int a_index, string title)
        {
            index = a_index;
            list = a_list;
            Vmaya.Utils.setText(this, title);
            onCBChangeValue = new CBEvent();
            gameObject.SetActive(true);
        }

        private void onValueChanged(bool value)
        {
            onCBChangeValue.Invoke(value, this);
        }
    }
}