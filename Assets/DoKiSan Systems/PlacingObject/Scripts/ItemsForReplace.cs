using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemsForReplace : MonoBehaviour
{
    [Header("Настройка поведения объекта")]
    [SerializeField] Transform raycastObj;
    [SerializeField] Material canReplace;
    [SerializeField] Material cantReplace;
    [SerializeField] float rayDistance = 0.1f;
    [SerializeField] bool isGenerator;
    [SerializeField] LayerMask defaultLayer;
    [TagSelector] 
    public string tagFilter = "";

    private bool can;
    [Header("Листы с колайдерами и материалами\nобъекта")]
    [SerializeField,HideInInspector] Collider[] colliders;
    [SerializeField,HideInInspector] MeshFilter[] meshes;
    [SerializeField,HideInInspector] List<Material> materialsList;

    [SerializeField] PointerEventData.InputButton _button;
    public SonataSlotsCheck sonataSlotsCheck;
    public SonataSlot slot;
    bool isPlace = false;
    private Vector3? _currentPosition = null;
    private Quaternion? _currentRotation = null;
    private bool isCollision = false;
    RaycastHit hit;

    //--------------------->Private

    private void OnEnable()
    {
        gameObject.layer = 2;
        colliders = GetComponentsInChildren<Collider>();
        meshes = GetComponentsInChildren<MeshFilter>();
        RigidbodyComponentWork(true);
        for (int i = 0; i < meshes.Length; i++)
        {
            materialsList.Add(meshes[i].GetComponent<Renderer>().material);
        }
        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].enabled = false;
        }
        GetComponent<BoxCollider>().enabled = true;
    }
    private void OnDisable()
    {
        _currentPosition = transform.position;
        _currentRotation = transform.rotation;
    }
    private void Update()
    {

        if (Physics.Raycast(raycastObj.position, raycastObj.forward,out hit, rayDistance) && !isCollision)
        {
            if (hit.transform.tag != tagFilter )
            {
                can = false;
            }
            else
            {
                can = true;
            }
        }
        else
        {
            can = false;
        }
        for (int i = 0; i < meshes.Length; i++)
        {
            meshes[i].GetComponent<Renderer>().material = can ? canReplace : cantReplace;
        }
    }
    private void ReturnToNormal()
    {
        for (int i = 0; i < meshes.Length; i++)
        {
            meshes[i].GetComponent<Renderer>().material = materialsList[i];
        }
        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].enabled = true;
        }
        gameObject.layer = defaultLayer;
        GetComponent<BoxCollider>().enabled = false;
        RigidbodyComponentWork(false);
        enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        isCollision = true;
    }
    private void OnTriggerStay(Collider other)
    {
        isCollision = true;
    }
    private void OnTriggerExit(Collider other)
    {
        isCollision = false;
    }
    private void RigidbodyComponentWork(bool isCreate)
    { 
        if (isCreate)
        {
            Rigidbody rigidbody = gameObject.AddComponent<Rigidbody>();
            rigidbody.isKinematic = true;
            rigidbody.useGravity = false;
        }
        else
            Destroy(GetComponent<Rigidbody>());
    }

    //---------------->Public 
    public bool RaycastHitCheck()
    {
        bool resultCheck = false;
        if (hit.transform.GetComponent<SonataSlot>())
        {
            if (hit.transform.GetComponent<SonataSlot>().CheckCanPlaceDevisec())
            {
                slot = hit.transform.GetComponent<SonataSlot>();
                sonataSlotsCheck = hit.transform.GetComponent<SonataSlot>().SlotReturn();
                resultCheck = true;
            }
        }
        else if (hit.transform.GetComponent<SonataSlotsCheck>())
        {
            SonataSlotsCheck slotsCheck = hit.transform.GetComponent<SonataSlotsCheck>();
            if (slotsCheck.IsSlotFree())
            {
                sonataSlotsCheck = slotsCheck;
                resultCheck = true;
            }
        }
        else
        {
            resultCheck = false;
        }
        return resultCheck;

    }
    public void ReservSlot(bool isReserv)
    {
        if (hit.transform.GetComponent<SonataSlot>()) 
        {
            hit.transform.GetComponent<SonataSlot>().ReservSlot(isReserv);
            hit.transform.GetComponent<SonataSlot>().AddSlotObject(transform.gameObject);
        }
        else if (hit.transform.GetComponent<SonataSlotsCheck>())
        {
            hit.transform.GetComponent<SonataSlotsCheck>().SlotReserv(transform.gameObject);
        }
        
    }

    public bool Place(Vector3 pos, Vector3 local, string name)
    {
        if (hit.transform.GetComponent<SonataSlot>())
        {
            GameObject slot = hit.transform.GetComponent<SonataSlot>().gameObject;

            if (slot.GetComponent<SonataSlot>().IsSlotLocation())
            {
                //transform.SetParent(slot.transform);
                transform.position = slot.transform.position;
                transform.localEulerAngles = slot.transform.localEulerAngles;
            }
            else
            {
                //transform.SetParent(slot.transform);
                transform.position = slot.transform.position;
                transform.localEulerAngles = local;
            }
        }
        else
        {
            transform.position = pos;
            transform.localEulerAngles = local;
        }
        
        transform.name = name;

        if (can)
        {
            ReturnToNormal();
        }
        else
        {
            return can;
        }
        return can;
    }

    public void EnableReplace(InventoryReplaceItem inventoryReplaceItem)
    {
        if (isPlace)
        {
            isPlace = false;
            transform.parent = null;
            inventoryReplaceItem.ActiveSystem(true);
            inventoryReplaceItem.SetReplacedObjects(gameObject, gameObject);
        }
    }
    public void ReturnInCurrentTransform()
    {
        transform.position = (Vector3)_currentPosition;
        transform.rotation = (Quaternion)_currentRotation;
        isPlace = true;
        ReturnToNormal();
    }
    public bool HaventCurrentTransform()
    {
        return (_currentPosition == null || _currentRotation == null);
    }
    public void ControlIsPlace(bool state)
    {
        isPlace = state;
    }

    public string ReturnNameObject()
    {
        return transform.name;
    }

    public bool NullComponentOrNot()
    {
        if (sonataSlotsCheck == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void ResetSlot()
    {
        Debug.Log(hit.transform.name);
        if (hit.transform.GetComponent<SonataSlot>())
        {
            sonataSlotsCheck.ReplaceObjectSlot(transform.gameObject);
        }
        else if(hit.transform.GetComponent<SonataSlotsCheck>())
        {
            sonataSlotsCheck.ClearSingleSlot();
        }
    }

    public bool GetGenerator()
    {
        return isGenerator;
    }
}
