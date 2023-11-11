using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LocalType : MonoBehaviour
{
    public Transform fieldForObjectButton;
    [SerializeField] TMP_Text typeViewText;
    [SerializeField] RectTransform root;
    [HideInInspector] public string type;

    public void ClickOnButton()
    {
        if (fieldForObjectButton.gameObject.activeSelf)
        {
            fieldForObjectButton.gameObject.SetActive(false);
        }
        else
        {
            fieldForObjectButton.gameObject.SetActive(true);
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate(root);
        LayoutRebuilder.ForceRebuildLayoutImmediate(root);
    }
    public void SetType(string newType)
    {
        type = newType;
        typeViewText.SetText(type);
    }
}
