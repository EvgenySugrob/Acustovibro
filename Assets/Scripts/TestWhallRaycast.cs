using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWhallRaycast : MonoBehaviour
{
    private Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void TestWhallForward()
    {
        anim.SetBool("WlLtForward", true);
    }
    public void TestWhallBack() 
    {
        anim.SetBool("WlLtForward",false);
    }
}
