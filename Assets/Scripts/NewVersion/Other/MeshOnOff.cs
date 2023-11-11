using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshOnOff : MonoBehaviour
{
    [SerializeField] List<MeshRenderer> meshList;

    public void MeshOn()
    {
        foreach (MeshRenderer renderer in meshList) 
        {
            renderer.enabled = true; 
        }
    }

    public void MeshOff() 
    {
        foreach (MeshRenderer renderer in meshList)
        {
            renderer.enabled = false;
        }
    }
}
