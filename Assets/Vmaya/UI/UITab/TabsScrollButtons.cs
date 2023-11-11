using UnityEngine;
using UnityEngine.UI;

namespace Vmaya.UI.UITabs
{
    [RequireComponent(typeof(ScrollRect))]
    public class TabsScrollButtons : UIComponent
    {
        [SerializeField]
        private Button _leftButton;
        [SerializeField]
        private Button _rightButton;
        [SerializeField]
        private TabsGroup _tabsGroup;

        public ScrollRect scrollView => GetComponent<ScrollRect>();

        private RectTransform viewportRect => scrollView.viewport.GetComponent<RectTransform>();
        private RectTransform contentRect => scrollView.content.GetComponent<RectTransform>();
        private Bounds scrollBounds => RectTransformUtility.CalculateRelativeRectTransformBounds(scrollView.viewport, scrollView.content);
        private bool requireLeftButton => scrollBounds.min.x < 0;
        private bool requireRightButton => scrollBounds.max.x - viewportRect.rect.width > 0;
        private float requireScoll => contentRect.rect.width - viewportRect.rect.width;

        private bool _isRequireScrollPrev = false;
        public bool isRequireScroll => requireScoll > 0;

        private void Awake()
        {
            _leftButton.onClick.AddListener(leftClick);
            _rightButton.onClick.AddListener(rightClick);
            scrollView.onValueChanged.AddListener(onScrollChange);
            refresh();
        }

        private void onScrollChange(Vector2 arg0)
        {
            refresh();
        }

        private float step => contentRect.rect.width / contentRect.rect.width * 0.5f;

        private void rightClick()
        {
            scrollView.horizontalNormalizedPosition += step;
        }

        private void leftClick()
        {
            scrollView.horizontalNormalizedPosition -= step;
        }

        private void refresh()
        {
            _leftButton.gameObject.SetActive(requireLeftButton);
            _rightButton.gameObject.SetActive(requireRightButton);
            _isRequireScrollPrev = isRequireScroll;
            TabItem[] list = _tabsGroup.items;
            foreach (TabItem item in list) item.refreshDragTab();
        }

        private void Update()
        {
            if (_isRequireScrollPrev != isRequireScroll) refresh();
        }
    }
}