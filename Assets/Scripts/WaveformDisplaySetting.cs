using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveformDisplaySetting : MonoBehaviour
{
    public Toggle fallingTg, reflectionTg, longitTg, doorWaveTg, windowWaveTg, windowLongitTg,ventilationWaveOutTg;
    public ParticleSystem soundWaveOut, soundWaveReflection, longitWaveIn, longitWaveOut, doorWavePs, ventilationWaveOut;
    public WindowParticalSystemOnOff windowPartical;
    public PassingWaveWindow passingWave;

    public void EnabledFallingWave(bool fallState)
    {
        if (fallState)
        {
            soundWaveOut.Play();
        }
        else
        {
            soundWaveOut.Stop();
        }
    }
    public void EnabledReflectionWave(bool reflectionState)
    {
        if (reflectionState)
        {
            soundWaveReflection.Play();
        }
        else
        {
            soundWaveReflection.Stop();
        }
    }
    public void EnabledLogitWave(bool longState)
    {
        if (longState)
        {
            longitWaveIn.Play();
            longitWaveOut.Play();
        }
        else
        {
            longitWaveIn.Stop();
            longitWaveOut.Stop();
        }
    }
    public void EnabledDoorWave(bool doorWaveState)
    {
        if (doorWaveState)
        {
            doorWavePs.Play();
        }
        else
        {
            doorWavePs.Stop();
        }
    }
    public void WindowWaveEnabled(bool windowWaveState)
    {
        if (windowWaveState)
        {
            windowPartical.WindowPSEnabled();
        }
        else
        {
            windowPartical.AllWindowPSDisabled();
        }
    }
    public void PassingWindowEnabled(bool passingWindowWaveState)
    {
        if (passingWindowWaveState)
        {
            passingWave.WaveGlassOn();
        }
        else
        {
            passingWave.WaveGlassOff();
        }
    }
    public void VentilationWaveOut(bool ventilationWaveOutState)
    {
        if (ventilationWaveOutState)
        {
            ventilationWaveOut.Play();
        }
        else
        {
            ventilationWaveOut.Stop();
        }
    }



    public void DoorWaveToggleDisable()
    {
        doorWaveTg.isOn = false;
    }

    public void AllDisable() 
    {
        ventilationWaveOutTg.isOn = false;
        fallingTg.isOn = false;
        reflectionTg.isOn = false;
        longitTg.isOn = false;
        doorWaveTg.isOn = false;
        windowWaveTg.isOn = false;
        windowLongitTg.isOn = false;
    }

}
