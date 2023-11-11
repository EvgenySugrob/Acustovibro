using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticalSystemVentilation : MonoBehaviour
{
    public ParticleSystem psUp, psDown, psOut;
    public ReflectionOnVentilation rayUp, rayDown;
    private float minSize = 2.5f;
    private float maxSize = 5f;

    public bool typeVentilationSelection = true;

    public void ParticalSystemsOn()
    {

            psUp.Play();
            psDown.Play();

    }
    public void ParticleSystemOff()
    {
        psUp.Stop();
        psDown.Stop();
        psOut.Stop();
    }

    public void SizeWaveMetalVentilation()
    {
        SizeVentilation(maxSize);
    }
    public void SizeWavePlasticVentilation()
    {
        SizeVentilation(minSize);
    }

    public void SizeVentilation(float size)
    {
        var particleUp = psUp.main;
        var particleDown = psDown.main;

        particleUp.startLifetime = size;
        particleDown.startLifetime = size;
    }
}
