using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayerCollapse : MonoBehaviour
{
    public GameObject audioPlayer, playerCollapse, audioPlayer1, playerCollapse1;

    public void CollapsePlayer()
    {
        StateAudioPlayerObject(false,true);
    }

    public void ExpandPlayer()
    {
        StateAudioPlayerObject(true, false);
    }

    public void StateAudioPlayerObject(bool audioPlayerState, bool playerCollapseState)
    {
        audioPlayer.SetActive(audioPlayerState);
        playerCollapse.SetActive(playerCollapseState);
        audioPlayer1.SetActive(audioPlayerState);
        playerCollapse1.SetActive(playerCollapseState);
    }
}
