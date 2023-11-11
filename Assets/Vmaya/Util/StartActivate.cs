using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartActivate : MonoBehaviour {
    public List<GameObject> objects;


    private void Start()
    {
        foreach (GameObject obj in objects)
            obj.SetActive(true);
    }
}
