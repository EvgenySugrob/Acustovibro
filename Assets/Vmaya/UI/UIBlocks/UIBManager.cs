using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Vmaya.UI.Components;
using Vmaya.UI.UITabs;
using static Vmaya.UI.UIBlocks.UIBPanel;

namespace Vmaya.UI.UIBlocks
{
    public class UIBManager : WindowManager
    {
        public UIBDropBox dropBoxTemplate;
        public UIBSeparator separatorTemplate;

        private UIBPanel _drag;
        public UIBPanel Drag => _drag;
        public Vector2 defaultLimitSize;

        public Vector2 sizePercent = new Vector2(0.2f, 0.2f);

        public UIPanelEvent onBeginDrag;

        public UnityEvent onManualChangeBoxes;

        public UIPanelEvent onEndDrag;

        public void beginDrag(UIBPanel panel)
        {
            _drag = panel;
            onBeginDrag.Invoke(_drag);
        }

        public void endDrag(UIBPanel panel)
        {
            _drag = null;
            onEndDrag.Invoke(panel);
        }

        public UIBDropBoxBase checkHitPanel(Vector2 point, UIBPanel panel, out Rect rect, out int index)
        {
            index = -1;
            if (panel.isAllowedDropping())
            {
                List<UIBDropBoxBase> dblist = new List<UIBDropBoxBase>(GetComponentsInChildren<UIBDropBoxBase>());

                dblist.Sort((UIBDropBoxBase b1, UIBDropBoxBase b2) => { return b1 is DropToTabs ? -1 : 1; });

                foreach (UIBDropBoxBase db in dblist)
                {
                    if (db.hitPlaceRect(point, panel, out rect, out index) && !db.transform.IsChildOf(panel.transform))
                        return db;
                }
            }
            rect = default;
            return null;
        }

        internal void refreshSeparators()
        {
            UIBSeparator[] list = GetComponentsInChildren<UIBSeparator>();
            foreach (UIBSeparator sep in list)
                sep.Refresh();
        }
    }
}