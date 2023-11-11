using Vmaya.UI.UIBlocks.RW;
using UnityEngine;
using Vmaya.Language;

namespace Vmaya.UI.UITabs
{
    public class TabContent : RWComponent
    {
        [SerializeField]
        private TabItem _tab;

        [SerializeField]
        private string _title;

        public Vector2 fixSize;

        public string title { get => Lang.instance[_title]; set => setTitle(value); }

        private void setTitle(string value)
        {
            if (!value.Equals(_title))
            {
                _title = value;
                if (_tab) _tab.Caption = title;
            }
        }

        private bool _initialize = false;

        public TabItem Tab { get => _tab; internal set => setTab(value); }

        public RectTransform content => transform.childCount > 0 ? transform.GetChild(0).GetComponent<RectTransform>() : null;

        private void Start()
        {
            if (!_initialize) init();
        }

        protected virtual void init()
        {
            if (_tab) {
                _tab.tabs.onTabChange.AddListener(onTabChange);
                gameObject.SetActive(_tab.Selected);
                _tab.Caption = title;
                _initialize = true;
            }
        }

        private void onTabChange(TabItem item)
        {
            gameObject.SetActive(_tab.Selected);
            if (item == _tab)
                _tab.Caption = title;
        }

        protected void setTab(TabItem a_tab)
        {
            if (_tab != a_tab)
            {
                _tab = a_tab;
                init();
            }
        }

        public void Show()
        {
            if (Tab) Tab.tabs.setSelect(Tab);
        }
    }
}