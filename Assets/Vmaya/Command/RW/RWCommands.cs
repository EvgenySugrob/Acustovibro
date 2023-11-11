using Vmaya.RW;
using UnityEngine;

namespace Vmaya.Command.RW
{
    [RequireComponent(typeof(CommandManager))]
    public class RWCommands : RWEvents
    {
        public CommandManager commandManager => GetComponent<CommandManager>();

        protected override void doReadData(dataRecord rec)
        {
            if (!string.IsNullOrEmpty(rec.data))
                commandManager.setListJson(rec.data);
        }

        protected override string doWriteData()
        {
            return commandManager.getCurrentListJson();
        }
    }
}