using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomClose : MonoBehaviour
{
    public WhallFrontMove whallFront;
    public WhallLeftMove whallLeft;
    public WhallRightMove whallRight;
    public ColliderRay colliderRay;

    public void CloseRoom() 
    {
        whallLeft.WhallBack();
        whallRight.WhallBack();
        colliderRay.WhallBack();
        whallFront.WhallBack();
    }

    public void WhallFrontClose()
    {
        whallFront.WhallBack();
    }
}
