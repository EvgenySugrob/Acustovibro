using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnabledDisableDoorWavePanel : MonoBehaviour
{
    public GameObject doorWaveToggle;

    public void EnabledDoorWaveToggle()
    {
        doorWaveToggle.SetActive(true);
    }
    public void DisabledDoorWaveToggle()
    {
        doorWaveToggle.SetActive(false);
    }
}
