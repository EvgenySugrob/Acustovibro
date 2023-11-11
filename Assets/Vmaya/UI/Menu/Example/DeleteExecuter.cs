using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vmaya.Command;

namespace Vmaya.UI.Menu.Example
{
    public class DeleteExecuter : MonoBehaviour, IExecutableAndRecoverable
    {
        public void ExecuteCommand()
        {
            CommandManager.ExecuteCmd(new ExecuteCommand(this));
        }

        public void Execute()
        {
            gameObject.SetActive(false);
        }

        public bool getPerformed()
        {
            return !gameObject.activeSelf;
        }

        public string getRecoveryData()
        {
            return null;
        }

        public void Recovery(string data)
        {
            gameObject.SetActive(true);
        }
    }
}
