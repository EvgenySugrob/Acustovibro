using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Vmaya.Util
{
    //Raises an event when some time elapses after the object loses focus
    public class LossFocus : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
    {
        [SerializeField]
        [Tooltip("Wait for seconds after loss focus")]
        private float _waitTimeout;

        [SerializeField]
        [Tooltip("Wait for seconds after show (If 0 then infinite)")]
        private float _waitFocus;

        [SerializeField]
        [Tooltip("Given any keys pressed")]
        private bool _useAnyKey;

        public UnityEvent OnLossFocus;

        private bool _wait;
        private bool _focus;

        private void OnEnable()
        {
            if (_waitFocus > 0)
                StartCoroutine(WaitTimeout(_waitFocus));
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _focus = true;
        }

        private void DoLossFocus()
        {
            if (_useAnyKey && VKeyboard.anyKey)
                StartCoroutine(WaitTimeout(_waitTimeout > 0 ? _waitTimeout : 0.1f));
            else OnLossFocus.Invoke();
        }

        private IEnumerator WaitTimeout(float waitForSeconds)
        {
            _wait = true;
            yield return new WaitForSeconds(waitForSeconds);
            if (!_focus) DoLossFocus();
            _wait = false;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (_waitTimeout > 0) {
                if (!_wait) StartCoroutine(WaitTimeout(_waitTimeout));
            } else DoLossFocus();
            _focus = false;
        }
    }
}
