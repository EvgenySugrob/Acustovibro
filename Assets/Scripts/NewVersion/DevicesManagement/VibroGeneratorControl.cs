using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibroGeneratorControl : MonoBehaviour
{
    [Header("Позиция камеры для перемещения")]
    [SerializeField] GameObject _mainCamera;
    [SerializeField] Transform _targetPoint;
    [SerializeField] Vector3 _startPoint;
    [SerializeField] Quaternion _startRotation;
    [SerializeField] float _speedMove = 3f;
    [SerializeField] float _speedRotation = 5f;
    [SerializeField, HideInInspector] float minDistanceBetweenPoint = 0.005f;
    [SerializeField] GameObject _returnBt;
    [SerializeField] ParticleSystem _particleSystem;

    public bool placeOnWhall=false;

    private Quaternion _targetRotation;

    [Header("Прочее")]
    [SerializeField] GameObject _settingPanelVibro;

    [SerializeField] bool _generatorIsOn;
    [SerializeField] private bool isMove = false;
    [SerializeField] private bool inGenerator = false;

    public void GeneratorOnOff(bool isOn)
    {
        _generatorIsOn = isOn;

        if (_generatorIsOn)
        {
            _particleSystem.Play();
        }
        else
        {
            _particleSystem.Stop();
        }
        SettingPanelGWNTextSet settingPanelGWNText = _settingPanelVibro.GetComponent<SettingPanelGWNTextSet>();
        settingPanelGWNText.TextGeneratorActive(_generatorIsOn);
    }

    public void SetReturnBt(GameObject button)
    {
        _returnBt = button;
    }

    public void SetMainCamera(GameObject mainCamera)
    {
        _mainCamera = mainCamera;
        _startPoint = _mainCamera.transform.position;
        _startRotation = _mainCamera.transform.rotation;
        EnableDisableMoveToGenerator();
    }

    public void SetSettingPanel(GameObject settingPanel)
    {
        _settingPanelVibro= settingPanel;
        SettingPanelGWNTextSet settingPanelGWNText = _settingPanelVibro.GetComponent<SettingPanelGWNTextSet>();

        settingPanelGWNText.SetToogleGenerator(_generatorIsOn);
    }

    public void EnableDisableMoveToGenerator()
    {
        inGenerator = !inGenerator;
        isMove = !isMove;
    }

    private void Start()
    {
        //Debug.Log(_targetPoint.rotation);
    }

    private void MoveToGenerator()
    {
        _mainCamera.GetComponent<CameraRotateAround>().enabled = false;
        _mainCamera.GetComponent<InventoryReplaceItem>().enabled = false;

        _mainCamera.transform.position = Vector3.Lerp(_mainCamera.transform.position, _targetPoint.position, _speedMove * Time.deltaTime);
        _mainCamera.transform.rotation = Quaternion.Lerp(_mainCamera.transform.rotation, _targetPoint.rotation, _speedRotation * Time.deltaTime);

        if (Vector3.Distance(_mainCamera.transform.position, _targetPoint.position) < minDistanceBetweenPoint)
        {
            isMove = false;
            _settingPanelVibro.SetActive(true);
            _returnBt.SetActive(true);
        }
    }
    private void MoveAwayFromGenerator()
    {
        _settingPanelVibro.SetActive(false);
        _returnBt.SetActive(false);

        _mainCamera.transform.position = Vector3.Lerp(_mainCamera.transform.position, _startPoint, _speedMove * Time.deltaTime);
        _mainCamera.transform.rotation = Quaternion.Lerp(_mainCamera.transform.rotation, _startRotation, _speedRotation * Time.deltaTime);

        if (Vector3.Distance(_mainCamera.transform.position, _startPoint) < minDistanceBetweenPoint)
        {
            isMove = false;


            _mainCamera.GetComponent<CameraRotateAround>().enabled = true;
            _mainCamera.GetComponent<InventoryReplaceItem>().enabled = true;
        }
    }

    private void Update()
    {
        if (inGenerator && isMove)
        {
            MoveToGenerator();
        }
        else if (!inGenerator && isMove)
        {
            MoveAwayFromGenerator();
        }
    }

    public bool GeneratorEnable()
    {
        return _generatorIsOn;
    }
}
