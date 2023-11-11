using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalCameraEngeneerOrtoPosition : MonoBehaviour
{
    public Camera additionalCam;

    public void CameraPositionEngeneerConstruction(bool camB, float ortoSize)
    {
        additionalCam.orthographic = camB;
        additionalCam.orthographicSize = ortoSize;
        int i = 0;
        i++;
    }
    
}
