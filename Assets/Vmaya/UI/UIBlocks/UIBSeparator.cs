using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Vmaya.UI.UIBlocks
{
    public class UIBSeparator : UIBComponent, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler
    {
        [SerializeField]
        private float _size = 5;

        private Vector3 _prevPos;
        private bool _pressed;
        private bool _isChange;

        private void Start()
        {
            Refresh();
        }

        public void Refresh()
        {
            if (DropBox)
            {
                float offset;
                float s2 = _size / 2;
                Rect rect = DropBox.Trans.rect;
                if (DropBox.Divided == DivideType.Horisontal)
                {
                    rect.width = _size;
                    offset = DropBox.EdgeBox.Trans.rect.width - DropBox.Trans.rect.width / 2;
                    rect.x = (DropBox.magnetType == MagnetType.Left ? offset : -offset) - s2; //DropBox.DivOffset;
                }
                else
                {
                    rect.height = _size;
                    offset = DropBox.EdgeBox.Trans.rect.height - DropBox.Trans.rect.height / 2;
                    rect.y = (DropBox.magnetType == MagnetType.Top ? s2 - offset : offset - s2) - s2; //DropBox.DivOffset;
                }
                setAnchor(Trans, DropBox.magnetType, rect);
            }
        }

        private bool isAllowedResize => getIsAllowedResize();

        private bool getIsAllowedResize()
        {
            UIBPanel panel = DropBox.EdgeBox.getPanel();
            return !panel || panel.fixSize.sqrMagnitude == 0;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _prevPos = VMouse.mousePosition;
            _pressed = true;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (isAllowedResize) setCursor(DropBox.Divided == DivideType.Horisontal ? "ht_hory" : "ht_vert");
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (isAllowedResize && !_pressed) setCursor(null);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            setCursor(null);
            _pressed = false;
            if (_isChange) Manager.onManualChangeBoxes.Invoke();
            _isChange = false;
        }

        private void Update()
        {
            if (_pressed)
            {
                if (VMouse.GetMouseButton(0))
                {
                    Vector2 delta = VMouse.mousePosition - _prevPos;

                    if (delta.sqrMagnitude > 0)
                    {
                        if (DropBox.Divided == DivideType.Horisontal) DropBox.setDivOffset(DropBox.DivOffset + delta.x);
                        else DropBox.setDivOffset(DropBox.DivOffset + delta.y);

                        _prevPos = VMouse.mousePosition;
                        _isChange = true;
                    }
                }
                else OnPointerUp(default);
            }
        }
    }
}