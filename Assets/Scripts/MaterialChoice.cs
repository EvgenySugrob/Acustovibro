using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MaterialChoice : MonoBehaviour
{
    public VolumeSoundTr volumeSound;
    public MeshRenderer meshRender;
    public Material[] materialsArray;
    public Sinewave sinewave;
    public int pointer = 0;

    public TMP_Text volumeText, coefText;

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
                MaterialSwitch(1f,1.5f,-5f,0.99f,"100%","0");
                break;
            case 1:
                pointer = num;
                meshRender.material = materialsArray[pointer];
                MaterialSwitch(0.7f, 1.3f, -3.7f, 0.7f, "70%", "0.032");
                break;
            case 2:
                pointer = num;
                meshRender.material = materialsArray[pointer];
                MaterialSwitch(0.85f,1.45f,-4.5f,0.85f, "85%", "0.015");
                break;
            case 3:
                pointer = num;
                meshRender.material = materialsArray[pointer];
                MaterialSwitch(0.4f,1f,-2.5f,0.4f,"40%","0.06");
                break;
            case 4:
                pointer = num;
                meshRender.material = materialsArray[pointer];
                MaterialSwitch(0.55f, 1f, -3f, 0.55f, "55%", "0.45");
                break;
            case 5:
                pointer = num;
                meshRender.material = materialsArray[pointer];
                MaterialSwitch(0.6f,1.4f,-3.5f,0.6f, "60%", "0.4");
                break;
        }
    }

    public void MaterialSwitch(float amplitude,float freauency,float moveSpeed, float minVolume, string textVolume, string textCoef)
    {
        sinewave.amplitude = amplitude;
        sinewave.freauency = freauency;
        sinewave.movementSpeed = moveSpeed;
        volumeSound.minVolume = minVolume;
        volumeSound.ChangeVolume(minVolume);
        volumeText.text = textVolume;
        coefText.text = textCoef;
    }
}

