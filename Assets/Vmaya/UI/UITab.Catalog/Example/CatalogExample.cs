using UnityEngine;
using Vmaya.UI.UITabs;

namespace Vmaya.UI.UIBlocks.Examples.Catalog
{
    public class CatalogExample : MonoBehaviour
    {

        public TabsPanel PanelPrefab;
        public TabContent CatalogContentPrefab;
        public TabContent[] otherContentPrefabs;

        private int _contentIndex = -1;

        private TabsPanel createPanel()
        {
            TabsPanel panel = Instantiate(PanelPrefab, transform);
            panel.Origin = PanelPrefab;
            return panel;
        }
        private bool createPanel(string contentPrefabName)
        {
            if (!findInstantiated(contentPrefabName)) { 
                TabsPanel panel = createPanel();
                TabContent content = panel.addContent(contentPrefabName);
                if (content.fixSize.y > 0)
                {
                    panel.Trans.sizeDelta = new Vector2(panel.Trans.sizeDelta.x, content.fixSize.y);
                }

                return true;
            }

            return false;
        }

        private TabContent findInstantiated(string contentNamePrefab)
        {
            TabContent[] list = GetComponentsInChildren<TabContent>();
            foreach (TabContent content in list)
                if (contentNamePrefab.Equals(content.Origin.name)) return content;

            return null;
        }

        public void createOtherPanel()
        {
            if (otherContentPrefabs.Length > 0)
            {
                int nidx = _contentIndex;
                void nextTryCreate()
                {
                    nidx = (nidx + 1) % otherContentPrefabs.Length;
                    if (!createPanel(otherContentPrefabs[nidx].name) && (nidx != _contentIndex))
                        nextTryCreate();
                    else _contentIndex = nidx;
                }

                nextTryCreate();
            }
        }

        public void createCatalogPanel()
        {
            createPanel(CatalogContentPrefab.name);
        }
    }
}