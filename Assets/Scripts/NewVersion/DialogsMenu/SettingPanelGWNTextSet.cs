using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanelGWNTextSet : MonoBehaviour
{
    [SerializeField] Toggle _generatorToggle;
    [SerializeField] TMP_Text _toggleText;
    [SerializeField] TMP_Text _statusPlayText;
    [SerializeField] TMP_Text _typeModeNoiseText;
    [SerializeField] TMP_Text _volumeText;
    [SerializeField] GameObject mainProperty;
    [SerializeField] Transform _defPosition;
    [SerializeField] Transform _offsetPosition;

    private void Update()
    {
        if(mainProperty.activeSelf)
        {
            transform.position = _offsetPosition.position;
        }
        else
        {
            transform.position = _defPosition.position;
        }
    }

    public void SetToogleGenerator(bool isGeneratorActive)
    {
        if (isGeneratorActive)
        {
            _generatorToggle.isOn = isGeneratorActive;
        }
        else
        {
            _generatorToggle.isOn = isGeneratorActive;
        }
    }

    public void TextGeneratorActive(bool isGeneratorActive)
    {
        if (isGeneratorActive)
        {
            _toggleText.text = "Отключить генератор";
        }
        else
        {
            _toggleText.text = "Включить генератор";
        }
    }

    public void TextPlayNoise(bool isPlay)
    {
        if (isPlay)
        {
            _statusPlayText.text = "Вкл";
        }
        else
        {
            _statusPlayText.text = "Выкл";
        }
    }

    public void TextTypeNoise(bool isWhiteNoise)
    {
        if (isWhiteNoise)
        {
            _typeModeNoiseText.text = "Белый шум";
        }
        else
        {
            _typeModeNoiseText.text = "Розовый шум";
        }
    }

    public void TextVolumeLevel(float volumeGenerator) 
    {
        float tempVolume = volumeGenerator * 10;
        _volumeText.text = tempVolume.ToString();
    }
        
}
