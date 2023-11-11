using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class ControlAssistence : MonoBehaviour
{
    [Header("Экран загрузки и выбора режима")]
    [SerializeField] GameObject loadScreen;
    [SerializeField] GameObject modeSelectScreen;
    [SerializeField] GameObject acustModeSelectScreen;
    [SerializeField] GameObject vibroModeSelectScreen;

    [Header("Экраны отображения таблиц и гистограмм")]
    [SerializeField] GameObject screenChartAndTable;
    [SerializeField] Image tableModImage;
    [SerializeField] TMP_Text modJobeText;
    [SerializeField] List<Sprite> tableImageList;
    [SerializeField] List<GameObject> RangedB30List;
    [SerializeField] List<GameObject> RangedBSimList;
    [SerializeField] List<GameObject> RangeVhaxList;
    [SerializeField] List<GameObject> RangeShaxList;
    [SerializeField] GameObject zoomTable;
    [SerializeField] GameObject zoomTableButton;

    [Header("Стрелка выбора")]
    [SerializeField] GameObject arrowPosition;
    [SerializeField] GameObject arrowPick;
    [SerializeField] List<Transform> arrowPositionList;

    [Header("Кнопкпи управления")]
    [SerializeField] List<GameObject> buttonsList;
    [SerializeField] GameObject buttonOnOff;
    [SerializeField] GameObject buttonScreen;
    [SerializeField] GameObject buttonOk;

    [Header("Генератор спектра и таймер")]
    [SerializeField] SpectrumAnalayzer30 spectrumAnalayzer30;
    [SerializeField] Timer timer;

    private bool isOnOffAnalyzerl = false;
    [SerializeField] private bool inModeSelect = false;
    [SerializeField] private bool inAcustModeSelect = false;
    [SerializeField] private bool inVibroModeSelect = false;
    [SerializeField] private bool chartAndTableVisible = false;
    public bool isZoomed = false;
    private int tempStep = 0;
    public List<GetSlotSonata> getSlotSonatas;

    [Header("Анимации антена/штекер")]
    [SerializeField] AntenaAnim antenaAnim;
    [SerializeField] ShtekerAnim shtekerAnim;

    [SerializeField] EnableDisableAnalyzer enableDisableAnalyzer;


    //public metods
    public void ArrowMoveInMenu(int step)
    {
        if (chartAndTableVisible == false)
        {
            tempStep = +step;

            if (tempStep > 1)
            {
                tempStep--;
            }
            else if (tempStep < 0)
            {
                tempStep++;
            }
            arrowPick.transform.position = arrowPositionList[tempStep].position;
        }
    }

    public void ConfirmModeWork()
    {
        CheckModeSelect(tempStep);
    }

    public void AnalyzerOnOff()
    {
        if (isOnOffAnalyzerl == false)
        {
            loadScreen.SetActive(true);
            StartCoroutine(LoadingAnalayzer());
        }
        else
        {
            OffAnalayzer();
        }
    }

    public void DisableVibroSlotStay()
    {
        foreach (GetSlotSonata slotSonata in getSlotSonatas)
        {
            if (slotSonata.IsActiveScript())
            {
                slotSonata.EnableDisableMoveToPoint();
            }
        }
    }

    public void ModeMenuPanel()
    {
        spectrumAnalayzer30.BarChartSwitchOnTableOff();

        foreach(GetSlotSonata slotSonata in getSlotSonatas)
        {
            if (slotSonata.IsActiveScript())
            {
                slotSonata.EnableDisableMoveToPoint();
            }
        }

        if (buttonOk.activeSelf == false)
        {
            buttonOk.SetActive(true);
        }
 
        zoomTable.SetActive(false);
        zoomTableButton.SetActive(false);
        isZoomed = false;
        enableDisableAnalyzer.VibroModeActive(false);
        spectrumAnalayzer30.CurrentRangeReset();
        modeSelectScreen.SetActive(true);
        spectrumAnalayzer30.enabled = false;
        timer.EnabledDisabledTimer(false);
        arrowPosition.SetActive(true);
        inAcustModeSelect = false;
        inVibroModeSelect= false;
        chartAndTableVisible = false;
        screenChartAndTable.SetActive(false);
        acustModeSelectScreen.SetActive(false);
        //spectrumAnalayzer30.LineMaxMinVisual();
        vibroModeSelectScreen.SetActive(false);
        buttonScreen.SetActive(false);
        inModeSelect = true;
    }

    public void SubModeMenuOpen()
    {
        spectrumAnalayzer30.BarChartSwitchOnTableOff();

        foreach (GetSlotSonata slotSonata in getSlotSonatas)
        {
            if (slotSonata.IsActiveScript())
            {
                slotSonata.EnableDisableMoveToPoint();
            }
        }

        if (buttonOk.activeSelf == false)
        {
            buttonOk.SetActive(true);
        }

        buttonScreen.SetActive(false);
        zoomTable.SetActive(false);
        zoomTableButton.SetActive(false);
        isZoomed = false;
        spectrumAnalayzer30.CurrentRangeReset();
        spectrumAnalayzer30.enabled = false;
        timer.EnabledDisabledTimer(false);
        arrowPosition.SetActive(true);
        chartAndTableVisible = false;
        screenChartAndTable.SetActive(false);
        //spectrumAnalayzer30.LineMaxMinVisual();

        if (inAcustModeSelect)
        {
            acustModeSelectScreen.SetActive(true);
        }
        else if (inVibroModeSelect)
        {
            vibroModeSelectScreen.SetActive(true);
        }
    }

    public bool ReturnActivAnalayzerOrNot()
    {
        return isOnOffAnalyzerl;
    }

    //private metods
    private void CheckModeSelect(int arrowPosition)
    {
        
        if (inModeSelect)
        {
            switch (arrowPosition)
            {
                case 0:
                    inAcustModeSelect=true;
                    enableDisableAnalyzer.VibroModeActive(false);
                    shtekerAnim.ShtekerOff();
                    antenaAnim.AntenaOn();
                    acustModeSelectScreen.SetActive(true);                    
                    break;

                case 1:                    
                    inVibroModeSelect = true;
                    enableDisableAnalyzer.VibroModeActive(true);
                    antenaAnim.AntenaOff();
                    shtekerAnim.ShtekerOn();
                    vibroModeSelectScreen.SetActive(true);
                    break;
            }
            inModeSelect = false;
            modeSelectScreen.SetActive(false);
        }
        else if (inAcustModeSelect) 
        {
            switch (arrowPosition)
            {
                case 0:
                    spectrumAnalayzer30.AcustModeSelect(0);
                    tableModImage.sprite = tableImageList[0];
                    modJobeText.text = "dBSIm";
                    buttonScreen.SetActive(true);
                    buttonOk.SetActive(false);

                    DisableTableList();
                    foreach (GameObject RangeText30 in RangedB30List)
                    {
                        RangeText30.SetActive(false);
                    }
                    foreach (GameObject RangeTextdBSIm in RangedBSimList)
                    {
                        RangeTextdBSIm.SetActive(true);
                    }
                    break;

                case 1:
                    spectrumAnalayzer30.AcustModeSelect(1);
                    tableModImage.sprite = tableImageList[1];
                    modJobeText.text = "dB1/3";
                    buttonScreen.SetActive(true);
                    buttonOk.SetActive(false);

                    DisableTableList();
                    foreach (GameObject RangeTextdBSIm in RangedBSimList)
                    {
                        RangeTextdBSIm.SetActive(false);
                    }
                    foreach (GameObject RangeText30 in RangedB30List)
                    {
                        RangeText30.SetActive(true);
                    }
                    break;
            }
            spectrumAnalayzer30.AudioSourceAcustChange();
            acustModeSelectScreen.SetActive(false);
            screenChartAndTable.SetActive(true);
            spectrumAnalayzer30.enabled = true;
            chartAndTableVisible = true;
            this.arrowPosition.SetActive(false);
            spectrumAnalayzer30.isLeftRight = true;
            timer.EnabledDisabledTimer(true);
            spectrumAnalayzer30.LineMaxMinVisual();
            spectrumAnalayzer30.CubeScaleSet();
            zoomTableButton.SetActive(true);
            
        }
        else if (inVibroModeSelect)
        {
            switch (arrowPosition)
            {
                case 0:
                    spectrumAnalayzer30.VibroAcustModeSelect(0);
                    tableModImage.sprite = tableImageList[2];
                    modJobeText.text = "VhaX";
                    buttonScreen.SetActive(true);
                    buttonOk.SetActive(false);

                    DisableTableList();
                    foreach (GameObject RangeText30 in RangeShaxList)
                    {
                        RangeText30.SetActive(false);
                    }
                    foreach (GameObject RangeTextdBSIm in RangeVhaxList)
                    {
                        RangeTextdBSIm.SetActive(true);
                    }
                    break;
                case 1:
                    spectrumAnalayzer30.VibroAcustModeSelect(1);
                    tableModImage.sprite = tableImageList[1];
                    modJobeText.text = "Shax";
                    buttonScreen.SetActive(true);
                    buttonOk.SetActive(false);

                    DisableTableList();
                    foreach (GameObject RangeTextdBSIm in RangeVhaxList)
                    {
                        RangeTextdBSIm.SetActive(false);
                    }
                    foreach (GameObject RangeText30 in RangeShaxList)
                    {
                        RangeText30.SetActive(true);
                    }
                    break;
            }
            FindObjectOfType<WhallSlotCheck>().EnableScript();
            vibroModeSelectScreen.SetActive(false);
            screenChartAndTable.SetActive(true);
            spectrumAnalayzer30.enabled = true;
            chartAndTableVisible = true;
            this.arrowPosition.SetActive(false);
            spectrumAnalayzer30.isLeftRight = true;
            timer.EnabledDisabledTimer(true);
            spectrumAnalayzer30.LineMaxMinVisual();
            spectrumAnalayzer30.CubeScaleSet();
            zoomTableButton.SetActive(true);
            
        }
        tempStep = 0;
        arrowPick.transform.position = arrowPositionList[tempStep].position;
    }

    IEnumerator LoadingAnalayzer()
    {
        buttonOnOff.SetActive(false);

        yield return new WaitForSeconds(1.5f);

        foreach (GameObject button in buttonsList)
        {
            button.SetActive(true);
        }

        modeSelectScreen.SetActive(true);
        loadScreen.SetActive(false);
        arrowPosition.SetActive(true);
        isOnOffAnalyzerl = true;
        inModeSelect = true;
        buttonOnOff.SetActive(true);
        spectrumAnalayzer30.CurrentRangeReset();
    }

    private void DisableTableList()
    {
        foreach (GameObject item in RangedB30List)
        {
            item.SetActive(false);
        }
        foreach (GameObject item in RangedBSimList)
        {
            item.SetActive(false);
        }
        foreach (GameObject item in RangeVhaxList)
        {
            item.SetActive(false);
        }
        foreach (GameObject item in RangeShaxList)
        {
            item.SetActive(false);
        }
    }
    private void OffAnalayzer()
    {
        spectrumAnalayzer30.BarChartSwitchOnTableOff();

        foreach (GetSlotSonata slotSonata in getSlotSonatas)
        {
            if (slotSonata.IsActiveScript())
            {
                slotSonata.EnableDisableMoveToPoint();
            }
        }

        buttonScreen.SetActive(false);
        zoomTable.SetActive(false);
        zoomTableButton.SetActive(false);
        isZoomed = false;
        antenaAnim.Diactivate();
        shtekerAnim.Diactivate();
        enableDisableAnalyzer.VibroModeActive(false);
        spectrumAnalayzer30.CurrentRangeReset();
        tempStep = 0;
        arrowPick.transform.position = arrowPositionList[tempStep].position;

        spectrumAnalayzer30.isLeftRight = false;

        arrowPosition.SetActive(false);
        screenChartAndTable.SetActive(false);
        modeSelectScreen.SetActive(false);
        acustModeSelectScreen.SetActive(false);
        vibroModeSelectScreen.SetActive(false);

        foreach (GameObject button in buttonsList)
        {
            button.SetActive(false);
        }

        isOnOffAnalyzerl = false;
        inModeSelect = false;
        inAcustModeSelect = false;
        inVibroModeSelect = false;
        chartAndTableVisible = false;

        spectrumAnalayzer30.enabled = false;
        timer.EnabledDisabledTimer(false);
        //spectrumAnalayzer30.LineMaxMinVisual();
    }

    public void ZoomedTable()
    {
        if (isZoomed == false)
        {
            zoomTable.SetActive(true);
            isZoomed= true;
        }
        else
        {
            zoomTable.SetActive(false);
            isZoomed = false;
        }
    }

    public bool IsModeSelect()
    {
        return inModeSelect;
    }
}
