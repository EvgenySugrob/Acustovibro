using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRaycast : MonoBehaviour
{

    public ReverbZoneColliderMove reverbZone;
    public RoomClose roomClose;
    public ReverbZoneClose reverbClose;
    public SwitchCamera switchCamera;
    public ParticalSystemsMove particalSystemsMove;
    public CameraRotateAround cameraRotate;
    public Transform whallBack, whallFront, whallRight, whallLeft;
    public CameraMode cameraModeIndex;
    public ReverbZoneTr reverbZoneTr;
    public SecondCameraOrtoPosition secondCameraOrtoPosition;

    public PlayStopWave playStopWave;


    public GameObject pauseMenu,panelSetingWave;

    public ProperiesPanel properiesPanel;
    [SerializeField] LayerMask layerMask;
    [SerializeField]private LayerMask _baseLayer;

    private bool _placeModIsActive;

    private void Update()
    {
        if (!_placeModIsActive)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


            RaycastHit hit;
            if (Physics.Raycast(ray, out hit,layerMask))
            {
                WhallRightMove whallRightMove = hit.collider.gameObject.GetComponent<WhallRightMove>();
                WhallLeftMove whallLeftMove = hit.collider.gameObject.GetComponent<WhallLeftMove>();
                WhallFrontMove whallFrontMove = hit.collider.gameObject.GetComponent<WhallFrontMove>();
                ColliderRay colliderRay = hit.collider.gameObject.GetComponent<ColliderRay>();

                if (whallRightMove)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        //whallRight окно
                        reverbZoneTr.reverbChangeIsNow = false;
                        roomClose.CloseRoom();
                        roomClose.WhallFrontClose();
                        reverbClose.CloseReverbZone();
                        whallRightMove.WhallForward();
                        reverbZone.RightColliderForward();
                        particalSystemsMove.RotationOnWhallRight();
                        cameraModeIndex.cameraPositionIndex = 3;
                        switchCamera.CheckIndex(cameraModeIndex.cameraPositionIndex);

                        secondCameraOrtoPosition.CameraVariation(cameraModeIndex.cameraPositionIndex);
                        CameraWRTarget();

                    }
                    if (Input.GetMouseButtonDown(1))
                    {
                        whallRightMove.WhallBack();
                        reverbZone.RightColliderBack();
                    }
                }
                else if (whallLeftMove)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                         //whallLeft дверь
                        reverbZoneTr.reverbChangeIsNow = false;
                        roomClose.CloseRoom();
                        roomClose.WhallFrontClose();
                        reverbClose.CloseReverbZone();
                        whallLeftMove.WhallForward();
                        reverbZone.LeftColliderForward();
                        particalSystemsMove.RotationWhallOnLeft();
                        cameraModeIndex.cameraPositionIndex = 1;
                        switchCamera.CheckIndex(cameraModeIndex.cameraPositionIndex);
                        secondCameraOrtoPosition.CameraVariation(cameraModeIndex.cameraPositionIndex);
                        CameraWLTarget();
                    }
                    if (Input.GetMouseButtonDown(1))
                    {
                        whallLeftMove.WhallBack();
                        reverbZone.LeftColliderBack();
                    }
                }
                else if (whallFrontMove)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                         // whallFront вентиляция
                        reverbZoneTr.reverbChangeIsNow = false;
                        roomClose.CloseRoom();
                        roomClose.WhallFrontClose();
                        reverbClose.CloseReverbZone();
                        whallFrontMove.WhallForward();
                        reverbZone.FrontColliderForward();
                        particalSystemsMove.RotationOnWhallFront();
                        cameraModeIndex.cameraPositionIndex = 2;
                        switchCamera.CheckIndex(cameraModeIndex.cameraPositionIndex);
                        secondCameraOrtoPosition.CameraVariation(cameraModeIndex.cameraPositionIndex);
                        CameraWFTarget();
                    }
                    if (Input.GetMouseButtonDown(1))
                    {
                        whallFrontMove.WhallBack();
                        reverbZone.FrontColliderBack();
                    }
                }
                else if (colliderRay)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        // whallBack радиатор
                        reverbZoneTr.reverbChangeIsNow = false;
                        switchCamera.ChangeOnAddCamera();
                        roomClose.WhallFrontClose();
                        roomClose.CloseRoom();
                        reverbClose.CloseReverbZone();
                        colliderRay.WhallForward();
                        reverbZone.BkColliderForward();
                        particalSystemsMove.RotationOnWhallBack();
                        cameraModeIndex.cameraPositionIndex = 0;
                        switchCamera.CheckIndex(cameraModeIndex.cameraPositionIndex);
                        secondCameraOrtoPosition.CameraVariation(cameraModeIndex.cameraPositionIndex);
                        CameraWBTarget();
                        playStopWave.ShowHide();
                    }
                    if (Input.GetMouseButtonDown(1))
                    {
                        colliderRay.WhallBack();
                        reverbZone.BkColliderBack();
                    }
                }
            }
        }
    }

    public void PlaceActive()
    {
        _placeModIsActive = !_placeModIsActive;
    }
    public void CameraWRTarget()
    {   
        cameraRotate.target = whallRight;
    }
    public void CameraWLTarget()
    {
        cameraRotate.target = whallLeft;
    }
    public void CameraWFTarget()
    {
        cameraRotate.target = whallFront;
    }
    public void CameraWBTarget()
    {
        cameraRotate.target = whallBack;
    }

    public void AnalayzerOpen()
    {
        _baseLayer.value = 8;
        layerMask.value = 0;
    }
    public void AnalayzerClose()
    {
        _baseLayer.value = 0;
        layerMask.value = 8;
    }
}
