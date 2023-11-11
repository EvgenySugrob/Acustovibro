using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProperiesPanel : MonoBehaviour
{
    [SerializeField] GameObject propertiesPanelMainView;
    [SerializeField] RectTransform rectTransformPropertiesPanel;
    [SerializeField] RectTransform rectScrollView;
    [SerializeField] Transform startPositionPanel;
    [SerializeField] Transform endPositionPanel;
    [SerializeField] Transform middlePositionPanel;

    [SerializeField] GameObject mainReturnBt;
    [SerializeField] GameObject mainChildReturnBt;
    [SerializeField] GameObject returnBackChildBt;

    [Header("Смена материала на объектах")]
    [SerializeField] GameObject matRadiator;
    [SerializeField] GameObject matVentilation;
    [SerializeField] GameObject matDoor;
    [SerializeField] GameObject matWindow;

    [Header("Математический вид")]
    [SerializeField] GameObject mathMode;

    [Header("Волна и ее параметры")]
    [SerializeField] GameObject waveSetting;
    [SerializeField] GameObject waveParametrs;

    [Header("Виды волн для отключения")]
    [SerializeField] GameObject windowWave;
    [SerializeField] GameObject windowLongit;
    [SerializeField] GameObject mainDoorTogle;
    [SerializeField] GameObject ventilationWave;

    [Header("Математический вид")]
    [SerializeField] CameraMode cameraMode;
    [SerializeField] ShowHideLine mainLine;
    [SerializeField] ShowHideLine secondLine;

    private float startHeightScrollView = 212f;
    private float endHeightScrollView = 770f;
    private float widthScroll = 480.6172f;
    private float middleHieghtScrollView = 318.4343f;

    private bool radiatorIsOpen;
    private bool ventilationIsOpen;
    private bool mainTypeDoor = true;

    private bool waveBt = true;

    //private metods
    private void ExtensionPropertiesPanel()
    {
        propertiesPanelMainView.transform.position = endPositionPanel.position;
        rectTransformPropertiesPanel.sizeDelta = endPositionPanel.GetComponent<RectTransform>().sizeDelta;

        rectScrollView.sizeDelta = new Vector2(widthScroll, endHeightScrollView);
    }

    private void ConstrictionPropertiesPanel()
    {
        propertiesPanelMainView.transform.position = startPositionPanel.position;
        rectTransformPropertiesPanel.sizeDelta = startPositionPanel.GetComponent<RectTransform>().sizeDelta;

        rectScrollView.sizeDelta = new Vector2(widthScroll, startHeightScrollView);
    }

    private void SetMiddlePositionPanel()
    {
        propertiesPanelMainView.transform.position = middlePositionPanel.position;
        rectTransformPropertiesPanel.sizeDelta = middlePositionPanel.GetComponent<RectTransform>().sizeDelta;

        rectScrollView.sizeDelta = new Vector2(widthScroll, middleHieghtScrollView);
    }

    private void WavePanelActive(bool isActive)
    {
        waveSetting.SetActive(isActive);
        waveParametrs.SetActive(isActive);
    }

    private void WhallLeftPanelOpen(bool isActive)
    {
        matDoor.SetActive(isActive);
        if (mainTypeDoor == false)
        {
            mainDoorTogle.SetActive(true);
        }
    }

    private void WhallRightPanelOpen(bool isActive)
    {
        matWindow.SetActive(isActive);
        windowWave.SetActive(isActive);
        windowLongit.SetActive(isActive);
    }
    private void WhallBackPanelOpen(bool isActive)
    {
        //matRadiator.SetActive(true);
        mathMode.SetActive(isActive);
    }
    private void WhallFrontPanelOpen(bool isActive)
    {
        //matVentilation.SetActive(true); включать на этапе перехода к объекту ближе
        ventilationWave.SetActive(isActive);
    }


    //public metods
    public bool RadiatorIsOpen()
    {
        if (radiatorIsOpen)
        {
            radiatorIsOpen= false;
            return true;
        }
        else 
        {
            return false;
        }
    }

    public bool VentilationIsOpen()
    {
        if (ventilationIsOpen)
        {
            ventilationIsOpen= false;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void MathModeOn(bool isActive)
    {
        WavePanelActive(!isActive);
        mainReturnBt.SetActive(!isActive);
        if (isActive)
        {
            cameraMode.SwitchCamearMode2D();
            mainLine.ShowLineRender();
            secondLine.ShowLineRender();
            SetMiddlePositionPanel();
        }
        else
        {
            cameraMode.SwitchCameraMode3D();
            mainLine.HideLine();
            secondLine.HideLine();
            ExtensionPropertiesPanel();
        }
    }

    public void VentilationOpen(bool isActive)
    {
        ventilationIsOpen = isActive;

        returnBackChildBt.SetActive(isActive);

        WavePanelActive(!isActive);

        matVentilation.SetActive(isActive);
        if (isActive)
        {
            SetMiddlePositionPanel();
        }
        else
        {
            ExtensionPropertiesPanel();
        }
    }

    public void RadiatorOpen(bool isActive)
    {
        radiatorIsOpen= isActive;

        returnBackChildBt.SetActive(isActive);

        WavePanelActive(!isActive);
        mathMode.SetActive(!isActive);
        matRadiator.SetActive(isActive);
        if (isActive)
        {
            SetMiddlePositionPanel();
        }
        else
        {
            ExtensionPropertiesPanel();
        }
    }

    public void DoorSwapOpenWaveTg(int index)
    {
        switch (index)
        {   
            case 0:

                if (mainTypeDoor == false)
                {
                    mainDoorTogle.SetActive(false);
                    mainTypeDoor = true;
                }

                break;
            case 1:

                if (mainTypeDoor == true) 
                {
                    mainDoorTogle.SetActive(true);
                    mainTypeDoor = false;
                }

                break;
        }
    }

    public void OpenWhall(int indexWhall)
    {
        mainReturnBt.SetActive(true);
        mainChildReturnBt.SetActive(true);
        ExtensionPropertiesPanel();
        WavePanelActive(true);
        switch (indexWhall)
        {
            case 0:
                WhallBackPanelOpen(true);
                break;
            case 1:
                WhallLeftPanelOpen(true);
                break;
            case 2:
                WhallFrontPanelOpen(true);
                break;
            case 3:
                WhallRightPanelOpen(true);
                break;
        }
    }

    public void CloseWhall()
    {
        WavePanelActive(false);

        WhallBackPanelOpen(false);
        WhallLeftPanelOpen(false);
        WhallFrontPanelOpen(false);
        WhallRightPanelOpen(false);

        ConstrictionPropertiesPanel();

        mainReturnBt.SetActive(false);
    }

    public void WaveBtClick()
    {
        waveBt = !waveBt;

        if (waveBt) 
        {
            ExtensionPropertiesPanel();
            waveParametrs.SetActive(true);
        }
        else
        {
            waveParametrs.SetActive(false);
            SetMiddlePositionPanel();
        }

    }
        
}
