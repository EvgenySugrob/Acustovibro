using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonataSlot : MonoBehaviour
{
    [SerializeField] bool slotBusy;
    [SerializeField] MeshOnOff meshOnOff;
    [SerializeField] bool isRadiator,isVentilation,isWindow;
    [SerializeField] GameObject sonataObject;
    

    public void ReservSlot(bool isBusy)
    {
        slotBusy = isBusy;
        if (meshOnOff != null)
        {
            if (slotBusy)
            {
                meshOnOff.MeshOn();
            }
            else
            {
                meshOnOff.MeshOff();
            }
        }
        
    }
    public void AddSlotObject(GameObject devices)
    {
        sonataObject = devices;
        sonataObject.transform.SetParent(transform, true);
    }

    public void RemoveSlotObject()
    {
        ReservSlot(false);
        Destroy(sonataObject);
        sonataObject = null;
    }

    public bool CheckSlotInBusy()
    {
        return slotBusy;
    }
    public bool CheckCanPlaceDevisec()
    {
        bool isCan;

        if(transform.GetComponentInParent<SonataSlotsCheck>().DeviceInstalled())
        {
            isCan = false;
        }
        else
        {
            isCan = true;
        }
            

        return isCan;
    }
    
    public SonataSlotsCheck SlotReturn()
    {
        SonataSlotsCheck sonataSlotsCheck = transform.GetComponentInParent<SonataSlotsCheck>();
        return sonataSlotsCheck;
    }

    public bool IsSlotLocation()
    {
        return isRadiator;
    }

    public bool EnabledSonataOrNo()
    {
        bool isOn = false;
        if (sonataObject.GetComponent<VibroGeneratorControl>())
        {
            isOn = sonataObject.GetComponent<VibroGeneratorControl>().GeneratorEnable();
            return isOn;
        }

        return isOn;
    }
}
