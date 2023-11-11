using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogsMenuReplace : MonoBehaviour
{
    [SerializeField] Inventory_MouseControlInput_Example _mouseControlInput;
    [SerializeField] InventoryReplaceItem _inventoryReplace;
    [SerializeField] GameObject _dialogsMenu;
    [SerializeField] CheckDevicesWhenMaterialSwitch _checkDevices;
    [SerializeField] GameObject _mainCamera;
    [SerializeField] GameObject _settingPanelGWN;
    [SerializeField] GameObject _settingsPanelVibro;
    [SerializeField] GameObject _buttonBack;
    [SerializeField] GameObject _horizontalTools;

    private GeneratorWhiteNoiseControl generatorWhitenoise;
    private VibroGeneratorControl vibroGenerator;
    [SerializeField]GameObject _hitObject;

   [SerializeField] bool isGeneratorSelect;
   [SerializeField] bool isVibroGenSelect;

    [Header("Слот ГБШ")]
    [SerializeField] CountPlaceDevices _countPlaceDevices;

    [Header("Зона реверберации и анализ")]
    [SerializeField] SpectrumAnalayzer30 spectrumAnalayzer;
    [SerializeField] GameObject audioReverbZone;

    public void DialogsmenuOpen(GameObject hitObject)
    {
        _hitObject= hitObject;
        //_hitObject = _hitObject.GetComponent<ItemParent>().GetParent();
        Debug.Log(_hitObject.name + " объект");
        if (_inventoryReplace.IsSystemActive() == false)
        {
            OpenCloseDialogsMenu(true);
        }
        
    }

    public void ConfirmReplace()
    {
        OpenCloseDialogsMenu(false);
        _mouseControlInput.ConfirmRepaetReplace(_hitObject);
    }

    public void EnableDisableDevices()
    {
        OpenCloseDialogsMenu(false);

        GameObject useObj = _hitObject.GetComponent<ItemParent>().GetParent();

        if (useObj.GetComponent<ItemsForReplace>().GetGenerator()) 
        {
            generatorWhitenoise = useObj.GetComponent<GeneratorWhiteNoiseControl>();
            generatorWhitenoise.SetSettingPanel(_settingPanelGWN);
            generatorWhitenoise.SetMainCamera(_mainCamera);
            generatorWhitenoise.SetReturnBt(_buttonBack);

            generatorWhitenoise.SetAnalayzerAndReverbZone(spectrumAnalayzer,audioReverbZone);
            isGeneratorSelect = true;
        }
        else
        {
            vibroGenerator = useObj.GetComponent<VibroGeneratorControl>();
            vibroGenerator.SetSettingPanel(_settingsPanelVibro);
            vibroGenerator.SetMainCamera(_mainCamera);
            vibroGenerator.SetReturnBt(_buttonBack);
            isVibroGenSelect= true;
        }
        _horizontalTools.SetActive(false);

    }
    public void DeleteObject()
    {
        if (_countPlaceDevices.GeneratorIsReplace() == false)
        {
            Debug.Log("clear");
            spectrumAnalayzer.RemoveAudioSourceGenerator();
            _countPlaceDevices.ClearSlot();
        }
        //_checkDevices.RemoveSingleDevices(_hitObject.GetComponent<ItemParent>().GetParent());
        _checkDevices.DeleteChoiseItem(_hitObject.GetComponent<ItemParent>().GetParent());
        
        OpenCloseDialogsMenu(false);
    }

    public void OpenCloseDialogsMenu(bool isOn)
    {
        _dialogsMenu.SetActive(isOn);
    }

    public void OnWorkGenerator(bool isOn) 
    {
        //try
        //{
        //    generatorWhitenoise.GeneratorOnOff(isOn);
        //}
        //catch 
        //{
        //    vibroGenerator.GeneratorOnOff(isOn);
        //}
        if (isGeneratorSelect)
        {
            generatorWhitenoise.GeneratorOnOff(isOn);
        }
        else if (isVibroGenSelect)
        {
            vibroGenerator.GeneratorOnOff(isOn);
        }
       
    }

    public void ReturnReplaceView()
    {
        //try
        //{
        //    generatorWhitenoise.EnableDisableMoveToGenerator();
        //    Debug.Log("отработал try");

        //}
        //catch 
        //{
        //    vibroGenerator.EnableDisableMoveToGenerator();
        //    Debug.Log("отработал catch");
        //}
        if (isGeneratorSelect)
        {
            generatorWhitenoise.EnableDisableMoveToGenerator();
            isGeneratorSelect= false;
            _horizontalTools.SetActive(true);
        }
        else if(isVibroGenSelect)
        {
            vibroGenerator.EnableDisableMoveToGenerator();
            isVibroGenSelect= false;
            _horizontalTools.SetActive(true);
        }
        
    }
}
