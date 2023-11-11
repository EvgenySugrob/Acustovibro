using TMPro;
using UnityEngine;

namespace Vmaya.UI.Tools
{
    public class ToolToggleTMPro : ToolToggle
    {
        [SerializeField]
        private TextMeshProUGUI _caption;

        [SerializeField]
        private TextMeshProUGUI _number;

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