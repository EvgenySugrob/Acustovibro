using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnUIAnalayzer : MonoBehaviour//, IPointerEnterHandler//, IPointerExitHandler
{
    [SerializeField] WhallSlotCheck whallSlotCheck;
    [SerializeField] CameraRotateAround cameraRotateAround, secondCameraRot;
    //public void OnPointerEnter(PointerEventData eventData)
    //{
    //    Debug.Log("Enter  " + gameObject.name);
    //    whallSlotCheck.enabled = false;
    //}

    //public void OnPointerExit(PointerEventData eventData)
    //{
    //    Debug.Log("Exit  " + gameObject.name);
    //    whallSlotCheck.enabled = true;
    //}
}
