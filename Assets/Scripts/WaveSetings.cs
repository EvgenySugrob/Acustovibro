using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSetings : MonoBehaviour
{
    public ParticleSystem wave;
    private float stepAmplitud = 1f;
    private float stepFreq = 0f;


    private void Start()
    {
        wave = GetComponent<ParticleSystem>();

    }
    private void Update()
    {
        var setAmplitude = wave.main;
        var setFreq = wave.emission;

        setAmplitude.startSize = stepAmplitud;
        setFreq.rateOverTime = stepFreq +1;
        
    }

    public void SetAmplitude(float stepSliderAmplitud)
    { 
        stepAmplitud = stepSliderAmplitud;
    }
    public void SetFrequency(float stepsSliderFreq)
    {
        stepFreq = stepsSliderFreq;
    }
}
