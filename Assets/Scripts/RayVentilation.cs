using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayVentilation : MonoBehaviour
{
    public ReflectionOnVentilation reflectionUp, reflectionDown;
    public float speed = 0.1f;

    private float max = 8f;
    private float min = 0f;

    private void Update()
    {
        if (reflectionUp.maxLength > max && reflectionDown.maxLength>max)
        {
            reflectionUp.maxLength = min;
            reflectionDown.maxLength = min;
        }
        else
        {
            reflectionUp.maxLength += speed*Time.deltaTime;
            reflectionDown.maxLength += speed*Time.deltaTime;
        }
    }

    public void ChangeReflectionCount(int typeVentilation)
    {
        switch (typeVentilation)
        {
            case 0:
                SetReflectionCount(13);
                break;
            case 1:
                SetReflectionCount(20);
                break;
        }
    }

    public void SetReflectionCount(int countReflection)
    {
        reflectionUp.reflection = countReflection;
        reflectionDown.reflection = countReflection;
    }

}
