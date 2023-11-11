using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassingWaveWindow : MonoBehaviour
{
    public ParticleSystem waveGlassUp, waveGlassWooden, waveGlassDown, waveGlassLeft, waveGlassMid, waveGlassRight;

    public void WaveGlassOn()
    {
        waveGlassUp.Play();
        waveGlassDown.Play();
        waveGlassLeft.Play();
        waveGlassMid.Play();
        waveGlassRight.Play();
        waveGlassWooden.Play();
    }
    public void WaveGlassOff()
    {
        waveGlassUp.Stop();
        waveGlassDown.Stop();
        waveGlassLeft.Stop();
        waveGlassMid.Stop();
        waveGlassRight.Stop();
        waveGlassWooden.Stop();
    }
    public void WaveGlassWoodenOn()
    {
        waveGlassWooden.Play();
    }
    public void WaveGlassWoodenOff()
    {
        waveGlassWooden.Stop();
    }

    public void AllWaveOff()
    {
        waveGlassUp.Stop();
        waveGlassDown.Stop();
        waveGlassLeft.Stop();
        waveGlassMid.Stop();
        waveGlassRight.Stop();
        waveGlassWooden.Stop();
    }
}
