using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableDisableParticleSystem : MonoBehaviour
{
    [SerializeField]
    public GameObject soundWaveOut, soundWaveReflection, LongitudinalWaveIn, LongitudinalWaveOut, ventilationPSUp, ventilationPSDown,ventilationWaveOut;
    public GameObject particalSystemDoor, windowLeft, windowRight, windowUp, glassUp, glassDown, glassLeft, glassMid, glassRight, glassWooden;

    public void DisablePartical(bool stateSystem)
    {
        soundWaveOut.SetActive(stateSystem);
        soundWaveReflection.SetActive(stateSystem);
        LongitudinalWaveIn.SetActive(stateSystem);
        LongitudinalWaveOut.SetActive(stateSystem);
        //ventilationPSUp.SetActive(stateSystem);
        //ventilationPSDown.SetActive(stateSystem);
        ventilationWaveOut.SetActive(stateSystem);
        particalSystemDoor.SetActive(stateSystem);
        windowLeft.SetActive(stateSystem);
        windowRight.SetActive(stateSystem);
        windowUp.SetActive(stateSystem);
        glassUp.SetActive(stateSystem);
        glassDown.SetActive(stateSystem);
        glassLeft.SetActive(stateSystem);
        glassMid.SetActive(stateSystem);
        glassRight.SetActive(stateSystem);
        glassWooden.SetActive(stateSystem);
}

    public void DisableSate()
    {
        DisablePartical(false);
    }
    public void EnabledState()
    {
        DisablePartical(true);
    }

    public void DisableEnablePS()
    {
        DisableSate();
        EnabledState();
    }
}
