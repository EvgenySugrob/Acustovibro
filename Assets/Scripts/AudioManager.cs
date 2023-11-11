using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] musicClips;
    private AudioSource source;
    private int currentTrack;
    private int fullLenght;
    private int playTime;
    [SerializeField] AudioSource standAudioSource;
    [SerializeField] AudioSource cubeSource;

    public TMP_Text clipText,clipText2;

    void Start()
    {
        source = GetComponent<AudioSource>();
        //PlayMusic();
        ShowCurrentTitle();
    }
    private void Update()
    {
        playTime=(int)source.time;
        if (playTime>=fullLenght)
        {
            NextTitle();
        }
    }
    public void PlayMusic()
    {
        if (source.isPlaying)
        {
            return;
        }
        cubeSource.Play();
        standAudioSource.Play();
        source.Play();
        ShowCurrentTitle();
        //StartCoroutine("WaitForMusicEnd");
    }
    public void NextTitle()
    {
        source.Stop();
        currentTrack++;
        if(currentTrack>musicClips.Length-1)
        {
            currentTrack = 0;
        }
        cubeSource.clip = musicClips[currentTrack];
        standAudioSource.clip = musicClips[currentTrack];
        source.clip = musicClips[currentTrack];
        cubeSource.Play();
        standAudioSource.Play();
        source.Play();

        ShowCurrentTitle();
        //StartCoroutine("WaitForMusicEnd");
    }
    public void PreviousTitle()
    {
        standAudioSource.Stop();
        source.Stop();
        currentTrack--;
        if (currentTrack <0)
        {
            currentTrack = musicClips.Length - 1;
        }
        source.clip=musicClips[currentTrack];
        standAudioSource.clip = musicClips[currentTrack];
        cubeSource.clip = musicClips[currentTrack];
        cubeSource.Play();
        standAudioSource.Play();
        source.Play();

        ShowCurrentTitle();
        //StartCoroutine("WaitForMusicEnd");
    }
    //public void StopMusic()
    //{
    //   // StopCoroutine("WaitForMusicEnd");
    //    source.Stop();
    //}
    //public void PauseMusic()
    //{
    //    //StopCoroutine("WaitForMusicEnd");
    //    source.Pause();
    //}
    public void ShowCurrentTitle()
    {
        clipText.text = source.clip.name;
        clipText2.text = source.clip.name;
        fullLenght = (int)source.clip.length;
    }
}