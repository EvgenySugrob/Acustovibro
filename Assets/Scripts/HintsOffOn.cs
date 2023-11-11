using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintsOffOn : MonoBehaviour
{
    public GameObject whallHints, radiatorHints, ventilationHints, hintsACMain, hintsMCMain;

    public void OffOnHints(bool state)
    {
        if (state)
        {
            HintsState(false);
        }
        else
        {
            HintsState(true);
        }
    }

    private void HintsState(bool state)
    {
        hintsACMain.SetActive(state);
        hintsMCMain.SetActive(state);
        //whallHints.SetActive(state);
        //radiatorHints.SetActive(state);
        //ventilationHints.SetActive(state);
    }

    public void RadiatorHintsOff()
    {
        radiatorHints.SetActive(false);
    }
    public void RadiatorHintsOn()
    {
        radiatorHints.SetActive(true);
    }
}
