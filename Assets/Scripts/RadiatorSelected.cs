using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiatorSelected : MonoBehaviour
{
    public GameObject pauseMenu, radiatorType, modePanel;
    public Animator animator;
    public CameraRotateAround rotateAround;
    public GameObject addCamera,waveBt;
    public WaveShowHide waveShow;
    public PlayStopWave stopWave;
    public AdditionalCameraEngeneerOrtoPosition ortoPosition;

    public EnableDisableParticleSystem enableDisablePS;

    public HintsOffOn hintsOff;

    public ReturnPastView buttonReturnPastView;

    private Vector3 target;
    private Quaternion rotationTarget;

    [SerializeField] ProperiesPanel properiesPanel;
    private bool _placeModIsActive;

    private void Start()
    {
        target = new Vector3(0.095f, -0.628f, -5.982f);
        rotationTarget = Quaternion.Euler(0f, 180f, 0f);
    }

    public void OnMouseEnter()
    {
        if (!_placeModIsActive)
            animator.SetBool("RadiatorSelected", true);
    }
    public void OnMouseExit()
    {
        if (!_placeModIsActive)
            animator.SetBool("RadiatorSelected", false);
    }

    public void OnMouseDown()
    {
        if (!_placeModIsActive)
        {
            animator.SetBool("Return", false);
            modePanel.SetActive(false);
            buttonReturnPastView.modePanelNeeds = true;

            buttonReturnPastView.ReturnButtonPastViewOn();
            radiatorType.SetActive(true);

            waveBt.SetActive(false);
            hintsOff.RadiatorHintsOff();

            animator.SetBool("RadiatorFocus_b", true);
            enableDisablePS.DisableEnablePS();
            rotateAround.enabled = false;
            waveShow.PlayWaveOnRadiator();
            addCamera.transform.localPosition = target;
            addCamera.transform.localRotation = rotationTarget;
            stopWave.EngineeringWaveOnly();

            ortoPosition.CameraPositionEngeneerConstruction(true, 0.5f);

            properiesPanel.RadiatorOpen(true);
        }
       
    }

    public void PlaceActive()
    {
        _placeModIsActive = !_placeModIsActive;
    }
}
