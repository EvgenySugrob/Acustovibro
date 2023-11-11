using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialVentilationMain : MonoBehaviour
{
    public MeshRenderer meshRender;
    public Material[] materialsArray;

    public int pointer = 0;

    private void Start()
    {
        meshRender = GetComponent<MeshRenderer>();
    }

    public void ChangeMaterialCap(int num)
    {
        switch (num)
        {
            case 0:
                pointer = num;
                meshRender.material = materialsArray[pointer];
                break;
            case 2:
                pointer = num;
                meshRender.material = materialsArray[pointer];
                break;
        }
    }
}
