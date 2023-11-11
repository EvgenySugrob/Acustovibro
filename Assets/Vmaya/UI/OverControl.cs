using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class OverControl : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private bool _isFocus;

    public UnityEvent onFocusChange; 

    public bool isFocus
    {
        get {
            return _isFocus;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _isFocus = true;
        onFocusChange.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _isFocus = false;
        onFocusChange.Invoke();
    }
}
