using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowParticalSystemOnOff : MonoBehaviour
{
    public ParticleSystem windowPSLeft, windowPSRight, windowPSUp;

    public void AllWindowPSDisabled()
    {
        windowPSLeft.Stop();
        windowPSRight.Stop();
        windowPSUp.Stop();
    }
    public void WindowPSEnabled()
    {
        windowPSUp.Play();
        windowPSLeft.Play();
        windowPSRight.Play();
    }

}
