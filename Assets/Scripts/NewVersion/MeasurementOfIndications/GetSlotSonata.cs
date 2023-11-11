using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetSlotSonata : MonoBehaviour
{
    [SerializeField] SpectrumAnalayzer30 spectrumAnalayzer;
    [SerializeField] SonataSlotsCheck sonataSlotsCheck;

    [SerializeField] GameObject _mainCamera;
    [SerializeField] Transform targetPoint;
    [SerializeField] Transform targetPoint1;
    [SerializeField] Vector3 _startPoint;
    [SerializeField] Quaternion _startRotation;
    [SerializeField] float _speedMove = 3f;
    [SerializeField] float _speedRotation = 5f;
    [SerializeField, HideInInspector] float minDistanceBetweenPoint = 0.005f;
    [SerializeField] GameObject returnBt;

    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] GameObject buttonOk;
    [SerializeField] List<GameObject> buttonsList;
    
    private Transform finalTargetPont;
    [SerializeField] private bool isActive;
    [SerializeField] bool isEngeneer;
    private bool inPoint;
    private bool isMove;

    [SerializeField] bool analyzerIsCheckThisPoint;

    //--------------------------------------->public
    public void ActiveScripts()
    {
        if(isActive)
        {
            isActive = false;
        }
        else
        {
            isActive = true;
        }
        
    }

    public bool IsActiveScript()
    {
        return isActive;
    }

    public void SaveStartPositionCamera()
    {
        _startPoint = _mainCamera.transform.position;
        _startRotation = _mainCamera.transform.rotation;
    }

    public void EnableDisableMoveToPoint()
    {
        if (isActive)
        {
            inPoint = !inPoint;
            isMove = !isMove;
        }
    }

    public void CalculationNearestPoint(Vector3 point)
    {
        if (isEngeneer == false)
        {
            float firstDist = Vector3.Distance(_mainCamera.transform.position, targetPoint.position);
            float secondDist = Vector3.Distance(_mainCamera.transform.position, targetPoint1.position);

            if (firstDist < secondDist)
            {
                finalTargetPont = targetPoint;
            }
            else
            {
                finalTargetPont = targetPoint1;
            }
        }
        else
        {
            finalTargetPont= targetPoint;
        }
        

        EnableDisableMoveToPoint();
    }

    public bool ReturnAnalyzerIsCheckThisPoint()
    {
        return analyzerIsCheckThisPoint;
    }

    //--------------------------------------->private

    private void Update()
    {
        if (inPoint && isMove)
        {
            MoveToPoint();
        }
        else if (!inPoint && isMove)
        {
            MoveAwayFromPoint();
        }
    }

    private void ButtonsDisableEnable(bool isOn)
    {
        Debug.Log("ННАЧАЛО!");
        foreach (GameObject button in buttonsList)
        {
            Debug.Log(button.name);
            button.SetActive(isOn);
        }
    }

    private void MoveToPoint()
    {
        Debug.Log("Поехал на точку");
        spectrumAnalayzer.IsWhallConected = true;
        analyzerIsCheckThisPoint= true;
        ButtonsDisableEnable(false);

        _mainCamera.GetComponent<WhallSlotCheck>().enabled= false;
        _mainCamera.GetComponent<CameraRotateAround>().enabled = false;
        //_mainCamera.GetComponent<InventoryReplaceItem>().enabled = false;

        _mainCamera.transform.position = Vector3.Lerp(_mainCamera.transform.position, finalTargetPont.position, _speedMove * Time.deltaTime);
        _mainCamera.transform.rotation = Quaternion.Lerp(_mainCamera.transform.rotation, finalTargetPont.rotation, _speedRotation * Time.deltaTime);

        if (Vector3.Distance(_mainCamera.transform.position, finalTargetPont.position) < minDistanceBetweenPoint)
        {
            isMove = false;
            finalTargetPont.transform.GetChild(0).gameObject.SetActive(true);
            lineRenderer.enabled = true;
            //_settingPanelGWN.SetActive(true);
            VibroAnalayzerEnable();
            returnBt.SetActive(true);
            ButtonsDisableEnable(true);
        }
    }
    private void MoveAwayFromPoint()
    {
        ButtonsDisableEnable(false);
        buttonOk.SetActive(false);
        VibroAnalayzerDisable();
        returnBt.SetActive(false);
        lineRenderer.enabled=false;
        finalTargetPont.transform.GetChild(0).gameObject.SetActive(false);

        _mainCamera.transform.position = Vector3.Lerp(_mainCamera.transform.position, _startPoint, _speedMove * Time.deltaTime);
        _mainCamera.transform.rotation = Quaternion.Lerp(_mainCamera.transform.rotation, _startRotation, _speedRotation * Time.deltaTime);

        if (Vector3.Distance(_mainCamera.transform.position, _startPoint) < minDistanceBetweenPoint)
        {
            isMove = false;

            
            _mainCamera.GetComponent<CameraRotateAround>().enabled = true;
            _mainCamera.GetComponent<WhallSlotCheck>().EnableScript();
            ActiveScripts();
            //_mainCamera.GetComponent<InventoryReplaceItem>().enabled = true;
            ButtonsDisableEnable(true);
            buttonOk.SetActive(true);
            spectrumAnalayzer.IsWhallConected = false;
            analyzerIsCheckThisPoint= false;
        }
    }

    private void VibroAnalayzerDisable()
    {
        spectrumAnalayzer.FreeModVibroAnalaizer();
    }
    private void VibroAnalayzerEnable()
    {
        if (isEngeneer)
        {
            VibroAnalyzerEngeneer();
        }
        else
        {
            VibroAnalayzer();
        }
    }

    private void VibroAnalyzerEngeneer() //измерение с инженерной конструкции
    {
        //Если слот занят
        if (sonataSlotsCheck.DeviceInstalled())
        {//Если включенно устройство и выключено
            if (sonataSlotsCheck.OnEnebledSonataSlot())
            {
                Debug.Log("Устройство защиты работает");
                spectrumAnalayzer.IfSonataSetAndActive();
            }
            else
            {
                spectrumAnalayzer.IfSonataSetNotActive();
                Debug.Log("Устройство защиты есть, но не работает");
            }

        }
        else
        {
            spectrumAnalayzer.IfSonataSetNotActive();
            Debug.Log("Устройства защиты НЕТ");
        }
    }
    private void VibroAnalayzer() //Измерение со стен
    {
        if(sonataSlotsCheck.IsSlotFree())
        {
            spectrumAnalayzer.IfSonataSetNotActive();
            Debug.Log("Соната есть, но выключенна");
        }
        else
        {
            if(sonataSlotsCheck.OnEnabledSingleSlot())
            {
                spectrumAnalayzer.IfSonataSetAndActive();
                Debug.Log("Соната активна");
            }
            else
            {
                spectrumAnalayzer.IfSonataSetNotActive();
                Debug.Log("Соната есть, но выключенна");
            }
            
        }
    }
}
