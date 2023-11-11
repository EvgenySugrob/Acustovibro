using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookIn : MonoBehaviour
{
    [SerializeField]
    private Transform target;


    void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - target.position);
    }
}
