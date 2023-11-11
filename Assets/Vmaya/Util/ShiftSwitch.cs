using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Vmaya.Util
{
    public class ShiftSwitch: MonoBehaviour
    {
        [SerializeField]
        private MonoBehaviour component1;
        [SerializeField]
        private MonoBehaviour component2;
        [SerializeField]
        private bool invert = false;

        private void Update()
        {

            if (VKeyboard.GetKey(Key.LeftShift))
            {
                if (component1.enabled)
                {
                    component1.enabled = invert;
                    component2.enabled = !invert;
                }
            } else
            {
                if (!component1.enabled)
                {
                    component2.enabled = invert;
                    component1.enabled = !invert;
                }
            }
        }
    }
}
