using System;
using System.Collections.Generic;
using UnityEngine;

namespace Vmaya.UI.UIBlocks
{
    public class UIBDropBox : UIBDropBoxBase
    {

        private DivideType _divided = DivideType.None;
        public DivideType Divided => _divided;

        private MagnetType _magnetType;
        public MagnetType magnetType => _magnetType;

        private float _divOffset;
        public float DivOffset;

        protected UIBDropBox _edgeBox;
        public UIBDropBox EdgeBox => _edgeBox;

        protected UIBDropBox _otherBox;
        public UIBDropBox OtherBox => _otherBox;

        protected UIBSeparator _separator;
        public UIBSeparator Separator => _separator;

        public bool isEmpty => GetComponentInChildren<UIBPanel>() == null;

        private int _boxIndexRect;

        public Vector2 magnetArea;

        private void OnValidate()
        {
            if (GetComponent<UIBManager>())
                Debug.LogError("UIBDropBox must be a child of UIBManager");
        }

        protected void destroyChildBoxes()
        {
            if (_otherBox && _edgeBox)
            {
                Destroy(_edgeBox.gameObject);
                Destroy(_otherBox.gameObject);
                Destroy(_separator.gameObject);
                _divided = DivideType.None;
                _edgeBox = null;
                _otherBox = null;
                _separator = null;
            }
        }

        private bool isChange()
        {
            return DivOffset != _divOffset;
        }

        protected void contentMoveTo(UIBDropBox place)
        {

            GameObject eb = place._edgeBox.gameObject;
            GameObject fb = place._otherBox.gameObject;
            GameObject sp = place._separator.gameObject;

            EdgeBox.Trans.SetParent(place.Trans);
            OtherBox.Trans.SetParent(place.Trans);
            Separator.Trans.SetParent(place.Trans);

            place._edgeBox      = _edgeBox;
            place._otherBox     = _otherBox;
            place._separator    = _separator;
            place._boxIndexRect = _boxIndexRect;

            place._divided      = _divided;
            place._magnetType   = _magnetType;

            Destroy(eb);
            Destroy(fb);
            Destroy(sp);

            place.refreshAnchors();
        }

        public void Clear()
        {
            destroyChildBoxes();
        }

        public void resetChildBoxes()
        {
            UIBPanel[] panels = GetComponentsInChildren<UIBPanel>();
            if (panels.Length == 0)
                destroyChildBoxes();

            else if (Divided != DivideType.None) {

                UIBDropBox db;
                if (EdgeBox.GetComponentsInChildren<UIBPanel>().Length > 0) db = EdgeBox;
                else if (OtherBox.GetComponentsInChildren<UIBPanel>().Length > 0) db = OtherBox;
                else return;

                if (db.Divided != DivideType.None)
                    db.contentMoveTo(this);
                else if (panels.Length == 1)
                {
                    UIBPanel panel = panels[0];
                    panel.Trans.SetParent(Trans);
                    setAnchor(panel.Trans, MagnetType.Full, Trans.rect);
                    destroyChildBoxes();
                    Debug.Log("Full one panel");
                } else
                {
                    Debug.Log("More than one panel (" + panels.Length + ")");
                }
            }
        }

        private float revealIncOffset(UIBDropBox childBox, Rect inc)
        {
            switch (magnetType)
            {
                case MagnetType.Top:
                    return _edgeBox == childBox ? -inc.height : inc.height;
                case MagnetType.Right:
                    return _edgeBox == childBox ? -inc.width : inc.width;
                case MagnetType.Bottom:
                    return _edgeBox == childBox ? inc.height : -inc.height;
                case MagnetType.Left:
                    return _edgeBox == childBox ? inc.width : -inc.width;
            }
            return 0;
        }

        public void setDivOffset(float v)
        {
            DivOffset = v;
            Refresh();
        }

        public override Vector3 limitSize()
        {
            Vector3 result = base.limitSize();
            if (Divided != DivideType.None)
            {
                List<UIBComponent> list = getChildren<UIBComponent>();
                foreach (UIBComponent child in list)
                {
                    Vector2 ls = child.limitSize();
                    if (Divided == DivideType.Horisontal)
                    {
                        result.x += ls.x;
                        result.y = Mathf.Max(ls.y, result.y);
                    }
                    else if (Divided == DivideType.Vertical)
                    {
                        result.y += ls.y;
                        result.x = Mathf.Max(ls.x, result.x);
                    } else result += child.limitSize();
                }
            }
            else
            {
                UIBPanel panel = findChildPanel();
                if (panel)
                    result = panel.limitSize();
            }
            return result;
        }

