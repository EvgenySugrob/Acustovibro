using System;
using System.Collections;
using System.Collections.Generic;
using Vmaya.UI.Menu;
using UnityEngine;
using UnityEngine.Events;
using Vmaya.UI.Users;
using Vmaya.Language;

namespace Vmaya.UI.Tools
{
    public class TopTools : MonoBehaviour
    {

        [SerializeField]
        private ToolPanel _endPanel;

        [SerializeField]
        private ToolPanel _toolPanelTemplate;

        [SerializeField]
        private MainMenu _mainMenu;

        [SerializeField]
        private int _menuPosition;

        [SerializeField]
        private ActionMap _actionMap;

        [SerializeField]
        private ToolPanel _defaultPanel;
        public ToolPanel defaultPanel { get { return _defaultPanel; } }

        private MenuItemData _mainMenuData;


        private void Awake()
        {
            if (_endPanel) StartCoroutine(checkEndPanel());
        }

        private float _prevScreenWidth = 0;
        private float _prevPanelCount = 0;
        private IEnumerator checkEndPanel()
        {
            yield return new WaitForSeconds(0.2f);

            if (_endPanel && ((_prevScreenWidth != Screen.width) || (_prevPanelCount != ActivePanels.Length)))
            {
                if (_endPanel.gameObject.activeSelf)
                {
                    _endPanel.transform.SetSiblingIndex(ActivePanels.Length - 1);

                    RectTransform rect = GetComponent<RectTransform>();
                    RectTransform eprect = _endPanel.GetComponent<RectTransform>();

                    float cwidth = 0;
                    foreach (ToolPanel panel in ActivePanels)
                        if (panel != _endPanel) cwidth += panel.GetComponent<RectTransform>().sizeDelta.x;

                    float newWidth = Screen.width - cwidth - 10;
                    if (newWidth != eprect.sizeDelta.x)
                        eprect.sizeDelta = new Vector2(newWidth, eprect.sizeDelta.y);

                    _prevScreenWidth = Screen.width;
                    _prevPanelCount = ActivePanels.Length;
                }
            }

            StartCoroutine(checkEndPanel());
        }

        private void Start()
        {
            if (EditorMode.instance) EditorMode.instance.onChange.AddListener(onChangeMode);

            ToolPanel[] panel_list = panels;

            gameObject.SetActive(panel_list.Length > 0);
            foreach (ToolPanel panel in panel_list) updateMainMenu(panel);
        }

        public ToolToggle[] items
        {
            get
            {
                return GetComponentsInChildren<ToolToggle>();
            }
        }

        public List<ToolToggle> modeItems
        {
            get
            {
                List<ToolToggle> result = new List<ToolToggle>();
                foreach (ToolToggle t in items)
                    if (t.item is ToolModeItem) result.Add(t);

                return result;
            }
        }

        private void onChangeMode()
        {
            foreach (ToolToggle tm in modeItems)
                if ((EditorMode.instance.mode == (tm.item as ToolModeItem).mode) && (tm.toggle.isOn))
                    return;

            foreach (ToolToggle tm in modeItems)
                tm.toggle.isOn = EditorMode.instance.mode == (tm.item as ToolModeItem).mode;
        }
/*
        private void onToggleChange(bool value)
        {
            foreach (ToolToggle tm in items)
                if (tm.toggle.isOn)
                {
                    EditorMode.setMode(tm.mode);
                    return;
                }

            EditorMode.setMode(null);
        }
*/
        public ToolPanel[] ActivePanels { get => GetComponentsInChildren<ToolPanel>(false); }
        public ToolPanel[] AllPanels { get => GetComponentsInChildren<ToolPanel>(true); }

        private void Refresh()
        {
            gameObject.SetActive(ActivePanels.Length > 0);
            if (gameObject.activeSelf && _endPanel) 
                StartCoroutine(checkEndPanel());
        }

        public void showPanel(ToolPanel panel)
        {
            gameObject.SetActive(true);
            panel.gameObject.SetActive(true);
            Refresh();
        }

        public void showPanel(string panelName)
        {
            foreach (ToolPanel panel in AllPanels)
                if (panel.PanelName.Equals(panelName) || panel.name.Equals(panelName))
                {
                    showPanel(panel);
                    break;
                }
        }

        public void closePanel(ToolPanel panel)
        {
            //EditorMode.instance.mode = null;
            panel.gameObject.SetActive(false);

            foreach (ToolToggle tm in items)
                tm.toggle.isOn = false;

            Refresh();
        }

        public ToolPanel[] panels
        {
            get
            {
                return GetComponentsInChildren<ToolPanel>(true);
            }
        }

        public void refreshPanels(User user)
        {
            foreach (ToolPanel pn in panels)
                if (pn.Role >= user.role) showPanel(pn);
                else closePanel(pn);
        }

        public void removePanel(ToolPanel panel)
        {
            if (panel)
            {
                if (!Vmaya.Utils.IsDestroyed(panel))
                    Destroy(panel.gameObject);

                _mainMenuData.removeSubMenu(panel.PanelName);
                _actionMap.removeAction(panel.PanelName);

                if (_mainMenuData.subItems.Count == 0)
                {
                    _mainMenu.RemoveItem(_mainMenuData);
                    _mainMenuData = null;
                }
                Refresh();
            }
        }

        private void updateMainMenu(ToolPanel tp)
        {
            if (_mainMenu)
            {
                bool isNew = _mainMenuData == null;
                if (isNew)
                {
                    _mainMenuData = new MenuItemData(Lang.instance["Panels"]);
                }

                if (_mainMenuData.Find(tp.PanelName) == -1)
                    _mainMenuData.addSubMenu(tp.PanelName, tp.PanelName);

                if (isNew) _mainMenu.InsertItem(Math.Min(_mainMenu.items.Count, _menuPosition), _mainMenuData);
                else _mainMenu.refreshItems();
            }

            UnityEvent a_event = new UnityEvent();
            a_event.AddListener(() => {showPanel(tp);});
            _actionMap.addAction(tp.PanelName, a_event);
        }

        public ToolPanel createPanel(string namePanel)
        {
            ToolPanel tp = Instantiate(_toolPanelTemplate, transform);
            tp.PanelName = namePanel;

            updateMainMenu(tp);
            Refresh();

            return tp;
        }
    }
}