using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowChange : MonoBehaviour
{
    public GameObject windowTriple, windowDuble, woodenWindow, windowParticleSystemUp;
    [SerializeField] GameObject _doubleWindowSlot;
    [SerializeField] GameObject _tripleWindowSlot;
    [SerializeField] GameObject _woodenWindowSlot;

    public void SwitchWindow(int num)
    {
        switch (num)
        {
            case 0:
                SetParameters(true, false, false);
                SetSlotType(true, false, false);
                break;
            case 1:
                SetParameters(false, true, false);
                SetSlotType(false, true, false);
                break ;
            case 2:
                SetParameters(false, false, true);
                SetSlotType(false, false, true);
                break ;
        }
    }
    public void SetParameters(bool windowDouble, bool windowTripl, bool windowWooden)
    {
        windowDuble.SetActive(windowDouble);
        windowTriple.SetActive(windowTripl);
        woodenWindow.SetActive(windowWooden);
    }

    private void SetSlotType(bool doubleSlot,bool tripleSlot,bool woodenSlot)
    {
        _doubleWindowSlot.SetActive(doubleSlot);
        _tripleWindowSlot.SetActive(tripleSlot);
        _woodenWindowSlot.SetActive(woodenSlot);
    }
}
