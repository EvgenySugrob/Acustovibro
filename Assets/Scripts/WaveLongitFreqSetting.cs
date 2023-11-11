using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveLongitFreqSetting : MonoBehaviour
{
    public ParticleSystem wave;
    private float stepFreq = 1f;


    private void Start()
    {
        wave = GetComponent<ParticleSystem>();

    }
    private void Update()
    {
        var setFreq = wave.emission;
        setFreq.rateOverTime = stepFreq+1;
    }

    public void SetFrequency(float stepsSliderFreq)
    {
        stepFreq = stepsSliderFreq;
    }
}
