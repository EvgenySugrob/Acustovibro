using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PropertiesPanelWarning : MonoBehaviour
{
    [Header("Что включать")]
    [SerializeField] GameObject propertiesPanelMain;
    [SerializeField] GameObject buttonClose;
    [SerializeField] EnableDisableAnalyzer enableDisableAnalyzer;
    [SerializeField] PropertyPanelProtectedMode propertyPanelProtected;

    [SerializeField] InventoryReplaceItem inventoryReplace;
    [SerializeField] GameObject dialogsMenuObject;
    [Header("Что проверять")]
    [SerializeField] GameObject assistance;
    [SerializeField] DisableCameraControlInPlacementMode disableCameraControl;
    [Header("Что показывать")]
    [SerializeField] GameObject dialogWindow;

    [SerializeField] GameObject secondCamera;
    [SerializeField] ReturnMainCamera returnMainCamera;
    [SerializeField] GameObject propertyPanel;
    [SerializeField] ReturnPastView returnPastView;
    [SerializeField] List<GetSlotSonata> sonataSlotList;
    [SerializeField] CountPlaceDevices countPlaceDevices; //висит на mainMenu

    [Header("Какой текст менять")]
    [SerializeField] TMP_Text textMessage;

    [SerializeField] bool isProtectedActive;

    public void ActivePanel()
    {
        if (assistance.activeSelf)
        {
            textMessage.text = "Изменение свойств в режиме \"Измерение\" недоступно.";
            dialogWindow.SetActive(true);
        }
        else
        {
            propertiesPanelMain.SetActive(true);
            buttonClose.SetActive(true);
        }
    }

    public void ActiveAnalayzer()
    {
        if (IsActiveVibroModeCheck() == false)
        {
            if (disableCameraControl.ReturnIsActive() == false)
            {
                textMessage.text = "Измерение в режиме \"Защита\" недоступно";
                dialogWindow.SetActive(true);
            }
            else
            {
                if (secondCamera.activeSelf)
                {
                    returnPastView.ReturnPastSceneView();
                    returnMainCamera.ReturnCamera();
                }
                propertiesPanelMain.SetActive(false);
                //dialogsMenuReplace.OpenCloseDialogsMenu(false);
                enableDisableAnalyzer.EnableDisableAssistance();
            }
        }
        
    }

    public void ActiveProtected()
    {
        if (disableCameraControl.ReturnIsActive() && inventoryReplace.enabled == true && dialogsMenuObject.activeSelf==false)
        {
            EnterExitProtectedMode();
        }
        else if(disableCameraControl.ReturnIsActive() == false && inventoryReplace.enabled == true && dialogsMenuObject.activeSelf == false)
        {
            EnterExitProtectedMode();
        }

    }

    private bool IsActiveVibroModeCheck()
    {
        bool activeVibroModeCheck = false;
        foreach (GetSlotSonata slot in sonataSlotList)
        {
            if (slot.ReturnAnalyzerIsCheckThisPoint())
            {
                activeVibroModeCheck = true;
                break;
            }
        }
        return activeVibroModeCheck;
    }

    private void EnterExitProtectedMode()
    {
        if (assistance.activeSelf)
        {
            textMessage.text = "Установка устройств защиты в режиме \"Измерение\" недоступна.";
            dialogWindow.SetActive(true);
        }
        else
        {
            if (secondCamera.activeSelf)
            {
                returnPastView.ReturnPastSceneView();
                returnMainCamera.ReturnCamera();
            }
            countPlaceDevices.SoundGeneratorOnAnalayzer();
            propertiesPanelMain.SetActive(false);
            disableCameraControl.PlaceModActivation();
            propertyPanelProtected.ProtectedModeActive();
        }
    }
}
