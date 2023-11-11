using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static Vmaya.UI.Tools.ToolToggle;

namespace Vmaya.UI.Tools {
    public class ToolKeySupport : MonoBehaviour
    {
        private ToolToggle[] getItems()
        {
            return GetComponentsInChildren<ToolToggle>(true);
        }

        public static void checkKeyDown(ToolToggle item)
        {
            void checkFKey(Key code)
            {
                if (VKeyboard.GetKeyDown(code)) item.toggle.isOn = !item.toggle.isOn;
            }

            if ((item.item.number > 0) && (item.useKeyboard > useKeyboardType.None))
            {
                if (item.useKeyboard == useKeyboardType.F)
                {
                    switch (item.item.number)
                    {
                        case 1: checkFKey(Key.F1); break;
                        case 2: checkFKey(Key.F2); break;
                        case 3: checkFKey(Key.F3); break;
                        case 4: checkFKey(Key.F4); break;
                        case 5: checkFKey(Key.F5); break;
                        case 6: checkFKey(Key.F6); break;
                        case 7: checkFKey(Key.F7); break;
                        case 8: checkFKey(Key.F8); break;
                        case 9: checkFKey(Key.F9); break;
                        case 10: checkFKey(Key.F10); break;
                        case 11: checkFKey(Key.F11); break;
                        case 12: checkFKey(Key.F12); break;
                    }

                }
                else
                {
                    if ((item.useKeyboard == useKeyboardType.WithLeftAlt) && !VKeyboard.GetKey(Key.LeftAlt)) return;
                    else if ((item.useKeyboard == useKeyboardType.WithLeftShift) && !VKeyboard.GetKey(Key.LeftShift)) return;
                    else if ((item.useKeyboard == useKeyboardType.WithRightAlt) && !VKeyboard.GetKey(Key.RightAlt)) return;
                    else if ((item.useKeyboard == useKeyboardType.WithRightShift) && !VKeyboard.GetKey(Key.RightShift)) return;

                    if (VKeyboard.GetKeyDown(Key.Digit1 + item.item.number - 1))
                        item.toggle.isOn = !item.toggle.isOn;
                }
            }
        }

        private void Update()
        {
            if (VKeyboard.anyKey)
            {
                ToolToggle[] list = getItems();
                foreach (ToolToggle item in list) checkKeyDown(item);
            }
        }
    }
}