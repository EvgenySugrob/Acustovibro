using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundOffOn : MonoBehaviour
{
    public GameObject volumeCube;
    public void OffOnSound(bool state)
    {
        if (state)
        {
            AudioListener.volume = 0f;
            volumeCube.SetActive(false);
        }
        else
        {
            AudioListener.volume = 1f;
            volumeCube.SetActive(true);
        }
    }
}
