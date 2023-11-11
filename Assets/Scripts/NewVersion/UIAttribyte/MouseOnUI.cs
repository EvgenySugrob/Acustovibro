using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseOnUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] CameraRaycast cameraRaycast;
    [SerializeField] CameraRotateAround cameraRotateAround, secondCameraRot;
    [SerializeField] bool onUI;
    public void OnPointerEnter(PointerEventData eventData)
    {
        cameraRaycast.enabled = false;
        onUI = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        cameraRaycast.enabled = true;
        onUI = false;
    }

    public bool ReturnStateMouse()
    {
        return onUI;
    }
}
