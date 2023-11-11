using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveUpLongitAmplitudeSetting : MonoBehaviour
{
    public ParticleSystem wave;
    private float stepAmplitud = 1f;

    public float coefficientUpWaveX = 0.6f;
    public float coefficientUpWaveY = 0.7f;



    private void Start()
    {
        wave = GetComponent<ParticleSystem>();
    }
    private void Update()
    {
        var setAmplitude = wave.main;

        if (stepAmplitud <= 0.2f)
        {
            coefficientUpWaveX = 3f;
            coefficientUpWaveY = 3f;
        }
        else
        {
            coefficientUpWaveX = 0.6f;
            coefficientUpWaveY = 0.7f;
        }
        setAmplitude.startSizeX = stepAmplitud * coefficientUpWaveX;
        setAmplitude.startSizeY = stepAmplitud * coefficientUpWaveY;


    }

    public void SetAmplitude(float stepSliderAmplitud)
    {
        stepAmplitud = stepSliderAmplitud;
    }
}
