using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingWaveDisplayOnWindow : MonoBehaviour
{
    public PassingWaveWindow passingWaveWindow;
    public WindowParticalSystemOnOff windowPartical;
    public ParticleSystem windowPSLeft, windowPSRight, windowPSUp, passingWaveWoodenGlass;
    public GameObject windowGlassPSLeft, windowGlassPSRight, waveMid;

    private Vector3 startPositionWaveLeft, startPositionWaveRight, startPositionWaveMid;
    private Vector3 targetPositionWaveLeft = new Vector3(0f, 0.267f, -0.119f);
    private Vector3 targetPositionWaveRight = new Vector3(0f, -0.2500001f, -0.119f);
    private Vector3 targetPositionWaveMid = new Vector3(-0.046f, 0f, 0.1f);
    private float startSize = 0.8f;
    private float targetSize = 0.6f;
    private float targetSizeWoodenWave = 2f;
    private float targetSizeWaveUp = 5f;
    private float startSizeWave = 0f;


    private void Start()
    {
        startPositionWaveLeft = windowGlassPSLeft.transform.localPosition;
        startPositionWaveRight = windowGlassPSRight.transform.localPosition;
        startPositionWaveMid = waveMid.transform.localPosition;
    }
    public void SettingWave(int typeWindow)
    {
        switch (typeWindow)
        {
            case 0:
                SetTargetPositionWaveMid();
                SetStartSizeAndPosition();
                break;
            case 1:
                SetTargetPositionWaveMid();
                SetStartSizeAndPosition();
                break;
            case 2:
                SetStartPositionWaveMid();
                SetTargetSizeAndPosition();
                break;
        }
    }
    public void SetTargetSizeAndPosition()
    {
        var leftWave = windowPSLeft.main;
        var rightWave = windowPSRight.main;
        var woodenWave = passingWaveWoodenGlass.main;
        var waveUp = windowPSUp.main;

        leftWave.startSizeY = targetSize;
        rightWave.startSizeY = targetSize;
        woodenWave.startLifetime = targetSizeWoodenWave;
        waveUp.startLifetime = targetSizeWaveUp;
        windowGlassPSLeft.transform.localPosition = targetPositionWaveLeft;
        windowGlassPSRight.transform.localPosition = targetPositionWaveRight;
    }
    public void SetStartSizeAndPosition()
    {
        var leftWave = windowPSLeft.main;
        var rightWave = windowPSRight.main;
        var woodenWave = passingWaveWoodenGlass.main;
        var waveUp = windowPSUp.main;

        leftWave.startSizeY = startSize;
        rightWave.startSizeY = startSize;
        woodenWave.startLifetime = startSizeWave;
        waveUp.startLifetime = startSizeWave;
        windowGlassPSLeft.transform.localPosition = startPositionWaveLeft;
        windowGlassPSRight.transform.localPosition = startPositionWaveRight;
    }
    public void SetTargetPositionWaveMid()
    {
        waveMid.transform.localPosition = targetPositionWaveMid;
    }
    public void SetStartPositionWaveMid()
    {
        waveMid.transform.localPosition = startPositionWaveMid;
    }
}
