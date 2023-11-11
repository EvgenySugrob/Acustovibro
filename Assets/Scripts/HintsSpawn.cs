using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HintsSpawn : MonoBehaviour
{
    public GameObject hintsPanel;

    void Update()
    {


    }

    public void OnMouseEnter()
    {
        hintsPanel.SetActive(true);
    }
    public void OnMouseExit()
    {
        hintsPanel.SetActive(false);
    }


}
