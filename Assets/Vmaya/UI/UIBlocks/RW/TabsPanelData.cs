using UnityEngine;

namespace Vmaya.UI.UIBlocks.RW
{
    [System.Serializable]
    public class TabsPanelData : PanelData
    {
        public int currentTabIndex;

        public TabsPanelData(int currectTabIndex, Rect rect) : base(rect)
        {
            this.currentTabIndex = currectTabIndex;
        }
    }
}