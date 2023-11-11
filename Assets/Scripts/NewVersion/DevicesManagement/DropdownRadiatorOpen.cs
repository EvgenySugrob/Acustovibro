using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropdownRadiatorOpen : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] CheckDevicesWhenMaterialSwitch _checkDevices;
    [SerializeField] TMP_Dropdown _dropdown;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_checkDevices.CheckDevicesOnRadiator() == true)
        {
            _dropdown.Hide();
        }
    }
}
