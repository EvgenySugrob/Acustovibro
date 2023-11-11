using Michsky.MUIP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchCamera : MonoBehaviour
{
    public GameObject mainCamera,additionalCamera, modePanel, ventilationType, doorType, mainDoorTg, windowType, radiatorType, ventilationMain,panelVolume, panelVolume1;
    public AudioLowPassFilter lowFilter;
    public Button button3D;
    public ButtonManager newButton3D;
    public Canvas secondCanvas, menuPanel, mainCanvas;
    public EnabledDisabledWindowWavePanel wavePanel;
    public Camera additionalCam;
    public PlayStopWave playStopWave;

    [SerializeField] ProperiesPanel properiesPanel;

    public void ChangeOnAddCamera()
    {
        newButton3D.Interactable(false);
        button3D.interactable = false;
        mainCamera.SetActive(false);
        mainCanvas.enabled = false;
        additionalCamera.SetActive(true);
        secondCanvas.enabled = true;
        menuPanel.worldCamera = additionalCam;
        playStopWave.ShowHide();
    }

    public void CheckIndex(int indexPosition)
    {
        switch (indexPosition)
        {
            case 0:
                Debug.Log("whall back");
                properiesPanel.OpenWhall(0);
                break;
            case 1:
                Debug.Log("Whall Left");
                properiesPanel.OpenWhall(1);

                //modePanel.SetActive(false);
                //doorType.SetActive(true);
                //mainDoorTg.SetActive(true);
                break;
            case 2:
                Debug.Log("Whall Front");
                properiesPanel.OpenWhall(2);

                //modePanel.SetActive(false);
                //ventilationMain.SetActive(true);
                break;
            case 3:
                Debug.Log("Whall Right");
                properiesPanel.OpenWhall(3);

                //modePanel.SetActive(false);
                //windowType.SetActive(true);
                //wavePanel.EnabledWindowToggle();
                break;
        }
        //panelVolume.SetActive(false);
        //panelVolume1.SetActive(true);
        ChangeOnAddCamera();
    }
}
