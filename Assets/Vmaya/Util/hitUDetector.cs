using Vmaya.Command;
using Vmaya.Scene3D;

namespace Vmaya.Util
{
    public class hitUDetector : hitDetector
    {
        protected override bool isAllowedA()
        {
            return base.isAllowedA() && !BaseDragDrop.isDragging && !CommandManager.isListRun;
        }
    }
}