        public virtual float[] limitCompression()
        {
            float[] result = new float[4] { 0, 0, 0, 0 };

            if (Divided != DivideType.None)
            {
                int idx1 = (int)_magnetType;
                int idx2 = (idx1 + 2) % 4;

                Vector2 els = _edgeBox.limitSize();
                Vector2 efs = _otherBox.limitSize();

                result[idx1] = vtof(els);
                result[idx2] = vtof(efs);
            }

            return result;
        }

        protected virtual UIBComponent createChild(UIBComponent template)
        {
            UIBComponent result = Instantiate(template, Trans);
            result.gameObject.name = Utils.UniqueName(template.GetType().Name);
            return result;
        }

        private void Update()
        {
            if (isChange() && (Divided != DivideType.None))
            {
                Refresh();
            }
        }

        public Vector2 ftov(float v)
        {
            return Divided == DivideType.Horisontal ? new Vector2(v, 0) : new Vector2(0, v);
        }

        public float vtof(Vector2 v)
        {
            return Divided == DivideType.Horisontal ? v.x : v.y;
        }

        private void checkBoxSizeCorrect()
        {
            if (Divided != DivideType.None)
            {
                float diff = vtof(_otherBox.Trans.rect.size) - vtof(_otherBox.limitSize());
                if (diff < 0) setDivOffset(DivOffset + diff);

                _edgeBox.checkBoxSizeCorrect();
                _otherBox.checkBoxSizeCorrect();
            }
        }

        private void Refresh()
        {
            void setPositionDelta(UIBDropBox box, Vector2 pos, Vector2 sizeDelta)
            {
                box.Trans.sizeDelta = sizeDelta;
                box.Trans.position  = pos;
                box.checkBoxSizeCorrect();
            }

            float inc = DivOffset - _divOffset;
            float[] minSizeA = limitCompression();

            int idx1 = (int)_magnetType;
            int idx2 = (idx1 + 2) % 4;

            float minSize = minSizeA[idx1];
            float maxSize = vtof(Trans.rect.size) - minSizeA[idx2];

            if (_magnetType == MagnetType.Left)
            {
                inc = Mathf.Min(Mathf.Max(_edgeBox.Trans.sizeDelta.x + inc, minSize), maxSize) - _edgeBox.Trans.sizeDelta.x;

                setPositionDelta(_edgeBox, new Vector3(_edgeBox.Trans.position.x + inc / 2f, _edgeBox.Trans.position.y),
                                            new Vector3(_edgeBox.Trans.sizeDelta.x + inc, _edgeBox.Trans.sizeDelta.y));
                setPositionDelta(_otherBox, new Vector3(_otherBox.Trans.position.x + inc / 2f, _otherBox.Trans.position.y),
                                            new Vector3(_otherBox.Trans.sizeDelta.x - inc, _otherBox.Trans.sizeDelta.y));
            }
            else if (_magnetType == MagnetType.Top)
            {
                inc = _edgeBox.Trans.sizeDelta.y - Mathf.Min(Mathf.Max(_edgeBox.Trans.sizeDelta.y - inc, minSize), maxSize);

                setPositionDelta(_edgeBox, new Vector3(_edgeBox.Trans.position.x, _edgeBox.Trans.position.y + inc / 2f),
                                            new Vector3(_edgeBox.Trans.sizeDelta.x, _edgeBox.Trans.sizeDelta.y - inc));
                setPositionDelta(_otherBox, new Vector3(_otherBox.Trans.position.x, _otherBox.Trans.position.y + inc / 2f),
                                            new Vector3(_otherBox.Trans.sizeDelta.x, _otherBox.Trans.sizeDelta.y + inc));
            }
            else if (_magnetType == MagnetType.Right)
            {
                
                inc = _edgeBox.Trans.sizeDelta.x - Mathf.Min(Mathf.Max(_edgeBox.Trans.sizeDelta.x - inc, minSize), maxSize);

                setPositionDelta(_edgeBox, new Vector3(_edgeBox.Trans.position.x + inc / 2f, _edgeBox.Trans.position.y),
                                            new Vector3(_edgeBox.Trans.sizeDelta.x - inc, _edgeBox.Trans.sizeDelta.y));
                setPositionDelta(_otherBox, new Vector3(_otherBox.Trans.position.x + inc / 2f, _otherBox.Trans.position.y),
                                            new Vector3(_otherBox.Trans.sizeDelta.x + inc, _otherBox.Trans.sizeDelta.y));
            }
            else if (_magnetType == MagnetType.Bottom)
            {
                inc = Mathf.Min(Mathf.Max(_edgeBox.Trans.sizeDelta.y + inc, minSize), maxSize) - _edgeBox.Trans.sizeDelta.y;

                setPositionDelta(_edgeBox, new Vector3(_edgeBox.Trans.position.x, _edgeBox.Trans.position.y + inc / 2f),
                                            new Vector3(_edgeBox.Trans.sizeDelta.x, _edgeBox.Trans.sizeDelta.y + inc));
                setPositionDelta(_otherBox, new Vector3(_otherBox.Trans.position.x, _otherBox.Trans.position.y + inc / 2f),
                                            new Vector3(_otherBox.Trans.sizeDelta.x, _otherBox.Trans.sizeDelta.y - inc));
            }

            _divOffset = DivOffset = _divOffset += inc;

            if (inc != 0)
                Manager.refreshSeparators();
        }

