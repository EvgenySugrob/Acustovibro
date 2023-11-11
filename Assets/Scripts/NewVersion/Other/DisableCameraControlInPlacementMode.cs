using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableCameraControlInPlacementMode : MonoBehaviour
{
    [Header("Отключаемые/включаемые")]
    [SerializeField] CameraRaycast _cameraRaycast;
    [SerializeField] AudioListener _audioListener;
    [SerializeField] GameObject _securityToolsPanel;
    [SerializeField] VentilationSelected _selectedVentilation;
    [SerializeField] RadiatorSelected _selectedRadiator;
    [SerializeField] AudioSource _audioSourceReverb;

    [Header("Изменяемые функции")]
    [SerializeField] CameraRotateAround _cameraRoateAround;
    [SerializeField] BoxCollider _reverbBox;

    [Header("Настройка камеры")]
    [SerializeField] Transform _cameraTransform;
    [SerializeField] Transform _defaultTarget;
    [SerializeField] Transform _placeTarget;

    [SerializeField] private float _maxZoom;
    [SerializeField] private float _minZoom;

    [Header("Генератор БГ взаимодействие со звуком")]
    [SerializeField] CountPlaceDevices _countPlaceDevices;

    private float _defaultMaxZoom;
    private float _defaultMinZoom;

    [SerializeField] private bool isOn=true;

    //-----------> Private
    private void Start()
    {
        _defaultMaxZoom = _cameraRoateAround.zoomMax;
        _defaultMinZoom = _cameraRoateAround.zoomMin;
    }

    
    private void DisableEnableScripts()
    { 
        isOn = !isOn;
        _reverbBox.enabled = isOn;
        _selectedVentilation.PlaceActive();
        _selectedRadiator.PlaceActive();
        _cameraRaycast.PlaceActive();
        _cameraRaycast.enabled = isOn;
        AudioOffOn(isOn);
        //_audioListener.enabled = isOn;
        _securityToolsPanel.SetActive(!isOn);
    }

    private void AudioOffOn(bool isOn)
    {
        int i = 0;
        if (isOn) 
        {
            i = 1;
            _audioSourceReverb.Play();
        }
        else
        {
            i = 2;
            _audioSourceReverb.Stop();
        }
        
        
    }
    private void ChangeCameraPosition()
    {
        if (isOn)
        {
            SetCameraTransform(_defaultTarget.position, _defaultTarget.rotation);
            _cameraRoateAround.ChangeZoomMinMaxValue(_defaultMaxZoom,_defaultMinZoom);
            GeneratorLowPassOffOn(true);
        }
        else
        {

            SetCameraTransform(_placeTarget.position, _placeTarget.rotation);
            _cameraRoateAround.ChangeZoomMinMaxValue(_maxZoom, _minZoom);
            GeneratorLowPassOffOn(false);
        }
    }

    private void GeneratorLowPassOffOn(bool isOn)
    {
        if (_countPlaceDevices.GeneratorIsReplace())
        {
            GameObject generator = _countPlaceDevices.GeneratorReturn();

            generator.GetComponent<AudioLowPassFilter>().enabled = isOn;
        }
    }
    private void SetCameraTransform(Vector3 position, Quaternion rotation)
    {
        _cameraTransform.position = position;
        _cameraTransform.rotation = rotation;
    }

    //-----------> Public
    public void PlaceModActivation()
    {
        DisableEnableScripts();
        ChangeCameraPosition();
    }
    
    public bool ReturnIsActive()
    {
        return isOn;
    }
}
