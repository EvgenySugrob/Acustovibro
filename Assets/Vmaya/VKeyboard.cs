using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Vmaya
{
    public class VKeyboard
    {
        internal static bool anyKey => Keyboard.current.anyKey.isPressed;

#if ENABLE_INPUT_SYSTEM
        public static bool GetKey(Key value)
        {
            return Keyboard.current[value].isPressed;
        }

        public static bool GetKeyDown(Key value)
        {
            return Keyboard.current[value].wasPressedThisFrame;
        }

        public static bool GetKeyUp(Key value)
        {
            return Keyboard.current[value].wasReleasedThisFrame;
        }
#else
        public static bool GetKey(KeyCode value)
        {
            return Input.GetKey(value);
        }

        public static bool GetKeyDown(KeyCode value)
        {
            return Input.GetKeyDown(value);
        }

        public static bool GetKeyUp(KeyCode value)
        {
            return Input.GetKeyUp(value);
        }
#endif
    }
}
