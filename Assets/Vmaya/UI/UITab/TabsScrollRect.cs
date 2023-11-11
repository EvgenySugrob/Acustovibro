using Vmaya.UI.UIBlocks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Vmaya.UI.UITabs
{
    public class TabsScrollRect : ScrollRect
    {
        private TabsPanel _panel => GetComponentInParent<TabsPanel>();
        private TabsScrollButtons _scrollTabsLayer => GetComponent<TabsScrollButtons>();

        private bool _isHold;

        protected override void Awake()
        {
            base.Awake();
            if (_panel) _panel.onDetach.AddListener(onDetach);
        }

        private void onDetach(UIBPanel value)
        {
            TabsHorisontalGroup thg = _panel.GetComponent<TabsHorisontalGroup>();
            if (thg && _isHold) thg.refreshLayers();
        }

        public override void OnBeginDrag(PointerEventData eventData)
        {
            base.OnBeginDrag(eventData);

            TabsHorisontalGroup thg = _panel.GetComponent<TabsHorisontalGroup>();

            if (thg && _panel && _panel.DropBox && _scrollTabsLayer && !_scrollTabsLayer.isRequireScroll)
            {
                Debug.Log("BeginDrag");
                _panel.OnPointerDown(transform.position);
                _panel.RecentlyDetach = true;

                _isHold = true;
            }
        }

        public override void OnDrag(PointerEventData eventData)
        {
            base.OnDrag(eventData);
            if (_isHold) _panel.OnDrag(eventData);
        }

        public override void OnEndDrag(PointerEventData eventData)
        {
            base.OnEndDrag(eventData);
            if (_isHold)
            {
                _panel.DoEndDrag();
                _isHold = false;
            }
        }
    }
}
