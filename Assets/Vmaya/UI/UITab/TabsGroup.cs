using System.Collections.Generic;
using Vmaya.UI.UIBlocks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Vmaya.UI.UITabs
{
    public class TabsGroup : UIComponent
    {
        [SerializeField]
        private TabItem _itemTemplate;

        [SerializeField]
        private ScrollRect _scrollRect;
        public ScrollRect scrollRect => _scrollRect;

        [SerializeField]
        private RectTransform _tabsLayer;
        public RectTransform tabsLayer => _tabsLayer;

        [SerializeField]
        private RectTransform _contentLayer;
        public RectTransform contentLayer => _contentLayer;

        [SerializeField]
        private UIBPanel _ownerPanel;

        [System.Serializable]
        public class TabEvent : UnityEvent<TabItem> {}

        private bool _initialized;

        public TabEvent onTabChange;
        public UnityEvent onInitialize;
        public TabItem[] items => _tabsLayer.GetComponentsInChildren<TabItem>(true);
        public TabContent[] contents => contentLayer.GetComponentsInChildren<TabContent>(true);

        private TabItem _select;
        public TabItem Select { get => _select; set => setSelect(value); }

        public bool showMe => items.Length > 1;


        private void Start()
        {
            startRefreshItems();
        }

        internal void Clear()
        {
            TabItem[] a_items =  items;
            foreach (TabItem item in a_items)
                Destroy(item.gameObject);
        }

        internal void moveTabs(TabsGroup tg)
        {
            List<TabItem> links = new List<TabItem>();
            List<TabContent> s_contents = new List<TabContent>(tg.contents);

            TabItem selected = tg.Select;

            for (int i = 0; i < s_contents.Count; i++)
            {
                links.Add(s_contents[i].Tab);
                s_contents[i].Tab = null;
            }

            foreach (TabItem item in tg.items)
                item.setTabsGroup(this);

            for (int i = 0; i < s_contents.Count; i++)
            {
                s_contents[i].transform.SetParent(_contentLayer);
                s_contents[i].Tab = links[i];
                allPlace(s_contents[i].GetComponent<RectTransform>());
            }
            if (selected) setSelect(selected);
        }

        private void startRefreshItems()
        {
            List<TabContent> s_contents = new List<TabContent>(contents);
            foreach (TabContent content in s_contents)
                if (content.Tab == null)
                    addTab(content);

            foreach (TabItem item in items)
                item.tabs = this;

            if (_select == null) FindToSelect();
        }

        private void Update()
        {
            if (!_initialized) StartInit();
        }

        private void refreshPanelTitle()
        {
            if (_select) _ownerPanel.title.SetText(_select.Caption);
        }

        private void StartInit()
        {
            foreach (TabItem item in items)
                if (item.Selected)
                {
                    refreshPanelTitle();
                    OnTabChange(item);
                    break;
                }
            _initialized = true;
            onInitialize.Invoke();
        }

        public void setSelect(TabItem value)
        {
            if (_select != value)
            {
                TabItem prev = _select;
                _select = !value || (IndexOf(value) > -1) ? value : null;
                if (prev) OnTabChange(prev);
                if (_select)
                {
                    OnTabChange(_select);
                    refreshPanelTitle();
                }
                else FindToSelect();
            }
        }

        private void FindToSelect()
        {
            Vmaya.Utils.setTimeout(this, () => { if (items.Length > 0) items[0].Selected = true; }, 0.01f);
        }

        internal void OnTabChange(TabItem tabItem)
        {
            onTabChange.Invoke(tabItem);

            if (tabItem.Selected)
            {
                if (IndexOf(tabItem) == -1) FindToSelect();
                else Vmaya.Utils.setTimeout(this, () => { focusToItem(tabItem); }, Consts.MIN_TIMEOUT);
            }
        }

        internal int IndexOf(TabItem tabItem)
        {
            TabItem[] list = items;
            for (int i = 0; i < list.Length; i++)
                if (list[i] == tabItem) return i;

            return -1;
        }

        private void focusToItem(TabItem tabItem)
        {
            if (scrollRect)
            {
                RectTransform STrans = scrollRect.content.GetComponent<RectTransform>();
                RectTransform VTrans = scrollRect.viewport.GetComponent<RectTransform>();

                Vector2 diffLength = new Vector2(STrans.rect.width - VTrans.rect.width, STrans.rect.height - VTrans.rect.height);

                if (diffLength.sqrMagnitude > 0)
                {

                    Vector3[] corners = new Vector3[4];
                    tabItem.Trans.GetLocalCorners(corners);

                    Vector2 leftEdge;
                    Vector2 rightEdge;
                    RectTransformUtility.ScreenPointToLocalPointInRectangle(VTrans, tabItem.Trans.TransformPoint(corners[0]), null, out leftEdge);
                    RectTransformUtility.ScreenPointToLocalPointInRectangle(VTrans, tabItem.Trans.TransformPoint(corners[2]), null, out rightEdge);

                    float leftOffset = leftEdge.x;
                    float rightOffset = rightEdge.x - VTrans.rect.width;

                    if (leftEdge.x < 0)
                        scrollRect.horizontalNormalizedPosition += leftOffset / diffLength.x;
                    else if (rightOffset > 0)
                        scrollRect.horizontalNormalizedPosition += rightOffset / diffLength.x;

                    float topOffset = leftEdge.y;
                    float bottomOffset = rightEdge.y - VTrans.rect.height;

                    if (leftEdge.y < 0)
                        scrollRect.verticalNormalizedPosition += topOffset / diffLength.y;
                    else if (rightOffset > 0)
                        scrollRect.verticalNormalizedPosition += bottomOffset / diffLength.y;
                }
            }
        }

        public TabItem addTab(TabContent container)
        {
            TabItem item = Instantiate(_itemTemplate, _tabsLayer ? _tabsLayer : transform);

            allPlace(container.GetComponent<RectTransform>());

            item.name = Utils.UniqueName(_itemTemplate.name);
            item.setCaption(container.title);
            item.tabs = this;
            container.Tab = item;

            return item;
        }

        public TabItem addTab(string title, TabContent container)
        {
            TabItem item = Instantiate(_itemTemplate, _tabsLayer ? _tabsLayer : transform);

            Vector2 tmpSize = container.content.sizeDelta;
            Vector2 ap = container.content.anchoredPosition;

            container.transform.SetParent(_contentLayer);

            allPlace(container.GetComponent<RectTransform>());

            container.content.sizeDelta = tmpSize;
            container.content.anchoredPosition = ap;

            item.name = Utils.UniqueName(_itemTemplate.name);
            item.setCaption(title);
            item.tabs = this;
            container.Tab = item;

            return item;
        }
    }
}