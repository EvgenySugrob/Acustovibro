using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderRay : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void WhallForward()
    {
        anim.SetBool("WBForward", true);
    }
    public void WhallBack()
    {
        anim.SetBool("WBForward", false);
    }
}