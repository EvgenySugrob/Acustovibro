using UnityEngine;
using UnityEngine.EventSystems;
using Vmaya.UI.UIBlocks;
using Vmaya.UI.UIBlocks.RW;

namespace Vmaya.UI.UITabs
{
    [RequireComponent(typeof(TabItem))]
    public class DragTab : UIComponent, IDragHandler, IPointerUpHandler
    {
        [SerializeField]
        private RWPanelSpawner _panelSpawner;

        [SerializeField]
        private string _prefabName;

        private UIBPanel _panel;

        protected TabItem Tab => GetComponent<TabItem>();

        private void OnValidate()
        {
            if (_panelSpawner)
            {
                if ((_panelSpawner.Templates != null) && (_panelSpawner.Templates.Length > 0))
                {
                    if (string.IsNullOrEmpty(_prefabName))
                        _prefabName = _panelSpawner.Templates[0].name;
                    else if (_panelSpawner.findTemplate(_prefabName) == null)
                        _prefabName = _panelSpawner.Templates[0].name;
                }
                else _prefabName = null;
            }
            else _prefabName = null;
        }

        private void Awake()
        {
            if (string.IsNullOrEmpty(_prefabName) || (_panelSpawner == null))
                Debug.LogError("panelSpawner or prefabName must not be null");
        }

        public void OnDrag(PointerEventData eventData)
        {

            TabContent content = Tab.Content;

            if (content && (content.transform.childCount > 0))
            {
                UIBManager manager = GetComponentInParent<UIBManager>();
                if (manager) _panel = _panelSpawner.createPanel(_prefabName, manager.transform);
                else
                {
                    Debug.Log("Must be a parent of UIBManager");
                    return;
                }

                _panel.name = Utils.UniqueName(_prefabName);
                _panel.gameObject.SetActive(true);
                TabsGroup tg = _panel.contentContainer.GetComponent<TabsGroup>();
                if (tg) tg.addTab(Tab.Caption, content);
                else content.content.transform.SetParent(_panel.contentContainer);
                transform.SetParent(_panel.title.transform);

                Trans.anchoredPosition = new Vector2(_panel.title.Trans.rect.width / 2, -_panel.title.Trans.rect.height / 2);

                _panel.OnPointerDown(Trans.position);
                _panel.RecentlyDetach = true;

                Tab.tabs.OnTabChange(Tab);
                HideTab();
            }
            else _panel.OnDrag(eventData);
        }

        private void HideTab()
        {
            for (int i = 0; i < Tab.transform.childCount; i++)
                Tab.transform.GetChild(i).gameObject.SetActive(false);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (_panel && _panel.isDrag)
            {
                _panel.OnPointerUp(eventData);
                Destroy(gameObject);
            }
        }
    }
}
