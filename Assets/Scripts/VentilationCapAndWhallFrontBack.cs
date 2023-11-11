using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VentilationCapAndWhallFrontBack : MonoBehaviour
{
    public RoomClose roomClose;

    public void WhallBack()
    {
        roomClose.WhallFrontClose();
    }
}
