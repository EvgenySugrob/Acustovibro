using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowLongitWaveSettingAmplitude : MonoBehaviour
{
    public ParticleSystem wave;
    private float stepAmplitud = 1f;
    public float coefficientX = 0.6f;
    public float coefficientY = 0.8f;

    private void Start()
    {
        wave = GetComponent<ParticleSystem>();
    }
    private void Update()
    {
        var setAmplitude = wave.main;

        if (stepAmplitud <= 0.3f)
        {
            coefficientX = 3f;
            coefficientY = 3f;
}
        else
        {
            coefficientX = 0.6f;
            coefficientY = 0.8f;
        }
        setAmplitude.startSizeX = stepAmplitud * coefficientX;
        setAmplitude.startSizeY = stepAmplitud * coefficientY;


    }

    public void SetAmplitude(float stepSliderAmplitud)
    {
        stepAmplitud = stepSliderAmplitud;
    }
}
