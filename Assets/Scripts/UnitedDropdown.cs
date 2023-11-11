using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Michsky.MUIP;

public class UnitedDropdown : MonoBehaviour
{
    public TMP_Dropdown dropdownMaterial;
    public CustomDropdown dropdownMaterialCustom;

    [SerializeField] List<MaterialChoice> _materialChoice;

    public void ValueChange(int num)
    {
        switch (num)
        {
            case 0:
                dropdownMaterialCustom.ChangeDropdownInfo(0);
                SwitchmaterialOnWhall(0);
                dropdownMaterial.value = 0;
                break;
            case 1:
                dropdownMaterialCustom.ChangeDropdownInfo(1);
                SwitchmaterialOnWhall(1);
                dropdownMaterial.value = 1;
                break;
            case 2:
                dropdownMaterialCustom.ChangeDropdownInfo(2);
                SwitchmaterialOnWhall(2);
                dropdownMaterial.value = 2;
                break;
            case 3:
                dropdownMaterialCustom.ChangeDropdownInfo(3);
                SwitchmaterialOnWhall(3);
                dropdownMaterial.value = 3;
                break;
            case 4:
                dropdownMaterialCustom.ChangeDropdownInfo(4);
                SwitchmaterialOnWhall(4);
                dropdownMaterial.value = 4;
                break;
            case 5:
                dropdownMaterialCustom.ChangeDropdownInfo(5);
                SwitchmaterialOnWhall(5);
                dropdownMaterial.value = 5;
                break;
        }
    }

    private void SwitchmaterialOnWhall(int index)
    {
        foreach (MaterialChoice material in _materialChoice)
        {
            material.ChangeMaterial(index);
        }
    }
}
