using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropdownWindowOpen : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] CheckDevicesWhenMaterialSwitch _checkDevices;
    [SerializeField] TMP_Dropdown _dropdown;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_checkDevices.CheckDevicesOnWindow() == true)
        {
            Debug.Log(_checkDevices.CheckDevicesOnWindow());
            _dropdown.Hide();
        }
    }
}
