using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpectrumAnalayzer30 : MonoBehaviour
{
    private const int SAMPLE_SIZE = 512;

    [Header("Модификатор для белого шума")]
    [SerializeField] float visualModForGeneratorWhiteNoiseDbsim;
    [SerializeField] float visualModForGeneratorDb30;
    [SerializeField] float visualModForGeneratorPinkNoiseDbsim;
    [SerializeField] float visualModForGeneratorPinkDb30;
    [SerializeField] float saveVisualMod;

    [Header("Настройка спавна сэмплов")]
    [SerializeField] GameObject prefabCube;
    [SerializeField] GameObject parentSpawn;
    [SerializeField] GameObject parentSpawnLevelCorrection;
    [SerializeField] Transform spawnPosition;
    [SerializeField] GameObject parentPositiondBSIM;
    [SerializeField] Transform parentPositionVhax;
    [SerializeField] float coefDistance;
    [SerializeField] float coefSize = 10f;
    public float rmsValue;
    public float dbValue;
    public int amnVisual = 30;


    [Header("Настройка поведения семплов")]
    public float maxVisualScale = 25f;
    public float visualModifier = 50f;
    public float smoothSpeed = 10f;
    public float keepPercentage = 0.5f;
    public float coefPlusDb30 = 10500f;
    public float coefPlusIsShax = 5000f;

    [SerializeField] bool isHideVibroActive;

    [Header("Настройка поведения уровней коррекции")]
    public float visualModifierLevelCorrection = 50f;
    public float keepPercentageLevelCorection = 0.5f;

    [Header("Отображение информации по выбраному сэмплу")]
    [SerializeField] TMP_Text hzRange;
    [SerializeField] TMP_Text currentValueText;
    [SerializeField] TMP_Text maxCurrentValueText;
    [SerializeField] TMP_Text minCurrentValueText;
    [SerializeField] TMP_Text avgCurrentValueText;

    [Header("Изменение материала сэмпла")]
    [SerializeField] Material defaultMat;
    [SerializeField] Material selectMat;

    [Header("Компоненты для переключения\nна таблицу и обратно")]
    [SerializeField] GameObject dBTable;
    [SerializeField] GameObject lsTextMain;
    [SerializeField] GameObject lsTextSecond;
    [SerializeField] GameObject lineGroupChartBar;
    [SerializeField] GameObject minMaxGroup;

    public bool isLeftRight = false;
    public bool IsWhallConected;

    [SerializeField] private AudioSource source;
    [SerializeField] AudioSource vibroSound;
    public AudioSource mainSource;
    [SerializeField] AudioSource hideNoiseSoure;
    [SerializeField] AudioSource generatorWhiteNoiseAudio;
    private float[] samples;
    private float[] spectrum;
    private float sampleRate;

    public List<Transform> mergeVisualList;
    public Transform[] visualList;
    public Transform[] levelVisualList;
    public MaxMinAvgValueCube[] maxMinAvgValueList;
    public MaxMinAvgValueCube[] maxMinAvgValueCorrectionList;
    private float[] visualScale;
    private float[] levelVisualScale;

    private int lastRange = 0;
    private int currentRange = 0;
    private int tempRange = 0;

    [SerializeField] private bool isSwitchChartBarOnTable = false;

    private Coroutine lastRoutine= null;

    private string[] db30RangeText = new string[30] { "25 Hz", "31,5 Hz", "40 Hz", "50 Hz", "63 Hz", "80 Hz", "100 Hz", "125 Hz", "160 Hz", "200 Hz",
                                                        "250 Hz", "315 Hz", "400 Hz", "400 Hz", "500 Hz", "630 Hz",
                                                        "800 Hz", "1 kHZ", "1250 Hz", "1600 Hz", "2 kHz", "2500 Hz", "3150 Hz",
                                                        "4 kHz", "5 kHZ", "6300 Hz", "8 kHz", "10 kHz", "12,5 kHz", "16 kHz" };
    private string[] dbsimRangeText = new string[14] {"dBA","dBA","dBA","dBA","31,5 Hz", "63 Hz", "125 Hz", "250 Hz", "500 Hz", "1 kHz", "2 kHz", "4 kHz", "8 kHz", "16 kHz" };
    private string[] vhaxRangeText = new string[9] { "Wh", "8 Hz", "16 Hz", "31,5 Hz", "63,0 Hz", "125 Hz", "250 Hz", "500 Hz", "1000 Hz" };
    private string[] shaxRangeText = new string[24] { "6,3 Hz", "8 Hz", "10 Hz", "12 Hz", "16 Hz", "20 Hz", "25 Hz", "31,5 Hz", "40 Hz", "50 Hz", "63 Hz", "80 Hz",
                                                      "100 Hz", "125 Hz", "160 Hz", "200 Hz", "250 Hz", "315 Hz", "400 Hz", "500 Hz", "630 Hz", "800 Hz", "1000 Hz", "1250 Hz"};

    [Space]
    [SerializeField]private bool isDb30 = false;
    [SerializeField]private bool isDbsim = false;
    [SerializeField]private bool isVhaX = false;
    [SerializeField]private bool isShaX = false;
    [Space]

    [Header("Вывод в таблицу значений")]
    [SerializeField] List<TMP_Text> tableRangeTextList;
    [SerializeField] List<TMP_Text> tableRangeDbSImTextList;
    [SerializeField] List<TMP_Text> tableRangeLevelCorection;
    [SerializeField] List<TMP_Text> tableRangeVhaxList;
    [SerializeField] List<TMP_Text> tableRangeShaxList;

    [SerializeField] CountPlaceDevices _countPlace;
    [SerializeField] ControlAssistence controlAssistence;
    [SerializeField] GameObject generatorWN;

    private float tempVisualModifier;

    private void OnEnable()
    {
        samples = new float[SAMPLE_SIZE];
        spectrum = new float[SAMPLE_SIZE];
        sampleRate = AudioSettings.outputSampleRate;
        SpawnLine();

        lastRoutine = StartCoroutine(UpdateMetric(currentRange));
        RecolorRange(currentRange, currentRange);
    }
    private void OnDisable()
    {
        StopAllCoroutines();
        hzRange.text = "";
        mergeVisualList.Clear();
        foreach (Transform chart in visualList)
        {
            chart.GetComponent<MaxMinAvgValueCube>().ClearMinMax();
            Destroy(chart.gameObject);
        }
        if (isDbsim)
        {
            foreach (Transform chart in levelVisualList)
            {
                chart.GetComponent<MaxMinAvgValueCube>().ClearMinMax();
                Destroy(chart.gameObject);
            }
        }

        samples = null;
        spectrum = null;
        levelVisualList= null;
        visualList= null;
        maxMinAvgValueList= null;
        maxMinAvgValueCorrectionList= null;

    }

    private void Update()
    {
        AnalyzeSound();
        UpdateVisual();
        if (isDbsim)
        {
            UpdateVisualLevelCorrection();
        }
    }

    private void AnalyzeSound()
    {
        source.GetOutputData(samples, 0);

        int i = 0;
        float sum = 0;

        for (; i < SAMPLE_SIZE; i++)
        {
            sum = samples[i] + samples[i];
        }

        rmsValue = Mathf.Sqrt(sum / SAMPLE_SIZE);

        dbValue = 20 * Mathf.Log10(rmsValue / 0.9f);

        source.GetSpectrumData(spectrum, 0, FFTWindow.Triangle);
    }

    private void SpawnLine()
    {
        if (isDbsim)
        {
            if(generatorWN)
            {
                UnityEngine.Debug.Log("Проверка на генератор БГ");
                if (generatorWN.GetComponent<GeneratorWhiteNoiseControl>())
                {
                    GeneratorWhiteNoiseControl control = generatorWN.GetComponent<GeneratorWhiteNoiseControl>();
                    if (control.ReturnIsPlay() == false)
                    {
                        source = mainSource;
                        ModParamScaleDown();
                    }
                    else
                    {
                        source = generatorWhiteNoiseAudio;
                        ModParamScaleUp();
                    }
                }
                else
                {
                    source = mainSource;
                    ModParamScaleDown();
                }
            }

            
            hzRange.text = "dBA";
            SpawnLineDbsim();
            SpawnLevelCorrection();
            
        }
        else if (isDb30)
        {
            if (generatorWN) 
            {
                if (generatorWN.GetComponent<GeneratorWhiteNoiseControl>())
                {
                    GeneratorWhiteNoiseControl control = generatorWN.GetComponent<GeneratorWhiteNoiseControl>();
                    if (control.ReturnIsPlay() == false)
                    {
                        source = mainSource;
                        ModParamScaleDown();
                    }
                    else
                    {
                        source = generatorWhiteNoiseAudio;
                        ModParamScaleUpDb30();
                    }
                }
                else
                {
                    source = mainSource;
                    ModParamScaleDown();
                }
            }
           
            hzRange.text = "25 Hz";
            SpawnLineDb30();
        }
        else if (isVhaX)
        {
            if (IsWhallConected == false)
            {
                source = vibroSound;
            }
            else
            {
                source = hideNoiseSoure;
            }
            ModParamScaleDown();

            hzRange.text = "Wh";
            SpawnLineVhax();
        }
        else if(isShaX)
        {
            if (IsWhallConected==false)
            {
                source = vibroSound;
            }
            else
            {
                source = hideNoiseSoure;
            }
            ModParamScaleDown();

            hzRange.text = "6,3";
            SpawnLineShax();
        }
        MergeLevelCorrectionWithDbsim();
    }

    private void SpawnLineVhax()
    {
        amnVisual = 9;
        coefDistance = 10;
        coefSize = 100;
        keepPercentage = 0.02f;

        visualScale = new float[amnVisual];
        visualList = new Transform[amnVisual];
        maxMinAvgValueList = new MaxMinAvgValueCube[amnVisual];
        parentSpawn.transform.position = spawnPosition.position;

        for (int i = 0; i < amnVisual; i++)
        {
            //GameObject go = Instantiate(prefabCube);
            GameObject go = Instantiate(prefabCube, parentSpawn.transform);
            visualList[i] = go.transform;
            visualList[i].position = (Vector3.right * coefDistance) * i;

            maxMinAvgValueList[i] = visualList[i].GetComponent<MaxMinAvgValueCube>();
        }
        parentSpawn.transform.position = parentPositionVhax.position;
    }

    private void SpawnLineShax()
    {
        amnVisual = 24;
        coefDistance = 3.7f;
        coefSize = 35;
        keepPercentage = 0.1f;

        visualScale = new float[amnVisual];
        visualList = new Transform[amnVisual];
        maxMinAvgValueList = new MaxMinAvgValueCube[amnVisual];
        parentSpawn.transform.position = spawnPosition.position;

        for (int i = 0; i < amnVisual; i++)
        {
            //GameObject go = Instantiate(prefabCube);
            GameObject go = Instantiate(prefabCube, parentSpawn.transform);
            visualList[i] = go.transform;
            visualList[i].position = (Vector3.right * coefDistance) * i;

            maxMinAvgValueList[i] = visualList[i].GetComponent<MaxMinAvgValueCube>();
        }
        parentSpawn.transform.position = spawnPosition.position;
    }

    private void SpawnLineDb30()
    {
        amnVisual = 30;
        coefDistance = 3;
        coefSize = 20;
        //visualModifier = 2500;
        keepPercentage = 0.1f;

        visualScale = new float[amnVisual];
        visualList = new Transform[amnVisual];
        maxMinAvgValueList = new MaxMinAvgValueCube[amnVisual];
        parentSpawn.transform.position = spawnPosition.position;

        for (int i = 0; i < amnVisual; i++)
        {
            //GameObject go = Instantiate(prefabCube);
            GameObject go = Instantiate(prefabCube, parentSpawn.transform);
            visualList[i] = go.transform;
            visualList[i].position = (Vector3.right * coefDistance) * i;

            maxMinAvgValueList[i] = visualList[i].GetComponent<MaxMinAvgValueCube>();
        }
        parentSpawn.transform.position = spawnPosition.position;
    }

    private void SpawnLevelCorrection()
    {
        levelVisualScale = new float[4];
        levelVisualList = new Transform[4];
        maxMinAvgValueCorrectionList = new MaxMinAvgValueCube[4];
        

        for (int i = 0; i < 4; i++)
        {
            GameObject goCorrection = Instantiate(prefabCube,parentSpawnLevelCorrection.transform);
            levelVisualList[i] = goCorrection.transform;
            levelVisualList[i].position = (Vector3.right * coefDistance) * i;

            maxMinAvgValueCorrectionList[i] = levelVisualList[i].GetComponent<MaxMinAvgValueCube>();
        }
        parentSpawnLevelCorrection.transform.position = spawnPosition.position;
    }

    private void SpawnLineDbsim()
    {
        amnVisual = 10;
        coefDistance = 6;
        coefSize = 60;
        //visualModifier = 3500;
        keepPercentage = 0.02f;

        visualScale = new float[amnVisual];
        visualList = new Transform[amnVisual];
        maxMinAvgValueList = new MaxMinAvgValueCube[amnVisual];
        parentSpawn.transform.position = spawnPosition.position;

        for (int i = 0; i < amnVisual; i++)
        {
            //GameObject go = Instantiate(prefabCube);
            GameObject go = Instantiate(prefabCube, parentSpawn.transform);
            visualList[i] = go.transform;
            visualList[i].position = (Vector3.right * coefDistance) * i;

            maxMinAvgValueList[i] = visualList[i].GetComponent<MaxMinAvgValueCube>();
        }
        parentSpawn.transform.position = parentPositiondBSIM.transform.position;
    }

    private void MergeLevelCorrectionWithDbsim()
    {
        if (isDbsim)
        {
            foreach (Transform trans in levelVisualList)
            {
                mergeVisualList.Add(trans);
            }
        }
        
        foreach(Transform trans in visualList)
        {
            mergeVisualList.Add(trans);
        }
    }

    private void UpdateVisual()
    {
        int visualIndex = 0;
        int spectrumIndex = 0;
        int averageSize = (int)(SAMPLE_SIZE * keepPercentage) / amnVisual;

        while (visualIndex < amnVisual)
        {
            int j = 0;
            float sum = 0;
            while (j<averageSize)
            {
                sum += spectrum[spectrumIndex];
                spectrumIndex++;
                j++;
            }

            float scaleY=1f;

            if (isDb30)
            {
                if(generatorWN != null)
                {
                    if (generatorWN.GetComponent<GeneratorWhiteNoiseControl>().ReturnIsPlay())
                    {
                        Debug.Log(generatorWN.GetComponent<GeneratorWhiteNoiseControl>().ReturnIsPlay());
                        scaleY = sum / averageSize * visualModifier;
                        Debug.Log("Играет DB30 шумелки");
                    }
                    else
                    {
                        if (visualIndex < 6)
                        {
                            scaleY = sum / averageSize * visualModifier;
                        }
                        else
                        {
                            scaleY = sum / averageSize * (visualModifier + coefPlusDb30); //увеличение спекторв после 6 для равномерной картинки
                        }
                        Debug.Log("Играет DB30 без шумелки");
                    }
                    
                }
                else
                {
                    if(visualIndex<6)
                    {
                        scaleY = sum / averageSize * visualModifier;
                    }
                    else
                    {
                        scaleY = sum / averageSize * (visualModifier + coefPlusDb30); //увеличение спекторв после 6 для равномерной картинки
                    }
                    Debug.Log("Играет DB30 без шумелки");
                }

            }
            else if (isShaX)
            {
                if(isHideVibroActive)
                {
                    scaleY = sum / averageSize * visualModifier;
                }
                else
                {
                    if(visualIndex<3)
                    {
                        scaleY = sum / averageSize * visualModifier;
                    }
                    else
                    {
                        Debug.Log("Вибрация без устройства защиты");
                        scaleY = sum / averageSize * (visualModifier + coefPlusIsShax);
                    }
                }
            }
            else
            {
                
                scaleY = sum / averageSize * visualModifier; //defualt String
                Debug.Log("дефолт настройки семплов - " + scaleY);
            }

            
            visualScale[visualIndex] -= Time.deltaTime * smoothSpeed;

            if (visualScale[visualIndex]<scaleY)
            {
                visualScale[visualIndex] = scaleY;
            }

            if (visualScale[visualIndex]>maxVisualScale)
            {
                visualScale[visualIndex] = maxVisualScale;
            }
            visualList[visualIndex].localScale = (Vector3.one * coefSize) + Vector3.up * visualScale[visualIndex];



            visualIndex++;
            Debug.Log(visualModifier);


            //if (isDb30)
            //{
            //    if (visualIndex < 6)
            //    {
            //        visualList[visualIndex].localScale = (Vector3.one * coefSize) + Vector3.up * visualScale[visualIndex];
            //    }
            //    else
            //    {
            //        Debug.Log("isDbsim визуалиндекс больше 6");
            //        visualList[visualIndex].localScale = (Vector3.one * coefSize) + Vector3.up * (visualScale[visualIndex]*coefMiltiplication);

            //    }
            //}
            //else if (isShaX)
            //{
            //    if (visualIndex<3)
            //    {
            //        visualList[visualIndex].localScale = (Vector3.one * coefSize) + Vector3.up * visualScale[visualIndex];
            //    }
            //    else
            //    {
            //        visualList[visualIndex].localScale = (Vector3.one * coefSize) + Vector3.up * visualScale[visualIndex]*coefMiltiplication;
            //    }
            //}
            //else
            //{
            //defual string

        }
    }

    private void UpdateVisualLevelCorrection()
    {
        int visualIndex = 0;
        int spectrumIndex = 0;
        int avgSize = (int)(SAMPLE_SIZE * keepPercentageLevelCorection) / 4;

        while (visualIndex<4)
        {
            int j = 0;
            float sum = 0;

            while (j<avgSize)
            {
                sum += spectrum[spectrumIndex];
                spectrumIndex++;
                j++;
            }
            float scaleY = sum/ avgSize * visualModifierLevelCorrection;
            levelVisualScale[visualIndex] -= Time.deltaTime*smoothSpeed;

            if (levelVisualScale[visualIndex]<scaleY)
            {
                levelVisualScale[visualIndex] = scaleY;
            }

            if (levelVisualScale[visualIndex] > maxVisualScale)
            {
                levelVisualScale[visualIndex] = maxVisualScale;
            }

            levelVisualList[visualIndex].localScale = (Vector3.one * coefSize) + Vector3.up * levelVisualScale[visualIndex];
            visualIndex++;
        }
    }

    IEnumerator UpdateMetric(int i)
    {
        yield return new WaitForSeconds(1.5f);
        
        float currentValue = mergeVisualList[i].localScale.y / 10f;
        currentValueText.text = Math.Round(currentValue,1).ToString();
        if(mergeVisualList[i].GetComponent<MaxMinAvgValueCube>())
        {
            maxCurrentValueText.text = mergeVisualList[i].GetComponent<MaxMinAvgValueCube>().ReturnMaxValue().ToString();

            minCurrentValueText.text = mergeVisualList[i].GetComponent<MaxMinAvgValueCube>().ReturnMinValue().ToString();
        }
        

        UpdateAllRange();

        StopCoroutine(lastRoutine);
        lastRoutine = StartCoroutine(UpdateMetric(i));
    }
    
    //----------------------->public
    public void SwitchAudioSourceOnGenerator(AudioSource generatorSource)
    {
        generatorWhiteNoiseAudio = generatorSource;
        generatorWN = generatorWhiteNoiseAudio.gameObject;
        source = generatorWhiteNoiseAudio;
    }
    public void RemoveAudioSourceGenerator()
    {
        source = GetComponent<AudioSource>();
    }

    private void RecolorRange(int range, int prevRange)
    {
        MeshRenderer meshRenderPrevRange = mergeVisualList[prevRange].GetComponent<MeshRenderer>();
        MeshRenderer meshRenderCurrRange = mergeVisualList[range].GetComponent<MeshRenderer>();

        meshRenderPrevRange.material = defaultMat;
        meshRenderCurrRange.material = selectMat;
    }

    public void SwitchCurrentRange(int step)
    {
        if (isLeftRight)
        {
            tempRange += step;


            if (tempRange >= 0 && tempRange <= mergeVisualList.Count - 1)
            {
                currentRange = tempRange;
            }
            else if (tempRange < 0)
            {
                currentRange = mergeVisualList.Count - 1;
                tempRange = mergeVisualList.Count - 1;
            }
            else if (tempRange > mergeVisualList.Count - 1)
            {
                currentRange = 0;
                tempRange = 0;
            }
            if (isDb30)
            {
                hzRange.text = db30RangeText[currentRange];
            }
            else if(isDbsim)
            {
                hzRange.text = dbsimRangeText[currentRange];
            }
            else if (isVhaX)
            {
                hzRange.text = vhaxRangeText[currentRange];
            }
            else if (isShaX)
            {
                hzRange.text = shaxRangeText[currentRange];
            }

            StopCoroutine(lastRoutine);
            lastRoutine = StartCoroutine(UpdateMetric(currentRange));

            RecolorRange(currentRange, lastRange);

            lastRange = currentRange;
        }
    }


    public void UpdateAllRange()
    {
        if (isLeftRight)
        {
            if (isDb30)
            {
                for (int i = 0; i < tableRangeTextList.Count; i++)
                {
                    float roundValue = (float)Math.Round(visualList[i].localScale.y / 10f, 1);
                    tableRangeTextList[i].text = roundValue.ToString();
                }
            }

            if (isDbsim)
            {
                for (int i = 0; i < tableRangeDbSImTextList.Count; i++)
                {
                    float roundValue = (float)Math.Round(visualList[i].localScale.y / 10f, 1);
                    tableRangeDbSImTextList[i].text = roundValue.ToString();
                }
                for (int i = 0; i < tableRangeLevelCorection.Count; i++)
                {
                    float roundValue = (float)Math.Round(levelVisualList[i].localScale.y / 10f, 1);
                    tableRangeLevelCorection[i].text = roundValue.ToString();
                }
            }

            if(isVhaX)
            {
                for (int i = 0; i < tableRangeVhaxList.Count; i++)
                {
                    float roundValue = (float)Math.Round(visualList[i].localScale.y / 10f, 1);
                    tableRangeVhaxList[i].text = roundValue.ToString();
                }
            }

            if(isShaX)
            {
                for (int i = 0; i < tableRangeShaxList.Count; i++)
                {
                    float roundValue = (float)Math.Round(visualList[i].localScale.y / 10f, 1);
                    tableRangeShaxList[i].text = roundValue.ToString();
                }
            }
        }    
    }

    public void BarChartSwitchOnTable()
    {
        isSwitchChartBarOnTable = !isSwitchChartBarOnTable;

        foreach (Transform visual in mergeVisualList)
        {
            visual.GetComponent<MaxMinAvgValueCube>().ShowHide();
        }


        lineGroupChartBar.SetActive(!isSwitchChartBarOnTable);
        lsTextMain.SetActive(!isSwitchChartBarOnTable);
        minMaxGroup.SetActive(!isSwitchChartBarOnTable);
        hzRange.gameObject.SetActive(!isSwitchChartBarOnTable);
        parentSpawn.SetActive(!isSwitchChartBarOnTable);
        parentSpawnLevelCorrection.SetActive(!isSwitchChartBarOnTable);
        currentValueText.gameObject.SetActive(!isSwitchChartBarOnTable);

        dBTable.SetActive(isSwitchChartBarOnTable);
        lsTextSecond.SetActive(isSwitchChartBarOnTable);
    }
    public void BarChartSwitchOnTableOff()
    {
        if (isSwitchChartBarOnTable)
        {
            foreach (Transform visual in mergeVisualList)
            {
                visual.GetComponent<MaxMinAvgValueCube>().ShowLine();
            }


            lineGroupChartBar.SetActive(true);
            lsTextMain.SetActive(true);
            minMaxGroup.SetActive(true);
            hzRange.gameObject.SetActive(true);
            parentSpawn.SetActive(true);
            parentSpawnLevelCorrection.SetActive(true);
            currentValueText.gameObject.SetActive(true);

            dBTable.SetActive(false);
            lsTextSecond.SetActive(false);

            isSwitchChartBarOnTable = false;
        }
    }

    public void AcustModeSelect(int idMode)
    {

        switch (idMode)
        {
            case 0:
                isDbsim = true;
                isDb30 = false;
                isVhaX = false;
                isShaX = false;
                break;
            case 1:
                isDbsim = false;
                isDb30 = true;
                isVhaX = false;
                isShaX = false;
                break;
        }
    }

    public void VibroAcustModeSelect(int idMode)
    {

        switch (idMode)
        {
            case 0:
                isVhaX = true;
                isShaX = false;
                isDbsim = false;
                isDb30 = false;
                break;
            case 1:
                isVhaX = false;
                isShaX = true;
                isDbsim = false;
                isDb30 = false;
                break;
        }
    }

    public void CurrentRangeReset()
    {
        tempRange = 0;
        currentRange= 0;
        lastRange = 0;
    }

    public void LineMaxMinVisual()
    {
        foreach (Transform visual in mergeVisualList)
        {
            visual.GetComponent<MaxMinAvgValueCube>().MaxMinValueLine();
        }
        //foreach (Transform visual in visualList)
        //{
        //    visual.GetComponent<MaxMinAvgValueCube>().MaxMinValueLine();
        //}
        //if (isDbsim)
        //{
        //    foreach(Transform visual in levelVisualList)
        //    {
        //        visual.GetComponent<MaxMinAvgValueCube>().MaxMinValueLine();
        //    }
        //}
    }

    public void CubeScaleSet()
    {
        if (isDbsim)
        {
            foreach (Transform visual in mergeVisualList)
            {
                visual.GetComponent<MaxMinAvgValueCube>().SwitchModeDb(true,false,false,false);
            }
        }
        else if (isDb30)
        {
            foreach (Transform visual in visualList)
            {
                visual.GetComponent<MaxMinAvgValueCube>().SwitchModeDb(false, true, false, false);
            }
        }
        else if(isVhaX)
        {
            foreach (Transform visual in visualList)
            {
                visual.GetComponent<MaxMinAvgValueCube>().SwitchModeDb(false,false,true,false);
            }
        }
        else if(isShaX)
        {
            foreach (Transform visual in visualList)
            {
                visual.GetComponent<MaxMinAvgValueCube>().SwitchModeDb(false, false, false, true);
            }
        }

    }

    public void ModParamScaleUp()
    {
        Debug.Log("Повышаем параметры модификатора для ГБ");
        
        if(generatorWN.GetComponent<GeneratorWhiteNoiseControl>().WhiteNoisIsPlay())
        {
            visualModifier = visualModForGeneratorWhiteNoiseDbsim;
            visualModifierLevelCorrection = visualModForGeneratorWhiteNoiseDbsim;
        }
        else
        {
            visualModifier = visualModForGeneratorPinkNoiseDbsim;
            visualModifierLevelCorrection = visualModForGeneratorPinkNoiseDbsim;
        }
        
        //visualModifier = 4500f;
        //visualModifierLevelCorrection = 4500f;
    }
    public void ModParamScaleUpDb30()
    {
        Debug.Log("Повышаю 30");
        if (generatorWN.GetComponent<GeneratorWhiteNoiseControl>().WhiteNoisIsPlay())
        {
            visualModifier = visualModForGeneratorDb30;
        }
        else
        {
            visualModifier = visualModForGeneratorPinkDb30;
        }
           
    }
    public void ModParamScaleDown()
    {
        Debug.Log("Понижаем параметры модификатора дял ГБ");

        visualModifier = saveVisualMod;
        visualModifierLevelCorrection = saveVisualMod;
        //visualModifier = 4500f;
        //visualModifierLevelCorrection = 4500f;
    }


    //виброизмерения
    public void IfSonataSetAndActive() //Если устройство защиты активно
    {
        isHideVibroActive= true;

        tempVisualModifier = visualModifier;
        visualModifier = 12000;
        hideNoiseSoure.volume = 1f;
        hideNoiseSoure.Play();
        source = hideNoiseSoure;
    }
    public void IfSonataSetNotActive()
    {
        isHideVibroActive= false;
        
        tempVisualModifier = visualModifier;

        visualModifier = 3500f;
        source.volume = 0.7f;
    }
    

    public void FreeModVibroAnalaizer()
    {
        visualModifier = tempVisualModifier;
        vibroSound.volume = 0.15f;
        hideNoiseSoure.volume = 0;
        hideNoiseSoure.Stop();
        if (controlAssistence.IsModeSelect())
        {
            source = mainSource;
        }
        else
        {
            source = vibroSound;
        }
        
    }

    public void AudioSourceAcustChange()
    {
        source= mainSource;
    }
}
