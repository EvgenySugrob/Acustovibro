using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWaveAwayFromMaterial : MonoBehaviour
{
    public ParticleSystem soundWaveAway, soundReflection;

    private float nonMaterialLifetime = 7f;
    private float betonMaterialLifetime = 5.5f;
    private float brickMaterialLifetime = 4.5f;
    private float woodenMaterialLifetime = 3.5f;
    private float drywallMaterialLifetime = 2f;
    private float foamblockMaterialLifetime = 1.5f;

    private float nonMaterialLifetimeRef = 2.5f;
    private float betonMaterialLifetimeRef = 2.3f;
    private float brickMaterialLifetimeRef = 1.9f;
    private float woodenMaterialLifetimeRef = 1.5f;
    private float drywallMaterialLifetimeRef = 1f;
    private float foamblockMaterialLifetimeRef = 0.5f;


    public void SwitchWaveLifetime(int indexMaterialType)
    {
        switch (indexMaterialType)
        {
            case 0:
                NonMaterialSpreadWave();
                break;
            case 1:
                BrickMaterialSpreadWave();
                break;
            case 2:
                BetonMaterialSpreadWave();
                break;
            case 3:
                FoamblockMaterialSpreadWave();
                break;
            case 4:
                DrywallMaterialSpreadWave();
                break;
            case 5:
                WoodMaterialSpreadWave();
                break;
        }
    }
    public void NonMaterialSpreadWave()
    {
        var soundWave = soundWaveAway.main;
        var soundWaveReflection = soundReflection.main;

        soundWaveReflection.startLifetime = nonMaterialLifetimeRef;
        soundWave.startLifetime = nonMaterialLifetime;
    }
    public void BrickMaterialSpreadWave()
    {
        var soundWave = soundWaveAway.main;
        var soundWaveReflection = soundReflection.main;

        soundWaveReflection.startLifetime = brickMaterialLifetimeRef;
        soundWave.startLifetime = brickMaterialLifetime;
    }
    public void BetonMaterialSpreadWave()
    {
        var soundWave = soundWaveAway.main;
        var soundWaveReflection = soundReflection.main;

        soundWaveReflection.startLifetime = betonMaterialLifetimeRef;
        soundWave.startLifetime = betonMaterialLifetime;
    }
    public void FoamblockMaterialSpreadWave()
    {
        var soundWave = soundWaveAway.main;
        var soundWaveReflection = soundReflection.main;

        soundWaveReflection.startLifetime = foamblockMaterialLifetimeRef;
        soundWave.startLifetime = foamblockMaterialLifetime;
    }
    public void DrywallMaterialSpreadWave()
    {
        var soundWave = soundWaveAway.main;
        var soundWaveReflection = soundReflection.main;

        soundWaveReflection.startLifetime = drywallMaterialLifetimeRef;
        soundWave.startLifetime = drywallMaterialLifetime;
    }
    public void WoodMaterialSpreadWave()
    {
        var soundWave = soundWaveAway.main;
        var soundWaveReflection = soundReflection.main;

        soundWaveReflection.startLifetime = woodenMaterialLifetimeRef;
        soundWave.startLifetime = woodenMaterialLifetime;
    }
}
