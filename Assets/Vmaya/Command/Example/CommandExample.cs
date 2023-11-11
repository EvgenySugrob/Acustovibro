using System;
using UnityEngine;
using Vmaya.Scene3D;

namespace Vmaya.Command
{
    [RequireComponent(typeof(CommandManager))]
    public class CommandExample : MonoBehaviour
    {
        public Transform template;

        public CommandManager commandManager => GetComponent<CommandManager>();
        private void Update()
        {
            if (VMouse.GetMouseButtonDown(0)) checkHit();
        }

        private void checkHit()
        {
            Ray ray = Camera.main.ScreenPointToRay(VMouse.mousePosition);
            RaycastHit[] hits = Physics.RaycastAll(ray);
            float minDistance = float.MaxValue;

            RaycastHit hit = default;
            foreach (RaycastHit ahit in hits)
            {
                if (minDistance > ahit.distance)
                {
                    minDistance = ahit.distance;
                    hit = ahit;
                }
            }

            if (hits.Length > 0) 
                commandManager.executeCmd(new CreateCommand(this, hit.point, hit.normal));
        }

        public Transform createObject()
        {
            return Instantiate(template, transform);
        }
    }
}
