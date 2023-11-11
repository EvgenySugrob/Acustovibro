using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearChilds : MonoBehaviour {
    public void Clear()
    {
        clearChilds(transform);
    }

    public static void clearChilds(Transform trans)
    {
        for (int i = 0; i < trans.childCount; i++)
        {
            Destroy(trans.GetChild(i).gameObject);
        }
    }
}
