using Michsky.MUIP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMode : MonoBehaviour
{
    public Camera cam;
    public GameObject additionCamera, wave, waveButton;
    public CameraRotateAround cameraRotate;
    public Button button3D, button2D;
    public ButtonManager newButton3D, newButton2D;
    public WaveShowHide stopWave;
    public PlayStopWave playStopWave;
    public SecondCameraOrtoPosition secondCameraOrtoPosition;
    public EnableDisableParticleSystem enableDisablePS;
    public int cameraPositionIndex;

    public void SwitchCamearMode2D()
    {
        SwitchCameras(false, true, true, false, 1.3f,false,false);
        stopWave.StopWave();
        EnabledSetingPanel();
        secondCameraOrtoPosition.CameraVariation(cameraPositionIndex);
        enableDisablePS.DisableSate();
    }
    public void SwitchCameraMode3D()
    {
        SwitchCameras(true, false, false, true, cam.orthographicSize,true,true);
        enableDisablePS.EnabledState();
        playStopWave.ShowHide();
    }

    private void StopWave()
    {
        stopWave.enabled=false;
    }

    public void SwitchCameras( bool cameraRotateB, bool camB, bool button3DB, bool button2DB, float ortSize,bool waveB, bool waveButtonB)
    {
        cameraRotate.enabled = cameraRotateB;
        cam.orthographic = camB;
        cam.orthographicSize = ortSize;
        newButton3D.Interactable(button3DB);
        newButton2D.Interactable(button2DB);
        button3D.interactable = button3DB;
        button2D.interactable = button2DB;
        wave.SetActive(waveB);
        waveButton.SetActive(waveButtonB);

    }

    private void EnabledSetingPanel()
    {
        playStopWave.ReturnButtonClick();
    }
}
