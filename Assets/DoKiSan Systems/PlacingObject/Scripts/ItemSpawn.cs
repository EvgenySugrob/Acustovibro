using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSpawn : MonoBehaviour
{
    [SerializeField] GameObject _item;
    [SerializeField] InventoryReplaceItem _inventoryReplaceItem;

    public void EnablePlaceObject()
    {
        //if (_inventoryReplaceItem.IsSystemActive())
        //{
            _inventoryReplaceItem.ActiveSystem(true);
            _inventoryReplaceItem.SetReplacedObjects(_item);
        //}
    }

}
