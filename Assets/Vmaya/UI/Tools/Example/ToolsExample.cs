using System.Collections.Generic;
using UnityEngine;

namespace Vmaya.UI.Tools
{
    public class ToolsExample : MonoBehaviour
    {
        [SerializeField]
        private ToolModeItem[] toolItems;

        public ToolPanel toolPanel;

        private List<ToolToggle> _toggles;

        private void Start()
        {
            createTools();
        }

        protected virtual void createTools()
        {
            if ((toolItems != null) && (toolItems.Length > 0))
            {
                _toggles = new List<ToolToggle>();

                foreach (ToolItem ti in toolItems)
                    _toggles.Add(toolPanel.createToggle(ti));
            }
        }
    }
}
