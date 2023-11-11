using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverbZoneColliderMove : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    public void RightColliderForward()
    {
        anim.SetBool("RigtCollMove", true);
    }
    public void RightColliderBack()
    {
        anim.SetBool("RigtCollMove",false);
    }
    public void LeftColliderForward()
    {
        anim.SetBool("LeftCollMove", true);
    }
    public void LeftColliderBack()
    {
        anim.SetBool("LeftCollMove", false);
    }
    public void BkColliderForward()
    {
        anim.SetBool("BackCollMove", true);
    }
    public void BkColliderBack()
    {
        anim.SetBool("BackCollMove", false);
    }
    public void FrontColliderForward()
    {
        anim.SetBool("FrontCollMove", true);
    }
    public void FrontColliderBack()
    {
        anim.SetBool("FrontCollMove", false);
    }
    public void FloorColliderForward()
    {
        anim.SetBool("FloorCollMove", true);
    }
    public void FloorColliderBack()
    {
        anim.SetBool("FloorCollMove", false);
    }
    public void RoofColliderForward()
    {
        anim.SetBool("RoofCollMove", true);
    }
    public void RoofColliderBack()
    {
        anim.SetBool("RoofCollMove", false);
    }
}
