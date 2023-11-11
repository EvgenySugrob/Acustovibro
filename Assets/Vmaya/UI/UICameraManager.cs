using Vmaya.Scene3D.UI;
using Vmaya.UI.Components;

namespace Vmaya.UI
{
    public class UICameraManager : CameraManager
    {
        public override void nextCamera(int inc = 1)
        {
            if (!Curtain.isModal && !InputControl.isFocus)
            {
                base.nextCamera(inc);
            }
        }
    }
}