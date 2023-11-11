using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableDisableAnalyzer : MonoBehaviour
{
    [SerializeField] GameObject analyzer;
    [SerializeField] GameObject onButton;
    [SerializeField] ControlAssistence controlAssistence;

    [SerializeField] WhallSlotCheck whallSlotCheck;


    [SerializeField] CameraRaycast cameraRaycast;
    [SerializeField] List<GetSlotSonata> getSlotSonataList;

    [SerializeField] ShtekerAnim shtekerAnim;
    [SerializeField] AntenaAnim antenaAnim;
    private bool isAssistanceActive = false;

    public void EnableDisableAssistance()
    {
        cameraRaycast.AnalayzerOpen();
        isAssistanceActive = !isAssistanceActive;


        if (isAssistanceActive == false)
        {
            cameraRaycast.AnalayzerClose();

            antenaAnim.Diactivate();
            shtekerAnim.Diactivate();
            if (controlAssistence.ReturnActivAnalayzerOrNot())
            {
                controlAssistence.AnalyzerOnOff();
            }
        }

        //whallSlotCheck.enabled = isAssistanceActive;
        
        analyzer.SetActive(isAssistanceActive);
        onButton.SetActive(isAssistanceActive);
    }


    public void VibroModeActive(bool isOn)
    {
        if (isOn == false) 
        {
            foreach (GetSlotSonata item in getSlotSonataList)
            {
                item.EnableDisableMoveToPoint();
            }
        }
        whallSlotCheck.enabled= isOn;
        whallSlotCheck.EneableIndicationMode(isOn);
    }
}
