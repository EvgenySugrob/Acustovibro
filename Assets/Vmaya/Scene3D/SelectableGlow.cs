using UnityEngine;

namespace Vmaya.Scene3D
{
    [RequireComponent(typeof(glowMaterial))]
    public class SelectableGlow : SelectableMouse
    {
        protected glowMaterial _glow => GetComponent<glowMaterial>();
        override public void showSelect()
        {
            base.showSelect();
            _glow.glowBegin(_glow.hitValue);
        }

        override public void hideSelect()
        {
            base.hideSelect();
            _glow.glowBegin(0);
        }

        public override void Reset()
        {
            _glow.initColors();
        }
    }
}