using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveShowHide : MonoBehaviour
{
    public ParticleSystem waveWbIn, waveWbOut, innerWave, outWave, radiatorWave, trubeWaveUp, trubeWaveDown,trubeWaveVertical;

    public void StartWave()
    {
        innerWave.Play();
    }
    public void StopWave()
    {
        waveWbIn.Stop();
        waveWbOut.Stop();
        innerWave.Stop();
        outWave.Stop();
    }

    public void PlayWaveOnRadiator()
    {
        radiatorWave.Play();
        trubeWaveUp.Play();
        trubeWaveDown.Play();
        trubeWaveVertical.Play();
    }
    public void StopWaveOnRadiator()
    {
        radiatorWave.Stop();
        trubeWaveUp.Stop();
        trubeWaveDown.Stop();
        trubeWaveVertical.Stop();

    }
}
