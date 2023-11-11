using Vmaya.UI.UIBlocks.RW;
using UnityEngine;

namespace Vmaya.UI.UITabs
{
    public class TabExample : MonoBehaviour
    {
        public RWPanelSpawner PanelSpawner;

        private int index = -1;
        public void createPanel()
        {
            TabsPanel panel = PanelSpawner.createRandom(transform) as TabsPanel;
            index = (index + 1) % panel.ContentSpawner.Templates.Length;
            panel.addContent(panel.ContentSpawner.Templates[index].name);
        }

        public void createPanel(string panelName, string contentName)
        {
            TabsPanel panel = PanelSpawner.createPanel(panelName, transform) as TabsPanel;
            panel.addContent(contentName);
        }
    }
}
