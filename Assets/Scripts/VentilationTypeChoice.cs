using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VentilationTypeChoice : MonoBehaviour
{
    public GameObject ventilation, ventilationIsolation, ventilationPlastic;
    public ParticalSystemVentilation systemVentilation;

    public void SwitchWindow(int num)
    {
        switch (num)
        {
            case 0:
                SwapVentilation(true, false,false,true);
                systemVentilation.ParticalSystemsOn();
                systemVentilation.SizeWaveMetalVentilation();
                break;
            case 1:
                SwapVentilation(false,true,false,false);
                systemVentilation.ParticleSystemOff();
                break;
            case 2:
                SwapVentilation(false, false, true,true);
                systemVentilation.ParticalSystemsOn();
                systemVentilation.SizeWavePlasticVentilation();
                break;
        }
    }

    public void SwapVentilation(bool ventilationBool, bool ventilationIsolationBool, bool ventilationPlasticBool, bool ventilationSelectionBool)
    {
        ventilation.SetActive(ventilationBool);
        ventilationIsolation.SetActive(ventilationIsolationBool);
        ventilationPlastic.SetActive(ventilationPlasticBool);
        systemVentilation.typeVentilationSelection = ventilationSelectionBool;
    }
}
