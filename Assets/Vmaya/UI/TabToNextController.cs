using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Vmaya.UI
{
    public class TabToNextController : MonoBehaviour, IUpdateSelectedHandler
    {
        public Selectable nextField;

        public void OnUpdateSelected(BaseEventData data)
        {
            if (VKeyboard.GetKeyDown(Key.Tab))
                nextField.Select();
        }
    }
}
