using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PropertiesPanelWarning : MonoBehaviour
{
    [Header("��� ��������")]
    [SerializeField] GameObject propertiesPanelMain;
    [SerializeField] GameObject buttonClose;
    [SerializeField] EnableDisableAnalyzer enableDisableAnalyzer;
    [SerializeField] PropertyPanelProtectedMode propertyPanelProtected;

    [SerializeField] InventoryReplaceItem inventoryReplace;
    [SerializeField] GameObject dialogsMenuObject;
    [Header("��� ���������")]
    [SerializeField] GameObject assistance;
    [SerializeField] DisableCameraControlInPlacementMode disableCameraControl;
    [Header("��� ����������")]
    [SerializeField] GameObject dialogWindow;

    [SerializeField] GameObject secondCamera;
    [SerializeField] ReturnMainCamera returnMainCamera;
    [SerializeField] GameObject propertyPanel;
    [SerializeField] ReturnPastView returnPastView;
    [SerializeField] List<GetSlotSonata> sonataSlotList;
    [SerializeField] CountPlaceDevices countPlaceDevices; //����� �� mainMenu

    [Header("����� ����� ������")]
    [SerializeField] TMP_Text textMessage;

    [SerializeField] bool isProtectedActive;

    public void ActivePanel()
    {
        if (assistance.activeSelf)
        {
            textMessage.text = "��������� ������� � ������ \"���������\" ����������.";
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
                textMessage.text = "��������� � ������ \"������\" ����������";
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
            textMessage.text = "��������� ��������� ������ � ������ \"���������\" ����������.";
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
