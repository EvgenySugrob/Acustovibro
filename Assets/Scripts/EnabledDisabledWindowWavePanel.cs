using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnabledDisabledWindowWavePanel : MonoBehaviour
{
    public GameObject windowWaveTg, windowLongitTg;

    public void EnabledWindowToggle()
    {
        windowWaveTg.SetActive(true);
        windowLongitTg.SetActive(true);
    }
    public void DisabledWindowToggle()
    {
        windowWaveTg.SetActive(false);
        windowLongitTg.SetActive(false);
    }
}
