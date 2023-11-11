using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderDefualtPosition : MonoBehaviour
{
    public bool changeSlider = false;
    public bool changeIsNow = false;
    public float speed = 0.05f;

    private float startSliderAmp = 1f;
    private float startSliderFreq = 0f;

    public Slider sliderAmp, sliderFreq;

    private void Update()
    {
        if (changeIsNow == true)
        {
            changeSlider = true;
            ReturnSliderStartPosition(changeSlider);
        }

    }

    public void ReturnSliderStartPosition(bool isChangeSlider)
    {
        changeSlider = isChangeSlider;
        if (changeSlider)
        {
            sliderAmp.value = Mathf.MoveTowards(sliderAmp.value,startSliderAmp, speed*Time.deltaTime);
            sliderFreq.value = Mathf.MoveTowards(sliderFreq.value, startSliderFreq, speed * Time.deltaTime);
            if (sliderAmp.value == startSliderAmp && sliderFreq.value == startSliderFreq)
            {
                changeSlider = false;
                changeIsNow = false;
            }
        }
    }
}
