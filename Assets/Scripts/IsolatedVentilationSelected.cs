using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsolatedVentilationSelected : MonoBehaviour
{
    public Animator animIsolatedCap;
    public CameraRotateAround rotateAround;
    public GameObject addCamera;
    public ParticalSystemVentilation systemVentilation;
    public PlayStopWave stopWave;
    public SecondCameraOrtoPosition ortoPosition;

    public void IsolatedCapUp()
    {
        animIsolatedCap.SetBool("IsolatedCapMoveUp", true);
    }

    public void OnMouseEnter()
    {
        animIsolatedCap.SetBool("IsolatedSelected", true);
    }
    public void OnMouseExit()
    {
        animIsolatedCap.SetBool("IsolatedSelected", false);
    }

    public void OnMouseDown()
    {
        rotateAround.enabled = false;
        ortoPosition.WFVariation();
        systemVentilation.ParticalSystemsOn();
        stopWave.EngineeringWaveOnly();
        IsolatedCapUp();
        animIsolatedCap.SetBool("IsolatedDeselect", true);
    }

    public void ReturnIsolatedCap()
    {
        animIsolatedCap.SetBool("IsolatedCapMoveUp", false);
    }
}
