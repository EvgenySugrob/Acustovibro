using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropdownOpen : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] CheckDevicesWhenMaterialSwitch _checkDevices;
    [SerializeField] TMP_Dropdown dropdown;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_checkDevices.CheckDevicesOnWhall()==true)
        {
            dropdown.Hide();
        }
    }
}
