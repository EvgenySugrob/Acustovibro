using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemParent : MonoBehaviour
{
    [SerializeField] GameObject parent;

    public GameObject GetParent()
    {
        return parent;
    }
}
