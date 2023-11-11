using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VentilationSelected : MonoBehaviour
{
    public Animator animCap;
    public CameraRotateAround rotateAround;
    public GameObject addCamera, waveBt,pauseMenu, ventilationType;
    public ParticalSystemVentilation systemVentilation;
    public PlayStopWave stopWave;
    public SecondCameraOrtoPosition ortoPosition;
    public AdditionalCameraEngeneerOrtoPosition additionalCameraEngeneerOrtoPosition;
    public EnableDisableParticleSystem enableDisablePS;

    public ReturnPastView returnPastView;

    [SerializeField] ProperiesPanel properiesPanel;

    private bool _placeModIsActive;


    public void CapUp()
    {
        if (!_placeModIsActive)
        {
            animCap.SetBool("CapMove", true);
        }
        
    }

    public void OnMouseEnter()
    {
        if (!_placeModIsActive)
        {
            animCap.SetBool("CapSelect", true);
        }
       
    }
    public void OnMouseExit()
    {
        if (!_placeModIsActive)
        {
            animCap.SetBool("CapSelect", false);
        }
        
    }

    public void OnMouseDown()
    {
        if (!_placeModIsActive)
        {
            if (systemVentilation.typeVentilationSelection)
            {
                systemVentilation.ParticalSystemsOn();
            }
            returnPastView.modePanelNeeds = false;
            returnPastView.ReturnButtonPastViewOn();
            ventilationType.SetActive(true);
            waveBt.SetActive(false);
            stopWave.EngineeringWaveOnly();
            enableDisablePS.DisableEnablePS();
            rotateAround.enabled = false;
            ortoPosition.VentilationOrtoSelection();
            CapUp();
            additionalCameraEngeneerOrtoPosition.CameraPositionEngeneerConstruction(true, 1.3f);

            properiesPanel.VentilationOpen(true);
        }
    }

    public void ReturnCap()
    {
        waveBt.SetActive(true);
        animCap.SetBool("CapMove",false);
    }

    public void PlaceActive()
    {
        _placeModIsActive = !_placeModIsActive;
    }
}
