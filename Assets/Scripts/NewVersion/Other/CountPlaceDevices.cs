using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountPlaceDevices : MonoBehaviour
{
    [Header("Списоки, поля установленых устройств")]
    [SerializeField] GameObject generatorsWhiteNoise;
    [SerializeField] List<GameObject> sonataList;
    [SerializeField] SpectrumAnalayzer30 spectrumAnalayzer;
    [SerializeField] VolumeSoundTr volumeSoundTr;

    //--------------------> Public 
    public void CheckLimmit(string nameCreatedObj, GameObject createdObj)
    {

        switch (nameCreatedObj)
        {
            case "Генератор белого шума":
                if (generatorsWhiteNoise == null)
                {
                    generatorsWhiteNoise = createdObj;
                    

                }
                break;

            case "Соната СВ-4Б":
                sonataList.Add(createdObj);
                break;
        }
    }

    public bool GeneratorIsReplace()
    {
        if (generatorsWhiteNoise == null)
        {

            return false;
        }
        else
        {

            return true;
        }
    }

    public void ClearSlot()
    {
        generatorsWhiteNoise = null;
    }

    public void GeneratorLowPassOn()
    {
        if(GeneratorIsReplace())
            generatorsWhiteNoise.GetComponent<GeneratorWhiteNoiseControl>().LowFilterMinMax(true);
    }
    public void GeneratorHighPassOn() 
    {
        if(GeneratorIsReplace())
            generatorsWhiteNoise.GetComponent<GeneratorWhiteNoiseControl>().LowFilterMinMax(false);
    }

    public GameObject GeneratorReturn()
    {
        return generatorsWhiteNoise;
    }

    public void SoundGeneratorOnAnalayzer()
    {
        if (GeneratorIsReplace())
        {
            if (generatorsWhiteNoise.GetComponent<GeneratorWhiteNoiseControl>().ReturnIsPlay())
            {
                spectrumAnalayzer.SwitchAudioSourceOnGenerator(generatorsWhiteNoise.GetComponent<AudioSource>());
                volumeSoundTr.SwitchVolumeWhenGeneratorPlay();
                spectrumAnalayzer.ModParamScaleUp();
            }
            else
            {
                volumeSoundTr.SwitchVolumeWhenGeneratorStop();
                spectrumAnalayzer.ModParamScaleDown();
            }
        }
        
    }
    public void ClearSonataList()
    {
       //Debug.Log(sonataOnWhall.gameObject);

        //foreach (GameObject item in sonataList)
        //{
        //    if (item == null)
        //    {
        //        sonataList.Remove(item);
        //    }
        //}
        //sonataList.Remove(sonataOnWhall);
        //foreach (GameObject sonata in sonataList)
        //{
        //    if (sonata == sonataOnWhall)
        //    {
                
        //    }
        //}
    }
}
