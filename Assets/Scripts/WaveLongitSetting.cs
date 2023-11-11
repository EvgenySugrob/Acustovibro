using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveLongitSetting : MonoBehaviour
{
    public ParticleSystem wave;
    private float stepAmplitud = 1f;
    public float coefficientX = 3f;
    public float coefficientY = 2.5f;


    private void Start()
    {
        wave = GetComponent<ParticleSystem>();

    }
    private void Update()
    {
        var setAmplitude = wave.main;

        if (stepAmplitud <= 0.15f)
        {
            coefficientX = 5f;
            coefficientY = 5f;
        }
        else
        {
            coefficientX = 3f;
            coefficientY = 2.5f;
        }
        setAmplitude.startSizeX = stepAmplitud*coefficientX;
        setAmplitude.startSizeY = stepAmplitud*coefficientY;

        
    }

    public void SetAmplitude(float stepSliderAmplitud)
    {
        stepAmplitud = stepSliderAmplitud;
    }
}
