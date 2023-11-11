using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuPanel : MonoBehaviour
{
    public static bool inPause = false;
    public GameObject pauseMenu, mainPanel, settingPanel, controlPanel, aboutPanel, canvas,canvasAC;

    private bool canvasIsActive = true;
    

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (inPause)
            {
                EnableCanvas();
                AllPanelClose();
                Resume();
            }
            else
            {
                DisableCanvas();
                Pause();
            }
        }
    }

    public void ButtonMenuClick()
    {
        DisableCanvas();
        Pause();
    }
    private void Pause()
    {
        AudioListener.pause = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        inPause = true;
    }
    public void Resume()
    {
        EnableCanvas();
        pauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
        inPause = false;
        AudioListener.pause = false;
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void SettingPanelOn()
    {
        settingPanel.SetActive(true);
        mainPanel.SetActive(false);
    }
    public void SettingPanelOff()
    {
        settingPanel.SetActive(false);
        mainPanel.SetActive(true);
    }
    public void ControlPanelOn()
    {
        controlPanel.SetActive(true);
        mainPanel.SetActive(false);
    }
    public void ControlPanelOff()
    {
        controlPanel.SetActive(false);
        mainPanel.SetActive(true);
    }
    public void AboutPanelOn()
    {
        aboutPanel.SetActive(true);
        mainPanel.SetActive(false);
    }
    public void AboutPanelOff()
    {
        aboutPanel.SetActive(false);
        mainPanel.SetActive(true);
    }

    public void AllPanelClose()
    {
        mainPanel.SetActive(true);
        SettingPanelOff();
        ControlPanelOff();
        AboutPanelOff();
    }

    public void DisableCanvas()
    {
        if (canvasIsActive)
        {
            canvas.SetActive(false);
            canvasAC.SetActive(false);
            canvasIsActive = false;
        }
    }
    public void EnableCanvas()
    {
        if (!canvasIsActive)
        {
            canvas.SetActive(true);
            canvasAC.SetActive(true);
            canvasIsActive = true;
        }
    }

}