        protected void refreshAnchors(float w = 0, float h = 0)
        {

            if (_edgeBox.findChildPanel())
            {
                w = _edgeBox.Trans.rect.width;
                h = _edgeBox.Trans.rect.height;
            }

            Rect rect = getBlockRects(w, h)[_boxIndexRect];
            setAnchor(_edgeBox.Trans, _magnetType, rect);
            setAnchorSpace(_otherBox.Trans, _magnetType, rect);
            Separator.Refresh();
        }

        public void Div(MagnetType a_magnetType, float w = 0, float h = 0)
        {

            _boxIndexRect = (int)a_magnetType;

            _magnetType = a_magnetType;
            _divided = (_magnetType == MagnetType.Left) || (_magnetType == MagnetType.Right) ? DivideType.Horisontal : DivideType.Vertical;

            _divOffset = DivOffset = 0;/* (_magnetType == MagnetType.Left ? rect.xMax :
                                    (_magnetType == MagnetType.Top ? rect.yMin :
                                    (_magnetType == MagnetType.Right ? rect.xMin :
                                        rect.yMax)));*/

            _edgeBox = createChild(Manager.dropBoxTemplate) as UIBDropBox;
            _otherBox = createChild(Manager.dropBoxTemplate) as UIBDropBox;
            _separator = createChild(Manager.separatorTemplate) as UIBSeparator;

            refreshAnchors(w, h);
        }

        public override bool Dropping(UIBPanel a_panel, int index)
        {
            if (index > -1)
            {
                List<Rect> rects = getBlockRects(a_panel.fixSize.x, a_panel.fixSize.y);
                Rect rect = rects[index];

                if ((Divided == DivideType.None) && a_panel.checkBlockRect(index, rect))
                {
                    _divided = (index == 0) || (index == 2) ? DivideType.Horisontal : DivideType.Vertical;

                    UIBPanel childPanel = findChildPanel();
                    Div((MagnetType)index, rect.width, rect.height);

                    a_panel.Trans.pivot = new Vector2(0.5f, 0.5f);
                    a_panel.Trans.SetParent(_edgeBox.Trans);
                    a_panel.fullSpace();

                    if (a_panel.resizePanel) a_panel.resizePanel.enabled = false;

                    if (childPanel && (childPanel != a_panel))
                    {
                        childPanel.Trans.SetParent(_otherBox.transform);
                        childPanel.fullSpace();
                    }

                    return true;
                }
            }

            return false;
        }

        public UIBPanel getPanel()
        {
            UIBPanel result = null;

            for (int i = 0; i < Trans.childCount; i++)
                if (result = Trans.GetChild(i).GetComponent<UIBPanel>()) break;

            return result;
        }

        protected override List<Rect> getBlockHitRects()
        {
            return getBlockRects(magnetArea.x, magnetArea.y);
        }

        public override bool hitPlaceRect(Vector2 point, UIBComponent component, out Rect rect, out int index)
        {
            Vector2 hitPoint;
            rect = default;
            index = -1;
            float minSize = float.MaxValue;

            if ((Divided == DivideType.None) && 
                RectTransformUtility.ScreenPointToLocalPointInRectangle(Trans, point, null, out hitPoint)) {

                Vector2 fix = component is UIBPanel ? (component as UIBPanel).fixSize : Vector2.zero;
                List<Rect> hitRects = getBlockHitRects();
                List<Rect> rects = getBlockRects(fix.x, fix.y);

                for (int i = 0; i < hitRects.Count; i++) {
                    Rect cur = hitRects[i];
                    if ((minSize > cur.size.sqrMagnitude) &&
                        cur.Contains(hitPoint) &&
                        component.checkBlockRect(index, rects[i]))
                    {
                        index   = i;
                        minSize = cur.size.sqrMagnitude;
                        rect    = rects[i];
                    }
                }
            }
            return index > -1;
        }

        private UIBPanel findChildPanel()
        {
            List<UIBPanel> result = getChildren<UIBPanel>();
            return result.Count > 0 ? result[0] : null;
        }
    }
}