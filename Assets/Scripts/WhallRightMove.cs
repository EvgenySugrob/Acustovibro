using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhallRightMove : MonoBehaviour
{
    private Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void WhallForward()
    {
        anim.SetBool("WlRtForward", true);
    }
    public void WhallBack()
    {
        anim.SetBool("WlRtForward", false);
    }
}
