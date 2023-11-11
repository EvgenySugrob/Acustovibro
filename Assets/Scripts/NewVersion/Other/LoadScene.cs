using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    bool ispustishka;
    public void ReloadSceneClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void LoadCatalog()
    {
        SceneManager.LoadScene(1);
    }
    public void LoadTren()
    {
        SceneManager.LoadScene(0);
    }
}
