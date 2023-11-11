using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverbZoneTr : MonoBehaviour
{
    public GameObject reverbZone;
    private AudioLowPassFilter lowPassFilter;
    public bool lowPassOn, lowPassOff, reverbChangeIsNow=false;
    public float lowPassSpeed = 10000;

    [SerializeField] CountPlaceDevices _countPlaceDevices;


    private void Start()
    {
        lowPassFilter = GetComponent<AudioLowPassFilter>();
        lowPassOn = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        CameraTag cameraTag = other.GetComponent<CameraTag>();
        if (cameraTag)
        {
            reverbZone.SetActive(true);
            lowPassOn = false;
            lowPassOff = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        CameraTag cameraTag = other.GetComponent<CameraTag>();
        if (cameraTag)
        {
            reverbZone.SetActive(false);
            lowPassOn = true;
            lowPassOff = false;
        }
    }
    private void Update()
    {
        if (reverbChangeIsNow == true)
        {
            ReverbOn();
        }
        if (lowPassOn)
        {
            _countPlaceDevices.GeneratorLowPassOn();
            lowPassFilter.cutoffFrequency = Mathf.MoveTowards(lowPassFilter.cutoffFrequency, 1500, lowPassSpeed * Time.deltaTime);
            if(lowPassFilter.cutoffFrequency == 1500)
                lowPassOn=false;
        }
        else if (lowPassOff)
        {
            _countPlaceDevices.GeneratorHighPassOn();
            lowPassFilter.cutoffFrequency = Mathf.MoveTowards(lowPassFilter.cutoffFrequency, 22000, lowPassSpeed * Time.deltaTime);
            if (lowPassFilter.cutoffFrequency == 22000)
                lowPassOff=false;
        }
    }

    public void ReverbOn()
    {
        _countPlaceDevices.GeneratorLowPassOn();
        lowPassFilter.cutoffFrequency = Mathf.MoveTowards(lowPassFilter.cutoffFrequency, 1500, lowPassSpeed * Time.deltaTime);
        if (lowPassFilter.cutoffFrequency == 1500)
            reverbChangeIsNow = false; 
    }
}
