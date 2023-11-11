using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Vmaya.Language;
using Vmaya.Scene3D;

namespace Vmaya.UI.UIBlocks
{
    public class UIBTitle : UIBComponent, IPointerUpHandler, IPointerDownHandler, IDragHandler
    {
        protected UIBPanel Panel => GetComponentInParent<UIBPanel>();
        protected RectTransform PanelRect => Panel.GetComponent<RectTransform>();

        [SerializeField]
        private Component _text;

        private static bool firstDrag = true;

        private void OnValidate()
        {
            if (!Panel && gameObject.activeInHierarchy) Debug.LogWarning("It must be child for UIBPanel component");
            if (!_text) _text = GetComponent<Text>();
        }

        public void SetText(string value)
        {
            Vmaya.Utils.setText(_text, value);
        }

        virtual public void OnPointerDown(PointerEventData data)
        {
            Panel.OnPointerDown(VMouse.mousePosition);
        }

        virtual public void OnDrag(PointerEventData data)
        {
            Panel.OnDrag(data);
            if (firstDrag)
            {
                rightToast.setText(Lang.instance["Use Left Shift to disable magnetization"]);
                firstDrag = false;
            }
        }

        public void OnPointerUp(PointerEventData data)
        {
            Panel.OnPointerUp(data);
        }

        internal string GetText()
        {
            return Vmaya.Utils.getText(_text);
        }
    }
}