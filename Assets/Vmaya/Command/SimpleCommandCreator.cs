using System;
using UnityEngine;
using Vmaya.Command;

namespace Vmaya.VideoObs.Commands
{
    public class SimpleCommandCreator : MonoBehaviour, ICommandCreator
    {
        public ICommand createCommand(string commandPack)
        {
            ICommand result = null;
            Type t;

            string[] unpack = commandPack.Split(new char[] { '/' }, 2);

            if ((unpack.Length > 1) && ((t = Type.GetType(unpack[0])) != null))
            {
                object obj = Activator.CreateInstance(t);
                JsonUtility.FromJsonOverwrite(unpack[1], obj);
                result = obj as ICommand;
            }
            else Vmaya.Util.Utils.LogError(this, "Wrong command pack: " + commandPack);

            return result;
        }

        public string packCommand(ICommand command)
        {
            return command.GetType() + "/" + JsonUtility.ToJson(command);
        }
    }
}
