using UnityEngine;
using UnityEngine.UI;

namespace Vmaya.UI.Tools
{
    public class ToolToggleText : ToolToggle
    {
        [SerializeField]
        private Text _caption;

        [SerializeField]
        private Text _number;

        internal override void resetItem(int number)
        {
            base.resetItem(number);
            _item.title = _caption.text;
            resetNumber();
        }

        protected override void resetNumber()
        {
            if ((_item.number != 0) && (useKeyboard > useKeyboardType.None)) _number.text = _item.number.ToString();
            else _number.transform.parent.gameObject.SetActive(false);
        }

        protected override void resetTexts()
        {
            _caption.text = item.title;
        }
    }
}