using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Vmaya.UI.Components;

namespace Vmaya.UI
{
    public class ShortcutManager : MonoBehaviour
    {
        [System.Serializable]
        public struct ShortcutData
        {
            public Text chortcutLabel;
            public UnityEvent action;
            [HideInInspector]
            public bool Ctrl;
            [HideInInspector]
            public bool Alt;
            [HideInInspector]
            public bool Shift;
            [HideInInspector]
            public Key keyCode;
        }

        [SerializeField]
        private ShortcutData[] shortcutData;

        private void Awake()
        {
            parseShortcut();
        }

        public static void parseShortcut(string shortCutStr, ref ShortcutData result)
        {
            string[] a = shortCutStr.Split('+');
            if (a.Length > 1)
            {
                result.Ctrl = a[0].Trim().ToLower().Equals("ctrl");
                result.Alt = a[0].Trim().ToLower().Equals("alt");
                result.Shift = a[0].Trim().ToLower().Equals("shift");
                result.keyCode = (Key)System.Enum.Parse(typeof(Key), a[1].ToUpper());
            }
            else result.keyCode = (Key)System.Enum.Parse(typeof(Key), shortCutStr);
        }


        private void parseShortcut()
        {
            for (int i=0; i< shortcutData.Length; i++)
                parseShortcut(shortcutData[i].chortcutLabel.text, ref shortcutData[i]);
        }

        private void Update()
        {
            if (!Curtain.isModal && !Vmaya.Scene3D.UI.InputControl.isFocus)
            {
                for (int i = 0; i < shortcutData.Length; i++)
                {
                    if (VKeyboard.GetKeyDown(shortcutData[i].keyCode))
                    {
                        if ((!shortcutData[i].Ctrl || VKeyboard.GetKey(Key.LeftCtrl)) &&
                            (!shortcutData[i].Alt || VKeyboard.GetKey(Key.LeftAlt)) &&
                            (!shortcutData[i].Shift || VKeyboard.GetKey(Key.LeftShift)))
                        {
                            shortcutData[i].action.Invoke();
                        }
                    }
                }
            }
        }
    }
}
