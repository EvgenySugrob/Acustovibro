using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vmaya.Command
{
    public abstract class BaseCommand : ICommand
    {
        protected BaseCommand() {}

        public abstract string commandName();
        public virtual void destroy() { }
        public abstract bool execute();

        public virtual float executeTime() { return 0; }

        public virtual bool isReady() { return true; }

        public abstract void redo();
        public abstract void undo();

    }
}