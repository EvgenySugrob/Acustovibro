using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchField : MonoBehaviour
{
    public Transform panelWithObject;
    [SerializeField] GameObject searchRoot, hierarchyRoot;
    public void Search(string inputText)
    {
        if (inputText != "")
        {
            searchRoot.SetActive(true);
            hierarchyRoot.SetActive(false);
            foreach (Transform child in panelWithObject)
            {
                child.gameObject.SetActive(child.GetComponent<ObjectButton>().CheckTags(inputText));
            }
        }
        else
        {
            searchRoot.SetActive(false);
            hierarchyRoot.SetActive(true);
        }
    }
}
