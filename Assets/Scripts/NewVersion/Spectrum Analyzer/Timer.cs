using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private int sec = 0;
    private int minute = 0;
    private int hours = 0;
    private int unitSec = 1;
    [SerializeField] TMP_Text timeWork;

    public IEnumerator TimeFlow()
    {
        while (true)
        {
            if (sec == 59)
            {
                minute++;
                sec = -1;
            }
            if (minute == 59)
            {
                hours++;
                minute= -1;
            }
            sec += unitSec;
            timeWork.text = hours.ToString("D2") + " : " + minute.ToString("D2") + " : " + sec.ToString("D2");
            yield return new WaitForSeconds(1);
        }
    }

    private void OnEnable()
    {
        StartCoroutine(TimeFlow());
    }

    private void OnDisable()
    {
        //StopCoroutine(TimeFlow());
        //EnabledDisabledTimer(false);
    }
    private void Start()
    {
        
    }

    public void EnabledDisabledTimer(bool isOn)
    {
        if (isOn)
        {
            sec = 0;
            minute = 0;
            hours = 0;
            StartStopTimer(1);
            //StartCoroutine(TimeFlow());
        }
        else 
        {
            sec = 0;
            minute = 0;
            hours = 0;
            StartStopTimer(0);
           // StopCoroutine(TimeFlow());
        }
    }

    public void StartStopTimer(int unit)
    {
        unitSec = unit;
    }
}
