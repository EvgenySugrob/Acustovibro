using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Vmaya;
using Vmaya.Scene3D;

public class FocalLengthCamera : MonoBehaviour
{
    public new Camera camera;

    public Limit focalLimit;
    public float step = 1;
    public float speed = 2f;
    public float orthographicFactor = 1f;
    public bool useWheel;

    public Button incBtn;
    public Button decBtn;

    private float focalTo;

    protected float viewSize { get => getViewSize(); set => setViewSize(value); }

    private void setViewSize(float value)
    {
        camera.orthographicSize = value * orthographicFactor;
        camera.fieldOfView = value;
    }

    private float getViewSize()
    {
        return camera.orthographic ? (camera.orthographicSize / orthographicFactor) : camera.fieldOfView;
    }

    private void Awake()
    {
        if (incBtn) incBtn.onClick.AddListener(zoomInc);
        if (decBtn) decBtn.onClick.AddListener(zoomDec);

        focalTo = getViewSize();
    }

    private void Update()
    {
        if (!hitDetector.isOverGUI())
        {
            if (VKeyboard.GetKeyDown(Key.F))
            {
                if (focalTo == focalLimit.max)
                    focalTo = focalLimit.min;
                else focalTo = focalLimit.Clamp(getViewSize() + step);
                refreshButtons();
            }
            else if (useWheel)
            {
                float delta = Mathf.Abs(getViewSize() - focalTo);

                float msw = VMouse.ScrollWheel;
                if ((msw != 0) && (delta < step))
                {
                    focalTo = focalLimit.Clamp(getViewSize() + (msw > 0 ? -step : step));
                    refreshButtons();
                }
            }
        }

        setViewSize(Mathf.Lerp(getViewSize(), focalTo, Time.deltaTime * speed));
    }

    private void refreshButtons()
    {
        if (decBtn) decBtn.interactable = getViewSize() > focalLimit.min;
        if (incBtn) incBtn.interactable = getViewSize() < focalLimit.max;
    }

    public void zoomInc()
    {
        focalTo = focalLimit.Clamp(getViewSize() + step);
        refreshButtons();
    }

    public void zoomDec()
    {
        focalTo = focalLimit.Clamp(getViewSize() - step);
        refreshButtons();
    }
}
