using UnityEngine;

namespace Vmaya.UI.UIBlocks
{
    public abstract class UIBDropBoxBase : UIBComponent
    {
        public abstract bool Dropping(UIBPanel a_panel, int index);
        public abstract bool hitPlaceRect(Vector2 point, UIBComponent component, out Rect rect, out int index);
    }
}