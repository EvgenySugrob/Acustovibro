using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhallLeftMove : MonoBehaviour
{
    private Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void WhallForward()
    {
        anim.SetBool("WlLtForward", true);
    }
    public void WhallBack()
    {
        anim.SetBool("WlLtForward", false);
    }
}
