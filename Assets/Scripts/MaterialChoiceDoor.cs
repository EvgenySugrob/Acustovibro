using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class MaterialChoiceDoor : MonoBehaviour
{
    public MeshRenderer meshRender;
    public Material[] materialsArray;
    public int pointer = 0;

    private void Start()
    {
        meshRender = GetComponent<MeshRenderer>();
    }

    public void ChangeMaterial(int num)
    {
        switch (num)
        {
            case 0:
                pointer = num;
                meshRender.material = materialsArray[pointer];
                break;
            case 1:
                pointer = num;
                meshRender.material = materialsArray[pointer];
                break;
        }
    }
}