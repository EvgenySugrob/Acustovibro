using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class WhallSlotCheck : MonoBehaviour
{
    [SerializeField] MouseOnUI mouseOnUI;

    [Header("Отключение при переходе в режим")]
    [SerializeField] CameraRaycast cameraRaycast;

    [Header("Настройки режима")]
    [SerializeField] Camera cameraPlayer;
    [SerializeField] float distanceRay;
    [SerializeField] LayerMask layerMask;

    [Header("Состояние режима")]
    [SerializeField] bool inIndicationMode;

    //-------------------------------->public
    public void EneableIndicationMode(bool isOn)
    {
        inIndicationMode= isOn;
       // cameraRaycast.enabled= !isOn;
    }

    public void EnableScript()
    { 
        enabled= true;
    }

    //-------------------------------->private
    private void Update()
    {
        if (mouseOnUI.ReturnStateMouse()==false) 
        {
            if (inIndicationMode)
            {
                Ray ray = cameraPlayer.ScreenPointToRay(Mouse.current.position.ReadValue());
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, distanceRay, layerMask))
                {
                    CheckWhall(hit);
                }
            }
        }
        
    }

    private void CheckWhall(RaycastHit raycastHit)
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (raycastHit.transform.GetComponent<GetSlotSonata>())
            {
                GetSlotSonata getSlotSonata = raycastHit.transform.GetComponent<GetSlotSonata>();

                getSlotSonata.ActiveScripts();
                getSlotSonata.SaveStartPositionCamera();
                getSlotSonata.CalculationNearestPoint(raycastHit.point);
                
            }
        }
    }

    
}
