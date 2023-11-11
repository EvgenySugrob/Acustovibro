using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Vmaya.UI
{
    public class DblClickUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private int _count=0;

        public float timeOut = 1;
        public UnityEvent onDblClick;
        public void OnPointerDown(PointerEventData eventData)
        {
            _count++;
            Vmaya.Utils.setTimeout(this, () => { _count = 0; }, timeOut);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (_count > 1) DoDblClick();
        }

        protected virtual void DoDblClick()
        {
            onDblClick.Invoke();
        }
    }
}
