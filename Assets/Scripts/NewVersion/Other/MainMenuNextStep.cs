using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuNextStep : MonoBehaviour
{
    [SerializeField] GameObject horizontlaMenu;
    [SerializeField] GameObject cameraRotation;
    [SerializeField] GameObject maincamera;
    [SerializeField] RotationAroundHome rotationAroundHome;
    [SerializeField] GameObject mainMenu;
    [SerializeField] List<AudioSource> audioList;
    [SerializeField] GameObject aboutPanel;
    public void StartProgrammJob()
    {
        cameraRotation.SetActive(false);
        rotationAroundHome.DisableScript();
        maincamera.SetActive(true);
        horizontlaMenu.SetActive(true);
        mainMenu.SetActive(false);
        foreach (AudioSource source in audioList)
        {
            source.Play();
        }
    }

    public void OpenAbout()
    {

    }
}
