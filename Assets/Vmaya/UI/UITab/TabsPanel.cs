using Vmaya.UI.UIBlocks;
using Vmaya.UI.UIBlocks.RW;
using UnityEngine;
using Vmaya.UI;

namespace Vmaya.UI.UITabs
{
    public class TabsPanel : UIBPanel
    {
        public RWContentSpawner ContentSpawner;

        public TabsGroup tabs => contentContainer ? contentContainer.GetComponent<TabsGroup>() : null;
        private void OnValidate()
        {
            bool check = _contentConteiner != null;
            _contentConteiner = tabs ? tabs.GetComponent<RectTransform>() : null;
            if (check && (_contentConteiner == null)) Debug.Log("The object must include a TabsGroup component");
        }

        public TabContent addContent(string prefabName)
        {
            TabContent content = ContentSpawner.createContent(prefabName, tabs.contentLayer);
            if (content)
                addContent(content);
            return content;
        }

        public TabContent addContent(TabContent content)
        {   
            tabs.addTab(content);
            return content;
        }

        protected override string getJsonData()
        {
            TabsPanelData data = new TabsPanelData(tabs.IndexOf(tabs.Select), GetScreenCoordinates());
            foreach (TabItem item in tabs.items)
                if (item.Content) 
                    data.list.Add(item.Content.getData());
            return JsonUtility.ToJson(data);
        }

        protected override void setJsonData(string jsonData)
        {
            base.setJsonData(jsonData);
            tabs.Clear();
            if (!string.IsNullOrEmpty(jsonData) && ContentSpawner)
            {
                TabsPanelData data = JsonUtility.FromJson<TabsPanelData>(jsonData);

                foreach (ComponentData item in data.list)
                    if (!string.IsNullOrEmpty(item.prefabName)) { 
                        TabContent content = addContent(item.prefabName);
                        if (content && !string.IsNullOrEmpty(item.jsonData))
                            content.setData(JsonUtility.FromJson<ComponentData>(item.jsonData));
                    }

                if (data.currentTabIndex > -1)
                    Vmaya.Utils.setTimeout(this, () => { 
                        if (data.currentTabIndex < tabs.items.Length)
                            tabs.Select = tabs.items[data.currentTabIndex]; 
                    }, Consts.MID_TIMEOUT);
            }
        }

        protected override Vector2 getFixSize()
        {
            Vector2 result = Vector2.zero;
            TabContent[] contents = tabs.contents;

            if (contents.Length > 0) result = contents[0].fixSize;
            /*foreach (TabContent content in contents)
            {
                result.x = Mathf.Max(result.x, content.fixSize.x);
                result.y = Mathf.Max(result.y, content.fixSize.y);
            }*/
            return result;
        }
    }
}