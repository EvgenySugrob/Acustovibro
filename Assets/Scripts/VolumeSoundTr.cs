using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeSoundTr : MonoBehaviour
{
    public GameObject reverbZone;
    public AudioSource audioSource;
    public bool volumeOn, volumeOff;
    public float volumeSpeed = 0.05f;
    public float maxVolume = 1f;
    public float minVolume = 0.99f;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        CameraTag cameraTag = other.GetComponent<CameraTag>();
        if (cameraTag)
        {
            volumeOn = true;
            volumeOff = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        CameraTag cameraTag = other.GetComponent<CameraTag>();
        if (cameraTag)
        {
            volumeOn = false;
            volumeOff = true;
        }
    }

    private void Update()
    {
        if (volumeOn)
        {
            audioSource.volume = Mathf.MoveTowards(audioSource.volume, maxVolume, volumeSpeed * Time.deltaTime);
            if(audioSource.volume == maxVolume)
                volumeOn = false;
        }
        else if (volumeOff)
        {
            audioSource.volume = Mathf.MoveTowards(audioSource.volume, minVolume, volumeSpeed * Time.deltaTime);
            if (audioSource.volume == minVolume)
                volumeOff = false;
        }
    }

    public void ChangeVolume(float volumeSource)
    {
        audioSource.volume = volumeSource;
    }

    public void SwitchVolumeWhenGeneratorPlay()
    {
        maxVolume = 1f;
        minVolume = 0.5f;
        audioSource.volume = minVolume;
    }

    public void SwitchVolumeWhenGeneratorStop() 
    {
        maxVolume = 1f;
        minVolume = 0.5f;
        audioSource.volume = minVolume;
    }
}
