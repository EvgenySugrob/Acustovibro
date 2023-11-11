using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Vmaya.UI.Tools
{
    public class ToolPanel : UIComponent
    {
        [SerializeField]
        private int _role;
        [SerializeField]
        private Text _title;
        [SerializeField]
        private RectTransform _block;
        [SerializeField]
        private string _panelName;

        [SerializeField]
        private ToolToggle _toggleTemplate;

        private int _indexCounter;

        public int Role => _role;
        public string PanelName { get => _panelName; set => setPanelName(value); }

        private void OnValidate()
        {
            setPanelName(_panelName);
        }

        private void Start()
        {
            _indexCounter = 0;
        }

        protected TopTools topTools => Vmaya.Utils.getParent<TopTools>(transform);


        public ToolToggle[] items => _block.GetComponentsInChildren<ToolToggle>();

        private void Awake()
        {
            if (items.Length > 0) startInitItems();
        }

        private void startInitItems()
        {
            foreach (ToolToggle toggle in items)
                if (toggle.item == null) {
                    _indexCounter++;
                    toggle.resetItem(_indexCounter);
                }
            StartCoroutine(updateDelay());
        }

        private void setPanelName(string value)
        {
            if (_title) _title.text = _panelName = value;
        }

        public ToolToggle createToggle(string name, string mode, Sprite a_sprite)
        {
            ToolToggle tool = createToggle(new ToolModeItem(name, mode, a_sprite));
            return tool;
        }

        public void setVisibility(bool value)
        {
            if (topTools)
            {
                if (value) topTools.showPanel(this);
                else topTools.closePanel(this);
            }
            else gameObject.SetActive(value);
        }

        public ToolToggle createToggle(ToolItem item)
        {
            if (_toggleTemplate)
            {
                setVisibility(true);
                ToolToggle tool = Instantiate(_toggleTemplate, _block);
                _indexCounter++;

                item.number = _indexCounter;
                tool.item = item;
                tool.toggle.group = _block.GetComponentInParent<ToggleGroup>();
                StartCoroutine(updateDelay());
                return tool;
            }
            else return null;
        }

        public void clearToggles()
        {
            _indexCounter = 0;

            ToolToggle[] items = _block.GetComponentsInChildren<ToolToggle>();
            foreach (ToolToggle item in items)
                Destroy(item.gameObject);

            Refresh();
        }

        internal void removeToggle(ToolToggle tb)
        {
            Destroy(tb.gameObject);
            updateDelay();
        }

        private IEnumerator updateDelay()
        {
            yield return new WaitForSeconds(0.01f);
            Refresh();
        }

        private void Refresh()
        {
            if (items.Length > 0)
            {
                setVisibility(true);

                if (!((Trans.anchorMin.x == 0f) && (Trans.anchorMax.x == 1f)))
                {
                    Vector2 size = _block.sizeDelta;
                    Trans.sizeDelta = new Vector2(size.x + 8, Trans.sizeDelta.y);
                }
            }
            else setVisibility(false);
        }
    }
}