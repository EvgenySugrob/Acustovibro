using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vmaya.Command;

namespace Vmaya.UI.Menu.Example
{
    public class RotateExecuter : MonoBehaviour, IExecutableAndRecoverable
    {
        [SerializeField]
        private float _angle;

        public void ExecuteCommand()
        {
            CommandManager.ExecuteCmd(new ExecuteCommand(this));
        }

        public void Execute()
        {
            transform.rotation = Quaternion.AngleAxis(_angle, Vector3.up);
        }

        public bool getPerformed()
        {
            return Mathf.Round(Vector3.SignedAngle(Vector3.forward, transform.forward, Vector3.up)) == _angle;
        }

        public string getRecoveryData()
        {
            return JsonUtility.ToJson(transform.rotation.eulerAngles);
        }

        public void Recovery(string data)
        {
            Vector3 angles =  JsonUtility.FromJson<Vector3>(data);
            transform.rotation = Quaternion.Euler(angles);
        }
    }
}
