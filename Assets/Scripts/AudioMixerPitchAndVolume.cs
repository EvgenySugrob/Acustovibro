using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioMixerPitchAndVolume : MonoBehaviour
{
    public AudioMixerGroup mixer;

    public void ChangeVolume(float volume)
    {
        mixer.audioMixer.SetFloat("MusicVolume", Mathf.Lerp(-40, 0, volume));
    }
    public void ChangePitch(float pitch)
    {
        mixer.audioMixer.SetFloat("PitchVolume", Mathf.Clamp(pitch,0,1)+1);
    }
}
