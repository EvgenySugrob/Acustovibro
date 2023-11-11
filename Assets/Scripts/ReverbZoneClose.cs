using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverbZoneClose : MonoBehaviour
{
    public ReverbZoneColliderMove reverbZone;

    public void CloseReverbZone() 
    {
        reverbZone.RightColliderBack();
        reverbZone.LeftColliderBack();
        reverbZone.FrontColliderBack();
        reverbZone.BkColliderBack();
        reverbZone.FloorColliderBack();
        reverbZone.RoofColliderBack();
    }
}
