using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Vmaya.UI.Components;

public class CheckDevicesWhenMaterialSwitch : MonoBehaviour
{
    [Header("����������")]
    public List<SonataSlotsCheck> _whallSlots;
    public GameObject floor;
    [SerializeField] CountPlaceDevices _countPlaceDevices;

    [Header("����� �� ����")]
    [SerializeField] List<SonataSlot> _doubleWindowSlots;
    [SerializeField] List<SonataSlot> _tripleWindowSlots;
    [SerializeField] List<SonataSlot> _woodenWindowSlots;

    [Header("����� �� ���������")]
    [SerializeField] List<SonataSlot> _radiatorSlots;

    [Header("���������� ����")]
    [SerializeField] GameObject _dialogsWindow;
    [SerializeField] TMP_Text _mainText;

    private bool _resultCheckOnWhall;
    private bool _resultCheckOnWindow;
    private bool _resultCheckOnRadiator;
    //------------------> Public 

    public void DeleteChoiseItem(GameObject itemForDelete)
    {
        Debug.Log(itemForDelete.name + " �������");
        if (itemForDelete.GetComponent<VibroGeneratorControl>())
        {
            if (itemForDelete.GetComponent<ItemsForReplace>().slot != null)
            {
                SonataSlot sonataSlot = itemForDelete.GetComponent<ItemsForReplace>().slot;
                sonataSlot.RemoveSlotObject();
            }
            else
            {
                SonataSlotsCheck sonataSlotsCheck = itemForDelete.GetComponent<ItemsForReplace>().sonataSlotsCheck;
                sonataSlotsCheck.DeleteDevicesInSlot();
                sonataSlotsCheck.ClearSingleSlot();
            }
        }
        else if (itemForDelete.GetComponent<GeneratorWhiteNoiseControl>())
        {
            Debug.Log("��������� ������ ����");
            if (itemForDelete.GetComponent<ItemsForReplace>().sonataSlotsCheck)
            {
                Debug.Log("��������� ������ ���� �������� �����");
                SonataSlotsCheck sonataSlotsCheck = itemForDelete.GetComponent<ItemsForReplace>().sonataSlotsCheck;
                sonataSlotsCheck.DeleteDevicesInSlot();
                sonataSlotsCheck.ClearSingleSlot();
            }
        }
    }

    public bool CheckDevicesOnRadiator()
    {
        _resultCheckOnRadiator = false;
        foreach (SonataSlot slot in _radiatorSlots)
        {
            if (slot.CheckSlotInBusy())
            {
                _resultCheckOnRadiator = true;
            }
        }
        if (_resultCheckOnRadiator)
        {
            string textMessage = "�� ������� ��������� ���������� ���������-����������������.\n���� ������� ��� ������, �� �������� ���������� ��������� ������.\n����������?";
            TextInDialogs(textMessage);
        }

        return _resultCheckOnRadiator;
    }

    public bool CheckDevicesOnWindow()
    {
        _resultCheckOnWindow = false;

        foreach (SonataSlot slot in _doubleWindowSlots)
        {
            if (slot.CheckSlotInBusy())
            {
                _resultCheckOnWindow = true;
                break;
            }
        }
        foreach (SonataSlot slot in _tripleWindowSlots)
        {
            if (slot.CheckSlotInBusy())
            {
                _resultCheckOnWindow = true;
                break;
            }
        }
        foreach (SonataSlot slot in _woodenWindowSlots)
        {
            if (slot.CheckSlotInBusy())
            {
                _resultCheckOnWindow = true;
                break;
            }
        }

        if (_resultCheckOnWindow)
        {
            string textMessage = "�� ���� ���������� ���������-����������������.\n���� ������� ��� ����, �� �������� ���������� ��������� ������.\n����������?";
            TextInDialogs(textMessage);
        }

        return _resultCheckOnWindow;
    }

    public bool CheckDevicesOnWhall()
    {
        _resultCheckOnWhall = false;
        int countDevicesOnWhall=0;

        foreach (SonataSlotsCheck slot in _whallSlots)
        {
            if (slot.IsSlotFree() == false)
            {
                countDevicesOnWhall++;
            }
        }
        Debug.Log(countDevicesOnWhall + "����������");
        if (countDevicesOnWhall>0)
        {
            _resultCheckOnWhall = true;
            string textMessage = "�� ������ ����������� " + countDevicesOnWhall + " �����������.\n���� ������� ��������, �� �������� ���������� ���������� ������.\n���������� ? ";
            TextInDialogs(textMessage);
            Debug.Log("� ���� " + countDevicesOnWhall +" �� ������.");
        }
        else
        {
            _resultCheckOnWhall= false;
        }
        return _resultCheckOnWhall;
    }

    public void ResultDialogs(bool result)
    {
        if (result)
        {
            PositiveResult();
        }
        else
        {
            NegativeResult();
        }
    }

    public void RemoveSingleDevices(GameObject hitGameobject)
    {
        if (hitGameobject.GetComponent<ItemsForReplace>().GetGenerator())
        {

            SonataSlotsCheck slotFloor = floor.GetComponent<SonataSlotsCheck>();
            GameObject tempGenerator = floor.GetComponent<SonataSlotsCheck>().ReturnObjectinSlot();
            _countPlaceDevices.ClearSlot();

            if (hitGameobject == tempGenerator)
            {
                slotFloor.DeleteDevicesInSlot();
                slotFloor.ClearSingleSlot();
            }
        }
        else
        {
            Debug.Log(hitGameobject.name + "vibroNoise");
            foreach (SonataSlotsCheck slot in _whallSlots)
            {
                GameObject tempSonata = slot.ReturnObjectinSlot();

                if (hitGameobject == tempSonata)
                {
                    slot.DeleteDevicesInSlot();
                    slot.ClearSingleSlot();
                }
                slot.SlotsDevicesClear(hitGameobject);
            }
        }


    }

    //---------------------> Private
    private void TextInDialogs(string text)
    {
        _mainText.text = text;
        _dialogsWindow.SetActive(true);
    }
    private void PositiveResult()
    {
        if (_resultCheckOnWhall)
        {
            foreach (SonataSlotsCheck sonata in _whallSlots)
            {
                GameObject tempSonata = sonata.ReturnObjectinSlot();

                if (tempSonata != null)
                {

                    //�������� ������ ������, �������� ����� �������� ������ �������� � ������� ��
                    sonata.DeleteDevicesInSlot();
                    sonata.ClearSingleSlot();
                    //Destroy(sonata);
                }
            }
            //_countPlaceDevices.ClearSonataList();
            
        }

        if (_resultCheckOnWindow)
        {
            foreach (SonataSlot slot in _doubleWindowSlots)
            {
                if (slot.CheckSlotInBusy())
                {
                    slot.RemoveSlotObject();
                    break;
                }
            }
            foreach (SonataSlot slot in _tripleWindowSlots)
            {
                if (slot.CheckSlotInBusy())
                {
                    slot.RemoveSlotObject();
                    break;
                }
            }
            foreach (SonataSlot slot in _woodenWindowSlots)
            {
                if (slot.CheckSlotInBusy())
                {
                    slot.RemoveSlotObject();
                    break;
                }
            }
        }
        if (_resultCheckOnRadiator)
        {
            foreach(SonataSlot slot in _radiatorSlots)
            {
                if (slot.CheckSlotInBusy())
                {
                    slot.RemoveSlotObject();
                    break;
                }
            }
        }
        _dialogsWindow.GetComponent<Window>().hide();
    }

    private void NegativeResult()
    {
        _dialogsWindow.GetComponent<Window>().hide();
    }
}
