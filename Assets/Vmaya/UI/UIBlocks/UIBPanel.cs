using Vmaya.UI.UIBlocks.RW;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System;
using UnityEngine.InputSystem;

namespace Vmaya.UI.UIBlocks
{
    public class UIBPanel : RWComponent
    {
        public bool checkMinSize = false;
        [SerializeField]
        protected RectTransform _contentConteiner;
        public Vector2 fixSize => getFixSize();

        public RectTransform contentContainer => _contentConteiner;
        public RectTransform content => _contentConteiner && (_contentConteiner.childCount > 0) ? _contentConteiner.GetChild(0).GetComponent<RectTransform>() : null;

        [System.Serializable]
        public class UIPanelEvent : UnityEvent<UIBPanel> { };
        public UIPanelEvent onDrop;
        public UIPanelEvent onDetach;

        public float recentlyDetachTimeout = 0;
        private bool _recentlyDetach = false;
        public bool RecentlyDetach { get => _recentlyDetach; set => setRecentlyDetach(value); }

        private int _drag;
        private Vector2 _pivot;
        private Vector2 _prevPivot;

        private Vector2 _safeSize;
        public Vector2 SafeSize => _safeSize;
        public UIBDropBox PlaceDropBox => DropBox ? DropBox.DropBox : null;

        public UIBResizePanel resizePanel => GetComponent<UIBResizePanel>();

        public UIBTitle title => GetComponentInChildren<UIBTitle>(true);

        private int _dropRectIndex;
        private UIBDropBoxBase _dropBox;

        public bool isDrag => _drag == 2;

        private void Awake()
        {
            updateSafeSize();
            Trans.anchorMin = Trans.anchorMax = new Vector2(0.5f, 0.5f);
            Trans.sizeDelta = _safeSize;

            if (resizePanel)
                resizePanel.onResize.AddListener(onResize);
        }

        public void updateSafeSize()
        {
            _safeSize = Trans.rect.size;
        }

        internal bool isAllowedDropping()
        {
            return !_recentlyDetach && !VKeyboard.GetKey(Key.LeftShift);
        }

        private void setRecentlyDetach(bool value)
        {
            if (_recentlyDetach = value)
            {
                Vmaya.Utils.setTimeout(this, () =>
                {
                    _recentlyDetach = false;
                }, recentlyDetachTimeout);
            }
        }

        private void onResize(Rect inc)
        {
            _safeSize = Trans.rect.size;
        }

        virtual public void OnPointerDown(Vector2 position)
        {
            if (isAllowDrag())
            {
                Trans.SetAsLastSibling();
                Vector2 result;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(Trans, position, null, out result);

                _prevPivot = Trans.pivot;
                _pivot = Trans.pivot + new Vector2(result.x / Trans.rect.size.x, result.y / Trans.rect.size.y);
                _drag = 1;
            }
        }

        virtual public bool isAllowDrag()
        {
            return VMouse.GetMouseButton(0);
        }

        virtual protected void doBeginDrag()
        {
            Manager.beginDrag(this);
            if (PlaceDropBox) Detach();
            else updateSafeSize();
        }

        public void Detach()
        {
            UIBDropBox PlaceDB = PlaceDropBox;

            if (resizePanel) resizePanel.enabled = true;

            Trans.anchorMin = Trans.anchorMax = new Vector2(0.5f, 0.5f);
            Trans.sizeDelta = SafeSize;
            Trans.SetParent(Manager.transform);

            if (recentlyDetachTimeout > 0) RecentlyDetach = true;
            PlaceDB.resetChildBoxes();
            onDetach.Invoke(this);
        }

        private void OnEnable()
        {
            Trans.SetAsLastSibling();
        }

        private void OnDisable()
        {
            UIBDropBox PlaceDB = PlaceDropBox;
            if (PlaceDB && PlaceDB.gameObject.activeInHierarchy)
                Vmaya.Utils.setTimeout(PlaceDB, PlaceDB.resetChildBoxes, Consts.MIN_TIMEOUT);
        }

        virtual protected void doDrag(Vector2 point)
        {
            Rect rect = default;
            _dropBox = Manager.checkHitPanel(point, this, out rect, out _dropRectIndex);
            if (_dropBox)
            {
                Trans.pivot = new Vector2(0.5f, 0.5f);
                rect.position = (Vector2)_dropBox.Trans.TransformPoint(rect.position);
                SetWorldRect(rect);
            }
            else
            {
                Trans.anchorMin = Trans.anchorMax = new Vector2(0.5f, 0.5f);
                Trans.sizeDelta = SafeSize;
            }
        }

        public virtual void DoEndDrag()
        {

            Vector2 localPointerPosition;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(Trans.parent.GetComponent<RectTransform>(), Trans.position, null, out localPointerPosition);

            Vector3 delta       = _prevPivot - _pivot;
            Trans.pivot         = _prevPivot;
            //Trans.sizeDelta     = SafeSize;
            Trans.localPosition = localPointerPosition + new Vector2(delta.x * Trans.rect.size.x, delta.y * Trans.rect.size.y);
            if (_dropBox)
            {
                if (_dropBox.Dropping(this, _dropRectIndex)) onDrop.Invoke(this);

                UIBManager m = Manager;
                Vmaya.Utils.setTimeout(m, () => { m.endDrag(this); }, 0.2f);
            } else Manager.endDrag(this);
        }

        virtual public void OnDrag(PointerEventData data)
        {
            if (_drag == 1)
            {
                _drag = 2;
                doBeginDrag();
            }
            Vector2 localPointerPosition;

            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                Trans.parent.GetComponent<RectTransform>(), data.position, null, out localPointerPosition
            ))
            {
                Vector3 delta = localPointerPosition - (Vector2)Trans.localPosition;

                Trans.pivot = _pivot;
                if (Manager.Restrict) 
                    delta = Utils.ClampDelta(localPointerPosition - (Vector2)Trans.localPosition, Trans, Manager.Trans);

                Trans.localPosition += delta;
                doDrag(data.position);
            }
        }

        public void OnPointerUp(PointerEventData data)
        {
            if (_drag == 2)
            {
                _drag = 0;
                DoEndDrag();
            }
            else _drag = 0;
        }

        public override bool checkBlockRect(int index, Rect rect)
        {
            if (checkMinSize)
            {
                Vector2 minSize = limitSize();
                return (rect.width >= minSize.x) && (rect.height >= minSize.y);
            }

            return base.checkBlockRect(index, rect);
        }

        public override Vector3 limitSize()
        {
            if (!fixSize.Equals(Vector2.zero))
                return fixSize;

            if (resizePanel)
                return new Vector2(Mathf.Min(resizePanel.MinSize.x, Manager.defaultLimitSize.x), Mathf.Min(resizePanel.MinSize.y, Manager.defaultLimitSize.y));
            else return Manager.defaultLimitSize;
        }

        protected override void setJsonData(string jsonData)
        {
            if (!DropBox &&!string.IsNullOrEmpty(jsonData))
            {
                PanelData data = JsonUtility.FromJson<PanelData>(jsonData);
                if (data.rect.size.sqrMagnitude > 0)
                    SetWorldRect(data.rect);
            }
            base.setJsonData(jsonData);
        }

        protected virtual Vector2 getFixSize()
        {
            return Vector2.zero;
        }
    }
}