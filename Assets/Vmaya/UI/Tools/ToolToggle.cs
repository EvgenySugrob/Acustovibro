using UnityEngine;
using UnityEngine.UI;
using Vmaya.Language;

namespace Vmaya.UI.Tools
{
    [RequireComponent(typeof(Toggle))]
    public abstract class ToolToggle : UIComponent
    {
        protected ToolItem _item;
        public ToolItem item { get => _item; set => setItem(value); }

        [System.Serializable]
        public enum useKeyboardType { None, OnlyNumebers, WithLeftAlt, WithLeftShift, WithRightAlt, WithRightShift, F };

        public useKeyboardType useKeyboard;

        [SerializeField]
        private Image _image;

        public Toggle toggle { get { return GetComponent<Toggle>(); } }

        private void Start()
        {
            toggle.onValueChanged.AddListener(onChange);
        }

        private void setItem(ToolItem value)
        {
            _item = value;
            _image.sprite = value.sprite;
            toggle.group = GetComponentInParent<ToggleGroup>();
            resetTexts();
            resetNumber();
        }

        internal virtual void resetItem(int number)
        {
            _item = new ToolItem(Lang.instance[name], _image.sprite);
            _item.number = number;
            toggle.group = GetComponentInParent<ToggleGroup>();
        }


        abstract protected void resetNumber();
        abstract protected void resetTexts();

        /*private void Update()
        {
            if (VKeyboard.anyKey) ToolKeySupport.checkKeyDown(this);
        }*/

        private void onChange(bool value)
        {
            
            if (value) _item.action(value);
            else Vmaya.Utils.setTimeout(transform.root.GetComponent<MonoBehaviour>(), ()=> {
                    _item.action(false);
                }, 0.1f);
        }
    }
}