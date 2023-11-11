using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayStopWave : MonoBehaviour
{
    public WaveShowHide waveShowHide;
    public GameObject settingWavePanel;
    public WaveformDisplaySetting waveDisplay;
    private bool isActive=false;

    public void ShowHide()
    {
        if (!isActive)
        {
            settingWavePanel.SetActive(true);
            waveShowHide.StartWave();
            isActive = true;
        }
        else
        {
            settingWavePanel.SetActive(false);
            waveShowHide.StopWave();
            waveDisplay.AllDisable();
            isActive = false;
        }
    }

    public void ReturnButtonClick()
    {
        settingWavePanel.SetActive(false);
        waveShowHide.StopWave();
        waveDisplay.AllDisable();
        isActive = false;
        waveShowHide.StopWaveOnRadiator();
    }
    public void EngineeringWaveOnly()
    {
        settingWavePanel.SetActive(false);
        waveShowHide.StopWave();
        waveDisplay.AllDisable();
        isActive = false;
    }

}
