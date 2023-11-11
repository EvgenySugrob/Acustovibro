using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class InventoryReplaceItem : MonoBehaviour
{
    [Header("Объект установки и настройки руки")]
    [SerializeField] GameObject replacedObjects;
    [SerializeField] float distance;
    [SerializeField] float projectionSpeed;
    [SerializeField] float rotationSpeed;
    [SerializeField] LayerMask layerMask;
    [SerializeField] Camera cameraPlayer;
    [SerializeField] CountPlaceDevices countPlaceDevices;

    private Vector3 objectPosition;
    private Vector3 rotation = new Vector3 (0f, 0f, 0f);
    [SerializeField] GameObject createdObj;
    private bool isActiveReplace;

    private Vector3 currentSurface = new Vector3(0f,0f,0f);
    private GameObject hitObject;

    //==================================
    //Public methods
    //==================================
    public void ActiveSystem(bool isActive)
    {
        isActiveReplace = isActive;
    }
    public bool IsSystemActive()
    {
        return isActiveReplace;
    }
    public void SetReplacedObjects(GameObject objects, GameObject createdObject = null)
    {
        if(createdObj)
            Destroy(createdObj);


        replacedObjects = objects;
        createdObj = createdObject;
        if (replacedObjects.GetComponent<ItemsForReplace>().NullComponentOrNot())
        {
            replacedObjects.GetComponent<ItemsForReplace>().ResetSlot();
        }
    }
    public GameObject GetHitObject()
    {
        if (hitObject)
            if (hitObject.GetComponent<ItemParent>())
            {
                GameObject tmpHitObject = hitObject;
                hitObject = null;
                return tmpHitObject;
            }
        return null;
    }
    public void ClearPrefab()
    {
        if (isActiveReplace)
        {
            if (createdObj.GetComponent<ItemsForReplace>().HaventCurrentTransform())
                Destroy(createdObj);
            else
                createdObj.GetComponent<ItemsForReplace>().ReturnInCurrentTransform();
            replacedObjects = null;
            isActiveReplace = false;
        }
    }

    public void PlacementPrefab()
    {
        if (isActiveReplace)
        {
            ItemsForReplace item = createdObj.GetComponent<ItemsForReplace>();
            string nameCreateObj = item.ReturnNameObject();

            if(item.RaycastHitCheck())
            {
                if (item.Place(objectPosition, createdObj.transform.rotation.eulerAngles, replacedObjects.name))
                {
                    countPlaceDevices.CheckLimmit(nameCreateObj, createdObj);
                    item.ReservSlot(true);
                    item.ControlIsPlace(true);
                    createdObj = null;
                    isActiveReplace = false;
                    currentSurface = Vector3.zero;
                    return;
                }
            }
            
        }

    }

    public void RotationObject(float stepScroll)
    {
        if (isActiveReplace)
        {
            rotation = new Vector3(0f, 0f, stepScroll * rotationSpeed);
        }
    }
    //==================================
    //Private methods
    //==================================

    private void Update()
    {
        Ray ray = cameraPlayer.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (isActiveReplace) 
        {

            if (Physics.Raycast(ray,out RaycastHit hit, distance,layerMask))
            {
                CreateObject(objectPosition);
                AllignToSurface(hit.normal);
                objectPosition = new Vector3(hit.point.x, hit.point.y, hit.point.z);
            }
            else
            {               
                objectPosition = ray.GetPoint(distance);
                CreateObject(objectPosition);
            }
        }
        else
        {
            if (Physics.Raycast(ray, out RaycastHit hit, distance, layerMask))
            {
                hitObject = hit.transform.gameObject;
                //Debug.Log(hitObject.name);
            }
        }
    }

    private void CreateObject(Vector3 position)
    {
        if (replacedObjects != null)
        {
            if (createdObj == null)
            {
                GameObject tempObject = Instantiate(replacedObjects, position, replacedObjects.transform.rotation);
                tempObject.name = replacedObjects.name;
                createdObj = tempObject;
            }
            else
            {
                createdObj.transform.rotation *= Quaternion.Euler(rotation);
                rotation = Vector3.zero;
                createdObj.transform.position = Vector3.Lerp(createdObj.transform.position, position, projectionSpeed * Time.fixedDeltaTime);
            }
        }
    }

    
    private void AllignToSurface(Vector3 surface)
    {
        if (currentSurface != surface)
        {
            createdObj.transform.rotation = Quaternion.LookRotation(surface.normalized);
            currentSurface = surface;
        }
    }
}
