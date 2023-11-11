using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorParticleSystem : MonoBehaviour
{
    public ParticleSystem doorPS;

    public void StartDoorPS()
    {
        doorPS.Play();
    }

    public void StopDoorPS()
    {
        doorPS.Stop();
    }
}
