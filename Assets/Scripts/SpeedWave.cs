using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedWave : MonoBehaviour
{
    public ParticleSystem waveWindow;

    public float speedWaveWooden =0.8f;
    public float speedWaveE1 = 0.3f;

    private void Start()
    {
        var wave = waveWindow.main;
        wave.startSpeed = speedWaveE1;
    }
    public void SetSpeedWaveOnWindow(int correctSpeedType)
    {
        var wave = waveWindow.main;
        switch (correctSpeedType)
        {

            case 0:
                wave.startSpeed = speedWaveE1;
                break;
            case 1:
                wave.startSpeed = speedWaveE1;
                break;
            case 2:
                wave.startSpeed = speedWaveWooden;
                break;
            default:
                break;
        }
    }
}
