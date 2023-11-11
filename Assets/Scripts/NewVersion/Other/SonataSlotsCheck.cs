using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonataSlotsCheck : MonoBehaviour
{
    [SerializeField] List<GameObject> sonataSlotsList;
    [SerializeField] GameObject singledSlot;
    private bool busy;

    public bool DeviceInstalled()//установлен ли датчик на один из слотов
    {
        foreach (GameObject sonata in sonataSlotsList)
        {
            if (sonata.GetComponent<SonataSlot>().CheckSlotInBusy())
            {
                busy = true;
                break;
            }
            else
                busy= false;
        }

        return busy;
    }

    public void ReplaceObjectSlot(GameObject devices)
    {
        foreach (GameObject thisDevices in sonataSlotsList)
        {
            if (thisDevices.GetComponent<SonataSlot>() == devices.GetComponent<ItemsForReplace>().slot)
            {
                thisDevices.GetComponent<SonataSlot>().ReservSlot(false);
                break;
            }
        }
    }

    public bool IsSlotFree()
    {
        if (singledSlot == null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void ClearSingleSlot()
    {
        singledSlot= null;
    }
    public void DeleteDevicesInSlot()
    {
        if (singledSlot.GetComponent<GeneratorWhiteNoiseControl>())
        {
            ParamCubeEditScale parametrs = FindObjectOfType<ParamCubeEditScale>();
            parametrs.GeneratorWhiteNoiseRemove();
        }
        Destroy(singledSlot.gameObject);
    }
    public void SlotReserv(GameObject devices)
    {
        singledSlot = devices;
        singledSlot.transform.SetParent(transform,true);

        if(singledSlot.GetComponent<GeneratorWhiteNoiseControl>())
        {
            ParamCubeEditScale parametrs = FindObjectOfType<ParamCubeEditScale>();
            parametrs.GetWhiteNoiseGenerator(singledSlot);
        }
        
        ////singledSlot.transform.localEulerAngles = Vector3.zero;
    }

    public GameObject ReturnObjectinSlot()
    {
        return singledSlot;
    }

    public void SlotsDevicesClear(GameObject hitObject)
    {
        foreach (GameObject item in sonataSlotsList)
        {
            SonataSlot slot = item.GetComponent<SonataSlot>();

            if (slot.CheckSlotInBusy())
            {
                Destroy(hitObject);
                slot.ReservSlot(false);
            }
        }
    }

    public bool OnEnebledSonataSlot()
    {
        bool isOn = false;
        foreach (GameObject item in sonataSlotsList)
        {
            if (item.GetComponent<SonataSlot>().CheckSlotInBusy())
            {
                isOn = item.GetComponent<SonataSlot>().EnabledSonataOrNo();
                return isOn;
            }
        }
        return isOn;
    }

    public bool OnEnabledSingleSlot()
    {
        bool isOn = false;
        if (singledSlot.GetComponent<VibroGeneratorControl>())
        {
            isOn = singledSlot.GetComponent<VibroGeneratorControl>().GeneratorEnable();
            return isOn;
        }
        return isOn;
    }
}
