using UnityEngine;
using Vmaya.UI.UIBlocks;
using UnityEngine.UI;

namespace Vmaya.UI.UITabs
{
    public class DropToTabs : UIBDropBoxBase
    {
        [SerializeField]
        private TabsGroup _group;

        private void Awake()
        {
            if (!_group) _group = GetComponent<TabsGroup>();

            if (Manager)
            {
                Manager.onBeginDrag.AddListener(onBeginDrag);
                Manager.onEndDrag.AddListener(onEndDrag);
            }
        }

        private void setAccessDropping(UIBPanel panel, bool value)
        {
            if (!_group.showMe) {
                UIBPanel my_panel = GetComponentInParent<UIBPanel>();
                if (my_panel && (panel != my_panel))
                    _group.scrollRect.gameObject.SetActive(value);
            }
        }

        private void onBeginDrag(UIBPanel panel)
        {
            setAccessDropping(panel, true);
        }

        private void onEndDrag(UIBPanel panel)
        {
            setAccessDropping(panel, false);
        }

        public override bool Dropping(UIBPanel a_panel, int index)
        {
            if (a_panel.content)
            {

                TabsGroup tg = a_panel.contentContainer.GetComponent<TabsGroup>();
                if (tg) _group.moveTabs(tg);
                else
                {
                    TabContent tc = a_panel.content.GetComponentInChildren<TabContent>();
                    _group.addTab(tc ? tc.title : a_panel.title.GetText(), tc);
                }

                Destroy(a_panel.gameObject);
            }
            return false;
        }

        public override bool hitPlaceRect(Vector2 point, UIBComponent component, out Rect rect, out int index)
        {
            Vector2 hitPoint;
            index = -1;
            rect = default;

            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(Trans, point, null, out hitPoint) &&
               Trans.rect.Contains(hitPoint)) {
                UIBPanel a_panel = component.GetComponent<UIBPanel>();
                if (a_panel.content)
                {
                    TabsGroup tg = a_panel.contentContainer.GetComponent<TabsGroup>();
                    if (tg && (tg.items.Length == 0))
                        return false;
                }
                ScrollRect scroll = _group.contentLayer.GetComponentInParent<ScrollRect>();
                RectTransform trans = scroll ? scroll.GetComponent<RectTransform>() : _group.contentLayer;
                rect    = trans.rect;
                rect.position = transform.InverseTransformPoint(trans.TransformPoint(rect.position));
                return true;
            }
            return false;
        }
    }
}