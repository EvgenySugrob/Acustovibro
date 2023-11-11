using Newtonsoft.Json.Bson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxMinAvgValueCube : MonoBehaviour
{
    [Header("Спавн минимума и максимума")]
    [SerializeField] GameObject minCube;
    [SerializeField] GameObject maxCube;

    [Header("Хранение текущих меток")]
    [SerializeField] GameObject currentMaxLine;
    [SerializeField] GameObject currentMinLine;

    private float currentMaxValue = 0;
    private float currentMinValue = 120;
    private float currentAvgValue;

    private float currentMaxSizeValue = 0;
    private float currentMinSizeValue = 1200;

    private bool isSpawnLineStart = false;

    public bool isdB30;
    public bool isdBSIm;
    public bool isVhax;
    public bool isShax;
    private bool isOn=false;

    private void Update()
    {
        if (isSpawnLineStart)
        {
            float currentMaxValueLine = transform.localScale.y;
            if (currentMaxValueLine > currentMaxSizeValue)
            {
                Destroy(currentMaxLine);
                currentMaxSizeValue = currentMaxValueLine;
                currentMaxValue = currentMaxValueLine / 10;
                float positionY = currentMaxValueLine * 8 / 100;
                Vector3 newSpawnPosition = new Vector3(transform.position.x, positionY, transform.position.z);
                GameObject newMaxLine = Instantiate(maxCube, newSpawnPosition, transform.rotation);
                currentMaxLine = newMaxLine;
                if (isdBSIm)
                {
                    newMaxLine.transform.localScale = new Vector3(80, 8, 60);
                }
                else if (isdB30)
                {
                    newMaxLine.transform.localScale = new Vector3(80, 8, 20);
                }
                else if (isVhax)
                {
                    newMaxLine.transform.localScale = new Vector3(80, 8, 100);
                }
                else if (isShax)
                {
                    newMaxLine.transform.localScale = new Vector3(80, 8, 20);
                }
            }

            if (currentMaxValueLine < currentMinSizeValue)
            {
                Destroy(currentMinLine);
                currentMinSizeValue = currentMaxValueLine;
                currentMinValue = currentMaxValueLine / 10;
                float positionY = currentMaxValueLine * 8 / 100;
                Vector3 newSpawnPosition = new Vector3(transform.position.x, positionY, transform.position.z);
                GameObject newMaxLine = Instantiate(minCube, newSpawnPosition, transform.rotation);
                currentMinLine = newMaxLine;
                if (isdBSIm)
                {
                    newMaxLine.transform.localScale = new Vector3(80, 8, 60);
                }
                else if (isdB30)
                {
                    newMaxLine.transform.localScale = new Vector3(80, 8, 20);
                }
                else if (isVhax)
                {
                    newMaxLine.transform.localScale = new Vector3(80, 8, 100);
                }
                else if (isShax)
                {
                    newMaxLine.transform.localScale = new Vector3(80, 8, 20);
                }
            }
        }
        
    }
    public void MaxMinValueLine()
    {
        if (isSpawnLineStart)
        {
            //StopCoroutine(SpawnValueLine());
            isSpawnLineStart= false;
        }
        else
        {
            //StartCoroutine(SpawnValueLine());
            isSpawnLineStart= true;
        }
    }

    public float ReturnMaxValue()
    {
        return (float)Math.Round(currentMaxValue,1);
    }

    public float ReturnMinValue()
    { 
        return (float)Math.Round(currentMinValue,1); 
    }

    public void SwitchModeDb(bool isDbSim, bool isDb30,bool isVhax, bool isShax)
    {
        isdBSIm = isDbSim;
        isdB30 = isDb30;
        this.isVhax = isVhax;
        this.isShax = isShax;

    }

    public void ClearMinMax()
    {
        Destroy(currentMinLine);
        Destroy(currentMaxLine);
    }

    public void ShowHide()
    {
        currentMinLine.SetActive(isOn);
        currentMaxLine.SetActive(isOn);
        isOn=!isOn;
    }

    public void ShowLine()
    {
        currentMinLine.SetActive(true);
        currentMaxLine.SetActive(true);
        isOn = false;
    }
}
