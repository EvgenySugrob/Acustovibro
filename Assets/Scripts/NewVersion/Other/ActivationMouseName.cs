using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ActivationMouseName : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] TMP_Text mainTitle;
    string panelMainTitle = "���������� ������";
    string generatorTitle = "��������� ������ ����";
    string sonataTitle = "������ ��-4�";
    [SerializeField] bool isWhiteNoiseGenerator;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isWhiteNoiseGenerator)
        {
            mainTitle.text = generatorTitle;
        }
        else 
        {
            mainTitle.text = sonataTitle;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mainTitle.text = panelMainTitle;
    }
}
