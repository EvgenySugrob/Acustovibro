using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

namespace Vmaya.UI.UITabs
{
    public class TabItem : UIComponent, IPointerDownHandler, IPointerClickHandler
    {
        [SerializeField]
        private float _space;
        [SerializeField]
        private TextMeshProUGUI _text;
        [SerializeField]
        private Image _backgroundImage;
        [SerializeField]
        private Button _closeButton;

        private TabsGroup _tabs;
        public TabsGroup tabs { get => _tabs; set => setTabsGroup(value); }
        public bool Selected { get => _tabs ? _tabs.Select == this : false; set => setSelected(value); }

        public DragTab dragTab => GetComponent<DragTab>();
        public TabContent Content => getContent();
        private float _setSpace;

        private void Awake()
        {
            if (_closeButton) _closeButton.onClick.AddListener(onClickCloseButton);

            _setSpace = Trans.rect.width - _text.GetComponent<RectTransform>().rect.width;
        }

        private void onClickCloseButton()
        {
            Destroy(gameObject);
            if (Content) Content.Close();
            tabs.setSelect(null);
        }

        private void setSelected(bool value)
        {
            if (_tabs)
            {
                if (value)
                    _tabs.setSelect(this);
                else if (Selected) _tabs.setSelect(null);
            }
        }

        private void Visibility(bool asSelect)
        {
            _backgroundImage.enabled = asSelect;
            if (_closeButton) _closeButton.gameObject.SetActive(asSelect);
        }

        public void setTabsGroup(TabsGroup value)
        {
            if (_tabs != value)
            {
                if (_tabs)
                {
                    _tabs.onTabChange.RemoveListener(onTabChange);
                }
                _tabs = value;
                if (_tabs)
                {
                    transform.SetParent(value.tabsLayer);
                    _tabs.onTabChange.AddListener(onTabChange);
                    if (_tabs.items.Length == 1) _tabs.setSelect(this);
                    else refreshDragTab();
                }
                Visibility(false);
            }
        }

        private void onTabChange(TabItem item)
        {
            if (item == this)
            {
                Visibility(Selected);
                refreshDragTab();
            }
        }

        public void refreshDragTab()
        {
            if (dragTab) dragTab.enabled = !Trans.IsChildOf(_tabs.tabsLayer) || Selected || !IsRequireScroll;
        }

        private TabContent getContent()
        {
            TabContent[] list = tabs.contentLayer.GetComponentsInChildren<TabContent>(true);
            foreach (TabContent item in list)
                if (item.Tab == this) return item;

            return null;
        }

        public string Caption { get => _text.text; set => setCaption(value); }
        public void setCaption(string text)
        {
            _text.text = text;
            refreshSize();
        }

        private void refreshSize()
        {
            float width = _text.preferredWidth + _space + _setSpace;
            Trans.sizeDelta = new Vector2(width, Trans.sizeDelta.y);
        }

        private void Start()
        {
            refreshSize();
        }

        private bool IsRequireScroll => tabs.scrollRect ? tabs.scrollRect.GetComponent<TabsScrollButtons>().isRequireScroll : false;

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!IsRequireScroll) Selected = true;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (IsRequireScroll) Selected = true;
        }
    }
}