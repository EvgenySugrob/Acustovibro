using Vmaya.UI.UIBlocks;
using UnityEngine;

namespace Vmaya.UI.UITabs
{
    public class TabsHorisontalGroup : UIComponent
    {
        [SerializeField]
        private UIBPanel _panel;

        [SerializeField]
        private RectTransform _titleLayer;

        [SerializeField]
        private TabsGroup _tabs;

        [SerializeField]
        private RectTransform _contentPanel;

        [SerializeField]
        private float _space;

        private void Awake()
        {
            _tabs.onTabChange.AddListener(onTabChange);
            _panel.onDrop.AddListener(onDrop);
        }

        private void onDrop(UIBPanel panel)
        {
            refreshLayers();
        }

        public void refreshLayers()
        {
            //_tabHidingPanel.gameObject.SetActive(isVisibleTabs);

            TabItem[] items = _tabs.items;

            bool showTitle = (_panel.Trans.parent.GetComponent<UIBDropBox>() == null) || !_tabs.showMe;

            RectTransform titleRect = _titleLayer.GetComponent<RectTransform>();
            titleRect.gameObject.SetActive(showTitle);
            titleRect.anchoredPosition = new Vector2(0, -_space);
            titleRect.sizeDelta = new Vector2(-_space * 2, titleRect.sizeDelta.y);

            float h = _space + (showTitle ? titleRect.rect.height : 0);

            _tabs.Trans.anchoredPosition = new Vector2(0, _tabs.showMe ? - h : -_space);
            _tabs.Trans.sizeDelta = new Vector2(-_space * 2, _tabs.Trans.sizeDelta.y);
            _tabs.scrollRect.gameObject.SetActive(_tabs.showMe);

            h += _tabs.showMe ? _tabs.Trans.rect.height : 0;

            foreach (TabItem item in items)
                item.gameObject.SetActive(_tabs.showMe);

            _contentPanel.anchoredPosition = new Vector2(0, -h);
            _contentPanel.sizeDelta = new Vector2(-_space * 2, -h - _space);
        }

        private void onTabChange(TabItem tab)
        {
            refreshLayers();
        }
    }
}
