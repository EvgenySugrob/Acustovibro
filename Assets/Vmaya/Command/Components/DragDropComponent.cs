using UnityEngine;
using Vmaya.Scene3D;

namespace Vmaya.Command.Components {
    public class DragDropComponent : DragDrop3d
    {
        private DragDropCommand _command;

        override protected void doBeginDrag()
        {
            base.doBeginDrag();
            _command = createCommand() as DragDropCommand;
        }

        override protected bool isAllowedDrag()
        {
            return base.isAllowedDrag() && !CommandManager.isListRun;
        }

        virtual protected ICommand createCommand()
        {
            return new DragDropCommand(this);
        }

        private CommandManager commandManager
        {
            get { return CommandManager.instance; }
        }

        override protected void doEndDrag()
        {
            base.doEndDrag();
            if (commandManager)
            {
                _command.saveFinalPosition();
                commandManager.executeCmd(_command);
            }
        }
    }
}