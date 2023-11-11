using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParamCubeEditScale : MonoBehaviour
{
    [SerializeField] List<ParamCube> paramCubeList;
    [SerializeField] GameObject whiteNoiseGenerator;
    [SerializeField] GeneratorWhiteNoiseControl whiteNoiseControl;
    [SerializeField] bool generatorIsPlace;

    public void ScaleMultiDefualt()
    {
        foreach (ParamCube parametr in paramCubeList)
        {
            parametr._scaleMiltiplier = 0.6f;
        }
    }

    public void ScaleMultiUp()
    {
        foreach (ParamCube parametr in paramCubeList)
        {
            parametr._scaleMiltiplier = 1f;
        }
    }

    public void GetWhiteNoiseGenerator(GameObject generator)
    {
        whiteNoiseGenerator = generator;
        whiteNoiseControl = whiteNoiseGenerator.GetComponent<GeneratorWhiteNoiseControl>();
        generatorIsPlace = true;
    }

    public void GeneratorWhiteNoiseRemove()
    {
        whiteNoiseGenerator = null;
        whiteNoiseControl= null;
        generatorIsPlace = false;
    }

    private void Update()
    {
        if (generatorIsPlace)
        {
            if (whiteNoiseControl.ReturnIsPlay())
            {
                ScaleMultiUp();
            }
            else
            {
                ScaleMultiDefualt();
            }
        }
    }

}
