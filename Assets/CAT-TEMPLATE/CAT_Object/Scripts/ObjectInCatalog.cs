using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectInCatalog : MonoBehaviour
{
    public string nameObject;
    public List<string> types;

    [SerializeField] float minDistance, maxDistance, rotationSpeed;
    [SerializeField] TMP_Text description;

    private CameraControl control;
    private Transform textField;
    private TMP_Text nameView;

    private void Initial()
    {
        SetText();
        nameView.text = nameObject;
    }

    private void SetText()
    {
        foreach (Transform child in textField)
            Destroy(child.gameObject);
        Instantiate(description.gameObject, textField);
    }

    public void SetSceneObjects(CameraControl sceneControl, Transform sceneTextField, TMP_Text sceneNameView)
    {
        control = sceneControl;
        textField = sceneTextField;
        nameView = sceneNameView;
        control.PrepareObject(gameObject, minDistance, maxDistance, rotationSpeed);
        Initial();
    }

}
