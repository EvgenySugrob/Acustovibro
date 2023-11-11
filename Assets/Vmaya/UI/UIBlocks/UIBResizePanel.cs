using UnityEngine;
using Vmaya.UI.Components;

namespace Vmaya.UI.UIBlocks
{
    [RequireComponent(typeof(UIBPanel))]
    public class UIBResizePanel : ResizePanel
    {
        private UIBPanel panel => GetComponent<UIBPanel>();
        protected override void doResizeAddRect(Rect inc)
        {
            if (!panel.DropBox)
            {
                base.doResizeAddRect(inc);
            }
        }

        protected override Vector2 getMinSize()
        {
            Vector2 fixSize = panel ? panel.fixSize : default;
            Vector2 result = base.getMinSize();
            return new Vector2(Mathf.Max(fixSize.x, result.x), Mathf.Max(fixSize.y, result.y));
        }
    }
}