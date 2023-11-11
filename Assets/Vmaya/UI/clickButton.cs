using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Vmaya.UI.Components;

namespace Vmaya.UI
{
    public class clickButton : MonoBehaviour
    {
        public Key keyCode = Key.Enter;

        void Update()
        {
            if (VKeyboard.GetKeyDown(keyCode))
            {
                Window window = GetComponentInParent<Window>();
                WindowManager manager = GetComponentInParent<WindowManager>();
                if (window && manager)
                {
                    RectTransform trans = window.GetComponent<RectTransform>();
                    if (trans.GetSiblingIndex() == manager.maxSiblinindex())
                        GetComponent<Button>().onClick.Invoke();
                }
            }
        }
    }
}