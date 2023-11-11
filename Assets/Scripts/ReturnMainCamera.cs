using Michsky.MUIP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReturnMainCamera : MonoBehaviour
{
    public GameObject mainCamera, additionalCamera, modePanel, ventilationType, ventilationWaveMain, doorType, mainDoorTg, windowType, radiatorType, ventilationCap, panelVolume, panelVolume1;
    public AudioLowPassFilter lowFilter;
    public ShowHideLine showMainLine, showSecondLine;
    public Button button2D,button3D;
    public ButtonManager newButton3D, newButton2D;
    public CameraMode cameraMode;
    public PlayStopWave playStop;
    public Canvas secondCanvas, mainCanvas, canvasMenu;
    public Camera cameraMain;
    public Animator animator;
    public VentilationSelected ventilationSelected;
    public ParticalSystemVentilation particalSystemVentilation;
    public EnabledDisabledWindowWavePanel enabledDisabledWindowWavePanel;
    public WindowParticalSystemOnOff windowPartical;
    public PassingWaveWindow passingWaveWnidow;

    public RoomClose roomClose;
    public ReverbZoneClose reverbZoneClose;

    public EnableDisableParticleSystem enableDisablePS;
    public HintsOffOn hintsOn;
    public SliderDefualtPosition sliderDefualtPosition;
    public ReverbZoneTr reverbZoneTr;
    public SecondCameraOrtoPosition secondCameraOrtoPosition;
    [SerializeField] ProperiesPanel properiesPanel;
    [SerializeField] AudioSource audioSource;


    public void ReturnCamera()
    {
        playStop.ReturnButtonClick();
        secondCameraOrtoPosition.ReturnAddCameraStartPosition();

        //panelVolume.SetActive(true);
        //panelVolume1.SetActive(false);

        sliderDefualtPosition.changeIsNow = true;
        reverbZoneTr.reverbChangeIsNow = true;

        roomClose.CloseRoom();
        //hintsOn.MainHintsOn();


        reverbZoneClose.CloseReverbZone();

        doorType.SetActive(false);
        mainDoorTg.SetActive(false);
        ventilationWaveMain.SetActive(false);

        radiatorType.SetActive(false);
        cameraMode.SwitchCameraMode3D();
        newButton2D.Interactable(true);
        newButton3D.Interactable(false);
        button2D.interactable = true;
        button3D.interactable = false;
        mainCamera.SetActive(true);
        mainCanvas.enabled = true;
        additionalCamera.SetActive(false);
        lowFilter.enabled = true;
        showMainLine.HideLine();
        showSecondLine.HideLine();
        playStop.ReturnButtonClick();
        secondCanvas.enabled = false;
        modePanel.SetActive(true);
        particalSystemVentilation.ParticleSystemOff();
        windowPartical.AllWindowPSDisabled();
        passingWaveWnidow.AllWaveOff();
        enabledDisabledWindowWavePanel.DisabledWindowToggle();
        windowType.SetActive(false);

        audioSource.volume = 0.5f;

        properiesPanel.CloseWhall();

        StopParticleSystem();

        canvasMenu.worldCamera = cameraMain;
        transform.GetComponent<ButtonManager>().DisableObjectSelf(transform.gameObject);
    }

    public void StopParticleSystem()
    {
        enableDisablePS.DisableSate();
        enableDisablePS.EnabledState();
    }
}
