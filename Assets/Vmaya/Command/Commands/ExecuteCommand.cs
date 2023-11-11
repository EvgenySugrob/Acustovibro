using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Vmaya.Language;

namespace Vmaya.Command
{
    public class ExecuteCommand : BaseCommand
    {
        [SerializeField]
        private Indent _executableComponent;

        [SerializeField]
        private string _data;

        protected IExecutableAndRecoverable executable => _executableComponent.Find() as IExecutableAndRecoverable;

        public ExecuteCommand(IExecutableAndRecoverable executable)
        {
            _executableComponent = new Indent(executable as Component);
        }

        public override string commandName()
        {
            return Lang.instance["Execute"];
        }

        public override bool execute()
        {
            if (!executable.getPerformed())
            {
                _data = executable.getRecoveryData();
                executable.Execute();
                return true;
            }
            else return false;
        }

        public override void redo()
        {
            executable.Execute();
        }

        public override void undo()
        {
            executable.Recovery(_data);
        }
    }
}
