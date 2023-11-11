using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHideLine : MonoBehaviour
{
    public LineRenderer lineRenderer;
    private bool isActive = false;

    public void SwitchShowHideLine()
    {
        if (!isActive)
        {
            ShowLineRender();
        }
        else
        {
            HideLine();
        }

    }
    public void HideLine()
    {
        lineRenderer.enabled = false;
        isActive = false;
    }

    public void ShowLineRender()
    {
        lineRenderer.enabled = true;
        isActive = true;
    }
}
