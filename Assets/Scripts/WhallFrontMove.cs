using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhallFrontMove : MonoBehaviour
{
    private Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void WhallForward()
    {
        anim.SetBool("WlFtForward", true);
    }
    public void WhallBack()
    {
        anim.SetBool("WlFtForward", false);
    }
}
