using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Vmaya.UI
{
    public class DropdownListFrame : MonoBehaviour
    {
        [System.Serializable]
        public enum OpeningDirection { Auto, Top, Bottom };

        [SerializeField]
        private OpeningDirection _openingDirection;

        [SerializeField]
        private RectTransform _frame;
        public RectTransform Frame => _frame;

        [SerializeField]
        private TMP_Dropdown _dropdownTop;

        [SerializeField]
        private float _space;

        private static Dictionary<TMP_Dropdown, Transform> _prevParent = new Dictionary<TMP_Dropdown, Transform>();

        private Canvas _topCanvas => (transform.root != transform) ? transform.root.GetComponent<Canvas>() : null;

        private RectTransformAnim _ranim => Frame.GetComponent<RectTransformAnim>();

        private void OnEnable()
        {
            if (_topCanvas && !_prevParent.ContainsKey(_dropdownTop))
            {
                _prevParent[_dropdownTop] = _dropdownTop.transform.parent;
                UpdateFrame();
                _dropdownTop.transform.parent = _topCanvas.transform;
            }
        }

        private void OnDestroy()
        {
            if (_prevParent.ContainsKey(_dropdownTop))
            {
                Vmaya.Utils.setTimeout(_dropdownTop.GetComponent<MonoBehaviour>(), () =>
                {
                    _dropdownTop.transform.parent = _prevParent[_dropdownTop];
                    _prevParent.Remove(_dropdownTop);
                }, _ranim ? _ranim.Duration : 0.1f);
            }
        }

        private void OnDisable()
        {
            UpdateFrame();
        }

        private void UpdateFrame()
        {
            float h;
            bool toOpen = enabled;
            RectTransform Trans = GetComponent<RectTransform>();

            float th = Trans.rect.height + _space;
            RectTransform sdtRect = _dropdownTop.GetComponent<RectTransform>();
            Rect dt_rect = UIComponent.GetScreenCoordinatesTrans(sdtRect);

            bool top = _openingDirection == OpeningDirection.Top;

            if (_openingDirection == OpeningDirection.Auto)
                top = dt_rect.yMin - th < 0;

            if (_ranim)
            {
                Rect op_rect = new Rect(dt_rect.x, dt_rect.y, dt_rect.width, dt_rect.height + th);
                if (top) op_rect.y += th;
                if (toOpen) _ranim.PlayTo(dt_rect, op_rect);
                else _ranim.PlayTo(op_rect, dt_rect);
            }
            else
            {
                h = toOpen ? th : 0;
                Vector2 pos = new Vector2(sdtRect.position.x, sdtRect.position.y + (enabled && top ? h : 0));
                _frame.sizeDelta = new Vector2(0, h);
                _frame.position = pos;
            }
        }
    }
}
