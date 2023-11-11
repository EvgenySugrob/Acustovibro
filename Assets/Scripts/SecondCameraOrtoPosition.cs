using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondCameraOrtoPosition : MonoBehaviour
{
    public GameObject addCamera;
    public int positionVariation;
    private Vector3 startPosition;
    private Quaternion startRotation;

    private void Start()
    {
        startPosition = addCamera.transform.localPosition;
        startRotation = addCamera.transform.localRotation;
    }
    public void CameraVariation(int variation)
    {
        switch (variation)
        {
            case 0:
                WBVariation();
                break;
            case 1:
                WLVariation();
                break;
            case 2:
                WFVariation();
                break;
            case 3:
                WRVariation();
                break;
        }
    }
    public void WBVariation()
    {
        CameraMove(-5.06f,0.1f,-6.96f,0f,90f,0f);
    }
    public void WLVariation()
    {
        CameraMove(7.7f, 0.1f, -5.44f, 0f, 0f, 0f);
    }
    public void WFVariation()
    {
        CameraMove(0.101f, 0.106f, 4.339f, 0f, 90f, 0f);
    }
    public void WRVariation()
    {
        CameraMove(-7.48f, 0.1f, -4.07f, 0f, 0f, 0f);
    }

    public void CameraMove(float transformX,float transfowrY, float transformZ, float rotationX, float rotationY, float rotationZ)
    {
        addCamera.transform.localPosition = new Vector3(transformX, transfowrY, transformZ);
        addCamera.transform.localRotation = Quaternion.Euler(rotationX,rotationY,rotationZ);
    }

    public void ReturnAddCameraStartPosition()
    {
        addCamera.transform.localPosition = startPosition;
        addCamera.transform.localRotation = startRotation;
    }

    public void VentilationOrtoSelection()
    {
        CameraMove(0.101f, 0.106f, 4.339f, 0f, 0f, 0f);
    }
}