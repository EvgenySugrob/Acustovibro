using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoReboot : MonoBehaviour
{
    private void Awake()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("NoReboot");

        if (objects.Length > 1)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }


}
