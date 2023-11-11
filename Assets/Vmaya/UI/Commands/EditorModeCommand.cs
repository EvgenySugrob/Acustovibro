using Vmaya.Command;
using UnityEngine;

namespace Vmaya.UI.Commands
{
    public class EditorModeCommand : BaseCommand
    {
        [SerializeField]
        private string _mode;
        [SerializeField]
        private string _prevMode;

        public EditorModeCommand() {}

        public EditorModeCommand(string mode)
        {
            _mode = mode;
            _prevMode = EditorMode.instance.mode;
        }

        public override string commandName()
        {
            return "Смена режима";
        }

        public override bool execute()
        {
            EditorMode.setMode(_mode, false);
            return true;
        }

        public override bool isReady()
        {
            return true;
        }

        public override void redo()
        {
            EditorMode.setMode(_mode, false);
        }

        public override void undo()
        {
            EditorMode.setMode(_prevMode, false);
        }
    }
}