using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeParticleSystemRadiator : MonoBehaviour
{
    public ParticleSystem tubeWaveUp, tubeWaveDown, tubeWaveVertical;

    public void SwitchTypeRadiator(int indexType)
    {

        switch (indexType)
        {
            case 0:
                SetSettingWave(2.5f, 1f,1f,1f);
                break;
            case 1:
                SetSettingWave(4.5f, 1.5f,1.4f,1.24f);
                break;
        }
    }

    public void SetSettingWave(float timeLife,float simulationSpeed, float sizeX,float sizeZ)
    {
        var waveUp = tubeWaveUp.main;
        var waveDown = tubeWaveDown.main;
        var waveVertical = tubeWaveVertical.main;

        waveUp.startLifetime = timeLife;
        waveDown.startLifetime = timeLife;
        waveVertical.startLifetime = timeLife;

        waveUp.simulationSpeed = simulationSpeed;
        waveDown.simulationSpeed = simulationSpeed;
        waveVertical.simulationSpeed = simulationSpeed;
        
        waveUp.startSizeX = sizeX;
        waveDown.startSizeX = sizeX;
        waveVertical.startSizeX = sizeX;

        waveUp.startSizeZ = sizeZ;
        waveDown.startSizeZ = sizeZ;
        waveVertical.startSizeZ = sizeZ;
    }
}
