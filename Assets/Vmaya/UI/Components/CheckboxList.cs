using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Vmaya.Collections;

namespace Vmaya.UI
{
    public class CheckboxList : MonoBehaviour, ISelectedList
    {
        [SerializeField]
        private Component source;
        protected IListSource _source;

        public bool AutoScroll;

        protected virtual void OnValidate()
        {
            if (source != null)
            {
                _source = source.GetComponent<IListSource>();
                source = _source as Component;
            }
        }
        public IListSource Source
        {
            get
            {
                return _source;
            }
        }

        [SerializeField]
        private Toggle toggleTemplate;
        [SerializeField]
        private float space = 5;
        [SerializeField]
        private float startIndex = -1;

        protected List<Toggle> toggleList;
        protected List<string> indexs;

        public UnityEvent onValuesChange;
        public UnityEvent onSelect;
        [HideInInspector]
        public CheckboxItem.CBEvent onItemChange;

        public bool onlySelect = false;
        private CheckboxItem currentSelect;

        virtual protected void Awake()
        {
            toggleTemplate.gameObject.SetActive(false);
            OnValidate();
            initSource();
            onItemChange = new CheckboxItem.CBEvent();
        }

        private void Start()
        {
            checkSource();
        }

        protected virtual void releaseSource()
        {
            if (_source is BaseListSource)
            {
                (_source as BaseListSource).onChange.RemoveListener(onSourceChange);
                _source.onAfterChange(updateList);
                _source = null;
            }
        }

        protected virtual void initSource()
        {
            if (_source != null)
            {
                if (_source is BaseListSource) (_source as BaseListSource).onChange.AddListener(onSourceChange);
                _source.onAfterChange(updateList);
            }
        }

        private void OnDestroy()
        {
            releaseSource();
        }

        public void setSource(IListSource a_source)
        {
            if (a_source != _source)
            {
                releaseSource();
                _source = a_source;
                source = _source as Component;
                if (isActiveAndEnabled) initSource();
            }
            else if (_source != null) updateList();
        }

        private void checkSource()
        {
            if ((toggleList == null) && (_source != null))
            {
                if (_source.getCount() > 0) updateList();
                else
                {
                    FileSource fsource = _source as FileSource;
                    if (fsource != null) fsource.onLoaded.AddListener(updateList);
                }
            }
        }

        private void OnEnable()
        {
            if (_source != null) _source.Refresh();
        }

        private void onSourceChange()
        {
            updateList();
        }

        public void Clear()
        {
            foreach (Transform child in transform)
                if (child != toggleTemplate.transform)
                    GameObject.Destroy(child.gameObject);

            if (toggleList != null) toggleList.Clear();
        }

        public virtual void updateList()
        {
            List<string> selected = getSelected();
            RectTransform srect = toggleTemplate.GetComponent<RectTransform>();

            Clear();

            if (toggleList == null) toggleList = new List<Toggle>();
            else toggleList.Clear();

            if (indexs == null) indexs = new List<string>();
            else indexs.Clear();

            float height = 5;
            CheckboxItem a_selected = default;
            for (int i = 0; i < _source.getCount(); i++)
            {
                string index = _source.getId(i);
                indexs.Add(index);
                Toggle option = (Toggle)Instantiate(toggleTemplate, transform);
                CheckboxItem item = option.gameObject.AddComponent<CheckboxItem>();
                item.name = toggleTemplate.name + "-" + index;
                item.initialize(this, i, _source.getName(i));

                RectTransform rect = option.GetComponent<RectTransform>();
                rect.localPosition = new Vector2(srect.localPosition.x, srect.localPosition.y - (i * rect.rect.height + space));
                item.onCBChangeValue.AddListener(onChange);
                option.isOn = selected.Contains(index);
                if (onlySelect)
                {
                    if (startIndex == i) option.isOn = true;
                    if (option.isOn) a_selected = item;
                }

                toggleList.Add(option);
                height += rect.rect.height;
            }
            RectTransform mrect = GetComponent<RectTransform>();

            mrect.sizeDelta = new Vector2(mrect.sizeDelta.x, height + space * 2);

            onValuesChange.Invoke();
            if (a_selected) 
                doSelect(a_selected);
        }

        virtual protected void Update()
        {
            if (startIndex > -1)
            {
                startIndex = -1;
                onSelect.Invoke();
            }
        }

        protected void scrollToItem(CheckboxItem item)
        {
            if (item)
            {   ScrollRect scrollRect = GetComponentInParent<ScrollRect>();
                if (scrollRect)
                    Utils.EnsureVisibility(scrollRect, item.GetComponent<RectTransform>());
            }
        }

        protected virtual void doSelect(CheckboxItem item)
        {
            currentSelect = item;
            if (AutoScroll) scrollToItem(currentSelect);
            onSelect.Invoke();
        }

        protected void onChange(bool value, CheckboxItem item)
        {
            if (onlySelect && currentSelect && (currentSelect != item))
                currentSelect.GetComponent<Toggle>().isOn = false;

            onItemChange.Invoke(value, item);
            onValuesChange.Invoke();
            doSelect(value ? item : null);
        }

        public CheckboxItem CurrentSelect
        {
            get
            {
                return currentSelect;
            }
        }

        public List<Toggle> getToggleList()
        {
            return toggleList;
        }

        public void setSelected(List<string> selected)
        {
            checkSource();
            if (toggleList != null)
                for (int i = 0; i < toggleList.Count; i++)
                    toggleList[i].isOn = selected.Contains(indexs[i]);
        }

        virtual public List<string> getSelected()
        {
            return getIds();
        }

        public List<string> getIds()
        {
            List<string> result = new List<string>();
            if (toggleList != null)
                for (int i = 0; i < toggleList.Count; i++)
                {
                    if (toggleList[i].isOn) result.Add(indexs[i]);
                }
            return result;
        }

        public List<string> getSelectedValues()
        {
            List<string> result = new List<string>();
            if (toggleList != null)
                for (int i = 0; i < toggleList.Count; i++)
                {
                    if (toggleList[i].isOn) result.Add(_source.getData(i));
                }
            return result;
        }

        public int getSelectedIndex()
        {
            if (CurrentSelect) return CurrentSelect.Index;
            return -1;
        }

        public string getSelectedValue()
        {
            List<string> result = new List<string>();
            if (toggleList != null)
                for (int i = 0; i < toggleList.Count; i++)
                {
                    if (toggleList[i].isOn) return _source.getData(i);
                }
            return null;
        }
    }
}