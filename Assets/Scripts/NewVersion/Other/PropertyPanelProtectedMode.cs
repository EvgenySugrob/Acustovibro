using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropertyPanelProtectedMode : MonoBehaviour
{
    [Header("Оснавная панель свойств")]
    [SerializeField] List<GameObject> _needsProperty;
    [SerializeField] GameObject _unneedProperty;
    bool protectedModeIsActive;

    [Header("Панели настройки генераторов")]
    [SerializeField] GameObject _whiteNoiseSetting;
    [SerializeField] GameObject _vibroNoiseSetting;

    public void ProtectedModeActive()
    {
        protectedModeIsActive = !protectedModeIsActive;

        if (protectedModeIsActive)
        {
            _unneedProperty.SetActive(false);

            foreach (GameObject property in _needsProperty)
            {
                property.SetActive(true);
            }
        }
        else
        {
            _unneedProperty.SetActive(true);

            foreach (GameObject property in _needsProperty)
            {
                property.SetActive(false);
            }
        }
    }


}
