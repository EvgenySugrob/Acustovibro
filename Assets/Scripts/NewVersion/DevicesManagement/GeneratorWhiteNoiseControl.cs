using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GeneratorWhiteNoiseControl : MonoBehaviour
{
    [Header("Позиция камеры для перемещения")]
    [SerializeField] GameObject _mainCamera;
    [SerializeField] Transform _targetPoint;
    [SerializeField] Vector3 _startPoint;
    [SerializeField] Quaternion _startRotation;
    [SerializeField] float _speedMove = 3f;
    [SerializeField] float _speedRotation = 5f;
    [SerializeField, HideInInspector] float minDistanceBetweenPoint = 0.005f;

    [Header("Взамиодействие с генератором")]
    [SerializeField] AudioSource _source;
    [SerializeField] GameObject _interfaceGWN;
    [SerializeField] List<Button> _buttonsList;
    [SerializeField] int[] typeSetting;
    [SerializeField] int[] typeMode;
    [SerializeField] int[] countVolume;
    [SerializeField] List<string> mainNameSetting;
    [SerializeField] List<string> nameMode;
    [SerializeField] List<string> volumeName;
    [SerializeField] TMP_Text titleSettText;
    [SerializeField] TMP_Text titleText;
    [SerializeField] TMP_Text funcText;
    [SerializeField] List<AudioClip> _audioClips;
    [SerializeField] GameObject _retunrBt;


    [SerializeField] private bool _isTypeSettingOpen = true;
    [SerializeField] private bool _isTypeModeOpen;
    [SerializeField] private bool _isCountVolumeOpen;
    private bool _isPlay;
    private bool _isWhiteNoise = true;

    [SerializeField] bool _generatorIsOn;

    [SerializeField] private bool isMove = false;
    [SerializeField] private bool inGenerator = false;

    [Header("UI генератора")]
    [SerializeField] GameObject _settingPanelGWN;

    [Header("Состояние генератора")]
    [SerializeField] float volumeGenerator = 1f;

    [SerializeField] private int tempStepMain = 0;
    [SerializeField] private int tempModeStep = 0;
    [SerializeField] private int tempVolumeStep = 0;

    [Header("Измерение")]
    [SerializeField] SpectrumAnalayzer30 spectrumAnalayzer;
    [SerializeField] GameObject reverbZone;
    [SerializeField] AudioLowPassFilter lowPassFilter;

    private float maxLowFilterCount = 22000f;
    private float minLowFilterCount = 1500f;
    private float lowFilterSpeed = 10000f;
    private bool isMax;
    private bool isChangeFilter;

    public bool WhiteNoisIsPlay()
    {
        return _isWhiteNoise;
    }
    public bool ReturnIsPlay()
    {
        if (_isPlay)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SetAnalayzerAndReverbZone(SpectrumAnalayzer30 analyzer,GameObject audioReverbZone)
    {
        spectrumAnalayzer = analyzer;
        reverbZone = audioReverbZone;
    }
    public void LowFilterMinMax(bool max)
    {
        isMax = max;
        isChangeFilter = true;
    }

    public void GeneratorOnOff(bool isOn)
    {
        _generatorIsOn= isOn;

        if (_generatorIsOn)
        {
            _interfaceGWN.SetActive(true);
        }
        else
        {
            _interfaceGWN.SetActive(false);
            mainNameSetting[0] = "Включить";
            titleSettText.text = mainNameSetting[0];
            _source.Stop();

            _isPlay = false;
            _settingPanelGWN.GetComponent<SettingPanelGWNTextSet>().TextPlayNoise(_isPlay);
        }
        SettingPanelGWNTextSet settingPanelGWNText = _settingPanelGWN.GetComponent<SettingPanelGWNTextSet>();
        settingPanelGWNText.TextGeneratorActive(_generatorIsOn);
    }

    public void SetSettingPanel(GameObject settingPanel)
    {
        _settingPanelGWN = settingPanel;
        volumeGenerator = _source.volume;

        SettingPanelGWNTextSet settingPanelGWNText = _settingPanelGWN.GetComponent<SettingPanelGWNTextSet>();

        settingPanelGWNText.SetToogleGenerator(_generatorIsOn);
        settingPanelGWNText.TextGeneratorActive(_generatorIsOn);
        settingPanelGWNText.TextPlayNoise(_isPlay);
        settingPanelGWNText.TextTypeNoise(_isWhiteNoise);
        settingPanelGWNText.TextVolumeLevel(volumeGenerator);
    }
    public void SetReturnBt(GameObject button)
    {
        _retunrBt = button;
    }

    public void EnableDisableMoveToGenerator()
    {
        inGenerator = !inGenerator;
        isMove = !isMove;
    }
    public void SetMainCamera(GameObject mainCamera)
    {
        _mainCamera = mainCamera;
        _startPoint = _mainCamera.transform.position;
        _startRotation = _mainCamera.transform.rotation;
        EnableDisableMoveToGenerator();
    }

    public void BtClick(int step)
    {
        if(_isTypeSettingOpen)
        {
            tempStepMain += step;

            if(tempStepMain>2)
            {
                tempStepMain--;
            }
            else if(tempStepMain<0)
            {
                tempStepMain++;
            }
            titleSettText.text = mainNameSetting[tempStepMain];
        }
        else if (_isTypeModeOpen)
        {
            tempModeStep += step;

            if (tempModeStep > 1)
            {
                tempModeStep--;
            }
            else if (tempModeStep < 0)
            {
                tempModeStep++;
            }
            funcText.text = nameMode[tempModeStep];
        }
        else if (_isCountVolumeOpen)
        {
            tempVolumeStep += step;

            if (tempVolumeStep > 9)
            {
                tempVolumeStep--;
            }
            else if (tempVolumeStep < 0)
            {
                tempVolumeStep++;
            }
            funcText.text = volumeName[tempVolumeStep];
        }
    }

    public void MainScreenActive()
    {
        if (_isTypeSettingOpen)
        {
            SettingSelect(tempStepMain);
        }
        else if (_isTypeModeOpen)
        {
            ModeSelect(tempModeStep);
        }
        else if(_isCountVolumeOpen)
        {
            VolumeSelect(tempVolumeStep);
        }

    }

    private void SettingSelect(int tempStep)
    {
        SettingPanelGWNTextSet settingPanelGWNText = _settingPanelGWN.GetComponent<SettingPanelGWNTextSet>();
        switch (tempStep)
        {
            case 0:
                if (_isPlay)
                {
                    mainNameSetting[0] = "Включить";
                    titleSettText.text = mainNameSetting[0];
                    _source.Stop();

                    _isPlay = false;
                }
                else if(_isPlay == false)
                {
                    _isPlay = true;
                    mainNameSetting[0] = "Выключить";
                    titleSettText.text = mainNameSetting[0];

                    if(_isWhiteNoise)
                    {
                        _source.clip = _audioClips[0];
                    }
                    else
                    {
                        _source.clip = _audioClips[1];
                    }
                    _source.Play();
                }
                settingPanelGWNText.TextPlayNoise(_isPlay);
                break;
            case 1:
                //HideShowSettText(false);
                //HideShowTitleAndFunc(true);

                titleText.text = mainNameSetting[1];
                funcText.text = nameMode[0];
                _isTypeSettingOpen = false;
                _isTypeModeOpen = true;

                HideShowSettText(false);
                HideShowTitleAndFunc(true);
                break;
            case 2:
                titleText.text = mainNameSetting[2];
                funcText.text = volumeName[tempVolumeStep];
                _isTypeSettingOpen = false;
                _isCountVolumeOpen = true;

                HideShowSettText(false);
                HideShowTitleAndFunc(true);
                break;
        }

    }

    private void ModeSelect(int tempStep)
    {
        SettingPanelGWNTextSet settingPanelGWNText = _settingPanelGWN.GetComponent<SettingPanelGWNTextSet>();
        switch (tempStep)
        {
            case 0:
                _isWhiteNoise = true;
                tempStepMain = 0;
                tempModeStep= 0;
                _isTypeModeOpen= false;
                _isTypeSettingOpen = true;

                _source.clip = _audioClips[0];
                titleSettText.text = mainNameSetting[0];
                break;
            case 1:
                _isWhiteNoise = false;
                tempModeStep = 0;
                tempStepMain = 0;
                _isTypeModeOpen = false;
                _isTypeSettingOpen = true;

                _source.clip = _audioClips[1];
                titleSettText.text = mainNameSetting[0];
                break;
        }
        if (_isPlay)
            _source.Play();
        settingPanelGWNText.TextTypeNoise(_isWhiteNoise);
        HideShowSettText(true);
        HideShowTitleAndFunc(false);
    }

    private void VolumeSelect(int tempStep)
    {
        SettingPanelGWNTextSet settingPanelGWNText = _settingPanelGWN.GetComponent<SettingPanelGWNTextSet>();
        switch (tempStep)
        {
            case 0:
                _source.volume = 0.1f;
                break;
            case 1:
                _source.volume = 0.2f;
                break;
            case 2:
                _source.volume = 0.3f;
                break;
            case 3:
                _source.volume = 0.4f;
                break;
            case 4:
                _source.volume = 0.5f;
                break;
            case 5:
                _source.volume = 0.6f;
                break;
            case 6:
                _source.volume = 0.7f;
                break;
            case 7:
                _source.volume = 0.8f;
                break;
            case 8:
                _source.volume = 0.9f;
                break;
            case 9:
                _source.volume = 1f;
                break;

        }
        settingPanelGWNText.TextVolumeLevel(_source.volume);
        _isCountVolumeOpen = false;
        _isTypeSettingOpen = true;
        HideShowSettText(true);
        HideShowTitleAndFunc(false);
    }

    private void HideShowTitleAndFunc(bool isOn)
    {
        titleText.gameObject.SetActive(isOn);
        funcText.gameObject.SetActive(isOn);
    }

    private void HideShowSettText(bool isOn)
    {
        titleSettText.gameObject.SetActive(isOn);
    }



    private void MoveToGenerator()
    {
        _mainCamera.GetComponent<CameraRotateAround>().enabled = false;
        _mainCamera.GetComponent<InventoryReplaceItem>().enabled = false;

        _mainCamera.transform.position = Vector3.Lerp(_mainCamera.transform.position, _targetPoint.position, _speedMove * Time.deltaTime);
        _mainCamera.transform.rotation = Quaternion.Lerp(_mainCamera.transform.rotation, _targetPoint.rotation, _speedRotation * Time.deltaTime);

        if (Vector3.Distance(_mainCamera.transform.position,_targetPoint.position)<minDistanceBetweenPoint)
        {
            isMove = false;
            _settingPanelGWN.SetActive(true);
            _retunrBt.SetActive(true);
        }
    }
    private void MoveAwayFromGenerator()
    {
        _settingPanelGWN.SetActive(false);
        _retunrBt.SetActive(false);

        _mainCamera.transform.position = Vector3.Lerp(_mainCamera.transform.position, _startPoint, _speedMove * Time.deltaTime);
        _mainCamera.transform.rotation = Quaternion.Lerp(_mainCamera.transform.rotation, _startRotation, _speedRotation * Time.deltaTime);

        if (Vector3.Distance(_mainCamera.transform.position, _startPoint) < minDistanceBetweenPoint)
        {
            isMove = false;


            _mainCamera.GetComponent<CameraRotateAround>().enabled = true;
            _mainCamera.GetComponent<InventoryReplaceItem>().enabled = true;
        }
    }

    private void Update()
    {
        if(inGenerator && isMove)
        {
            MoveToGenerator();
        }
        else if(!inGenerator && isMove)
        {
            MoveAwayFromGenerator();
        }


        if(isMax && isChangeFilter)
        {
            lowPassFilter.cutoffFrequency = Mathf.MoveTowards(lowPassFilter.cutoffFrequency, minLowFilterCount, lowFilterSpeed * Time.deltaTime);
            if(lowPassFilter.cutoffFrequency==minLowFilterCount)
                isChangeFilter = false;
        }
        else if (!isMax && isChangeFilter)
        {
            lowPassFilter.cutoffFrequency = Mathf.MoveTowards(lowPassFilter.cutoffFrequency, maxLowFilterCount, lowFilterSpeed * Time.deltaTime);
            if(lowPassFilter.cutoffFrequency == maxLowFilterCount)
                isChangeFilter = false;
        }
    }
}
