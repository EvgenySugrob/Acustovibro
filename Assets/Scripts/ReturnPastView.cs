using Michsky.MUIP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnPastView : MonoBehaviour
{
    public GameObject radiatorType, ventilationType, returnBt, returnPastViewScene, modePanel;

    public Animator radiatorAnimator;
    public ParticalSystemVentilation particalSystemVentilation;

    public RadiatorSelected radiatorSelected;
    public VentilationSelected ventilationSelected;

    public HintsOffOn hintsOffOn;

    public CameraMode cameraMode;

    public WaveShowHide waveShowHideRadiator;

    public bool modePanelNeeds = false;

    [SerializeField] ProperiesPanel properiesPanel;
    [SerializeField] ButtonManager buttonManager;

    public void ReturnPastSceneView()
    {
        if (modePanelNeeds)
        {
            CloseTypeConstructions(false, false, true, true, false);
        }
        else
        {
            CloseTypeConstructions(false, false, true, false, false);
        }
        
        if (properiesPanel.RadiatorIsOpen()) 
        {
            properiesPanel.RadiatorOpen(false);
        }
        if (properiesPanel.VentilationIsOpen())
        {
            properiesPanel.VentilationOpen(false);
        } 
    }
    public void CloseTypeConstructions(bool radiatorState, bool ventilationState, bool returnBtState, bool modePanelState, bool returnViewBtState)
    {
        radiatorType.SetActive(radiatorState);
        ventilationType.SetActive(ventilationState);
        returnBt.SetActive(returnBtState);
        modePanel.SetActive(modePanelState);
        returnPastViewScene.SetActive(returnViewBtState);

        radiatorAnimator.SetBool("RadiatorFocus_b", false);
        radiatorAnimator.SetBool("Return", true);

        particalSystemVentilation.ParticleSystemOff();
        cameraMode.SwitchCameraMode3D();
        waveShowHideRadiator.StopWaveOnRadiator();
        ventilationSelected.ReturnCap();

        hintsOffOn.RadiatorHintsOn();
        buttonManager.DisableObjectSelf(buttonManager.gameObject);
    }
    public void ReturnButtonPastViewOn()
    {
        returnPastViewScene.SetActive(true);
        returnBt.SetActive(false);
    }
}
