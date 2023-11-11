using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

[Serializable]
class HierarchyList
{
    public string globalType;
    public List<string> localTypes;
}
public class HierarchyInitial : MonoBehaviour
{
    [SerializeField] List<ObjectInCatalog> allObjectInCatalog; //Сюда заносим все объекты, которые подлежат каталогизации
    [SerializeField] int startPrefabNumber; //Здесь указываем стартовый префаб, который будет открываться при первом запуске каталога

    [SerializeField] List<HierarchyList> hierarchy; //Сюда заносим полную иерархию каталога (названия групп и подгрупп) 
    [SerializeField] LocalType prefabLocalType; //Префаб подгруппы (не трогаем)
    [SerializeField] ObjectButton prefabObjectButton; //Префаб кнопки, вызывающей объект каталога (не трогаем)
    [SerializeField] TMP_Dropdown globalTypeSelector; //Выпадающий список, который содержит главные типы на сцене (не трогаем)
    [SerializeField] GameObject globalFieldPrefab; //Префаб поля, в которое будут заноситься подгруппы (не трогаем)
    [SerializeField] Transform contentHierarchy; //Поле контента слайдера (не трогаем)
    [SerializeField] RectTransform root; //То же самое поле, но RectTransform, используется для обновления LayoutGroup (не трогаем)
    [SerializeField] Transform searchPanel; //Панель, в которой будет работать посиковая строка (не трогаем)
    [SerializeField] GameObject searchPanelRoot;

    private List<GameObject> globalFields = new List<GameObject>(); //Список главных групп (не трогаем)
    private int selectedIndex = 0; //Индекс выбранной главной группы (не трогаем)


    private void OnEnable()
    {
        Initial();
    }

    private void Initial()
    {
        ClearItemList();
        foreach (HierarchyList typeGlobal in hierarchy)
        {
            GameObject newGlobal = Instantiate(globalFieldPrefab, contentHierarchy); //Создание главных разделов каталога
            globalFields.Add(newGlobal);
            CreateNewItemList(typeGlobal.globalType);
            newGlobal.SetActive(true);
            foreach (string typeLocal in typeGlobal.localTypes)
            {
                GameObject newLocal = Instantiate(prefabLocalType.transform.parent.gameObject, newGlobal.transform); //Создание подкаталогов
                newLocal.SetActive(true);
                newLocal.transform.GetChild(0).GetComponent<LocalType>().SetType(typeLocal);
                newLocal.transform.GetChild(0).GetComponent<LocalType>().fieldForObjectButton.gameObject.SetActive(true);
                foreach (ObjectInCatalog objectInCatalog in allObjectInCatalog)
                {
                    foreach (string objectType in objectInCatalog.types)
                    {
                        if (objectType == typeLocal)
                        {
                            GameObject newButtonObject = Instantiate(prefabObjectButton.gameObject, newLocal.transform.GetChild(0).GetComponent<LocalType>().fieldForObjectButton); //Создание кнопок вызова объектов
                            SetButtonSettings(newButtonObject, objectInCatalog);
                            if (allObjectInCatalog.IndexOf(objectInCatalog) == selectedIndex)
                                newButtonObject.GetComponent<ObjectButton>().SelectObject();
                            break;
                        }
                    }
                }
                LayoutRebuilder.ForceRebuildLayoutImmediate(root);
                LayoutRebuilder.ForceRebuildLayoutImmediate(root);
                newLocal.transform.GetChild(0).GetComponent<LocalType>().fieldForObjectButton.gameObject.SetActive(false);
            }
            if (globalFields.Count > 1)
                newGlobal.SetActive(false);
        }
        foreach (ObjectInCatalog objectInCatalog in allObjectInCatalog)
        {
            GameObject newButtonObject = Instantiate(prefabObjectButton.gameObject, searchPanel);
            SetButtonSettings(newButtonObject, objectInCatalog);

            string buttonTag = objectInCatalog.nameObject.ToLowerInvariant();
            foreach (string type in objectInCatalog.types)
            {
                buttonTag += " " + type.ToLowerInvariant();
            }
            newButtonObject.GetComponent<ObjectButton>().SetTags(buttonTag);
            searchPanelRoot.SetActive(false);
        }
    }
    private void SetButtonSettings(GameObject newButtonObject, ObjectInCatalog objectInCatalog)
    {
        newButtonObject.GetComponent<ObjectButton>().objectInCatalog = objectInCatalog;
        newButtonObject.GetComponent<ObjectButton>().SetNameButton(objectInCatalog.nameObject);
        newButtonObject.SetActive(true);
    }
    private void ClearItemList()
    {
        globalTypeSelector.options.Clear();
    }
    private void CreateNewItemList(string typeName)
    {
        List<string> newOptions = new List<string> { typeName };
        globalTypeSelector.AddOptions(newOptions);
    }
    public void SelectGlobalType(int indexType)
    {
        globalFields[selectedIndex].SetActive(false);
        globalFields[indexType].SetActive(true);
        selectedIndex = indexType;
    }
    public void ExitProgram()
    {
        Application.Quit();
    }
}
