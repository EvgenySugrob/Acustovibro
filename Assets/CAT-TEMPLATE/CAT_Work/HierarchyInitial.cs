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
    [SerializeField] List<ObjectInCatalog> allObjectInCatalog; //���� ������� ��� �������, ������� �������� �������������
    [SerializeField] int startPrefabNumber; //����� ��������� ��������� ������, ������� ����� ����������� ��� ������ ������� ��������

    [SerializeField] List<HierarchyList> hierarchy; //���� ������� ������ �������� �������� (�������� ����� � ��������) 
    [SerializeField] LocalType prefabLocalType; //������ ��������� (�� �������)
    [SerializeField] ObjectButton prefabObjectButton; //������ ������, ���������� ������ �������� (�� �������)
    [SerializeField] TMP_Dropdown globalTypeSelector; //���������� ������, ������� �������� ������� ���� �� ����� (�� �������)
    [SerializeField] GameObject globalFieldPrefab; //������ ����, � ������� ����� ���������� ��������� (�� �������)
    [SerializeField] Transform contentHierarchy; //���� �������� �������� (�� �������)
    [SerializeField] RectTransform root; //�� �� ����� ����, �� RectTransform, ������������ ��� ���������� LayoutGroup (�� �������)
    [SerializeField] Transform searchPanel; //������, � ������� ����� �������� ��������� ������ (�� �������)
    [SerializeField] GameObject searchPanelRoot;

    private List<GameObject> globalFields = new List<GameObject>(); //������ ������� ����� (�� �������)
    private int selectedIndex = 0; //������ ��������� ������� ������ (�� �������)


    private void OnEnable()
    {
        Initial();
    }

    private void Initial()
    {
        ClearItemList();
        foreach (HierarchyList typeGlobal in hierarchy)
        {
            GameObject newGlobal = Instantiate(globalFieldPrefab, contentHierarchy); //�������� ������� �������� ��������
            globalFields.Add(newGlobal);
            CreateNewItemList(typeGlobal.globalType);
            newGlobal.SetActive(true);
            foreach (string typeLocal in typeGlobal.localTypes)
            {
                GameObject newLocal = Instantiate(prefabLocalType.transform.parent.gameObject, newGlobal.transform); //�������� ������������
                newLocal.SetActive(true);
                newLocal.transform.GetChild(0).GetComponent<LocalType>().SetType(typeLocal);
                newLocal.transform.GetChild(0).GetComponent<LocalType>().fieldForObjectButton.gameObject.SetActive(true);
                foreach (ObjectInCatalog objectInCatalog in allObjectInCatalog)
                {
                    foreach (string objectType in objectInCatalog.types)
                    {
                        if (objectType == typeLocal)
                        {
                            GameObject newButtonObject = Instantiate(prefabObjectButton.gameObject, newLocal.transform.GetChild(0).GetComponent<LocalType>().fieldForObjectButton); //�������� ������ ������ ��������
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
