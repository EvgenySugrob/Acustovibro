using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Vmaya.Scene3D.UI
{
    [RequireComponent(typeof(InputField))]
    public class InputControl : MonoBehaviour, ISelectHandler
    {
        public static bool isFocus;

        private void Awake()
        {
            GetComponent<InputField>().onEndEdit.AddListener(OnEndEdit);
        }

        public void OnSelect(BaseEventData eventData)
        {
            isFocus = true;
        }

        private void OnEndEdit(string text)
        {
            isFocus = false;
        }
    }
}