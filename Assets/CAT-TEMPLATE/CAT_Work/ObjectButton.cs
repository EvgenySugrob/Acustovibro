using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectButton : MonoBehaviour
{
    public ObjectInCatalog objectInCatalog;
    [SerializeField] CameraControl sceneControl;
    [SerializeField] Transform sceneTextField;
    [SerializeField] TMP_Text sceneNameView;
    [SerializeField] TMP_Text typeViewText;
    private string tags;

    public void SelectObject()
    {
        objectInCatalog.SetSceneObjects(sceneControl, sceneTextField, sceneNameView);
    }
    public void SetNameButton(string newButtonName)
    {
        typeViewText.SetText(newButtonName);
    }
    public void SetTags(string newTags)
    {
        tags = newTags;
    }
    public bool CheckTags(string inputTag)
    {
        return tags.Contains(inputTag.ToLowerInvariant());
    }
}
