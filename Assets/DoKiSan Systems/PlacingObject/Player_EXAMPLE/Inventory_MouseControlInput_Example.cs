using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_MouseControlInput_Example : MonoBehaviour
{
    private Sugrob_PlacingObject_Inputs _inputs;
    [SerializeField] InventoryReplaceItem _inventory;
    private float stepScroll;
    private float step;

    [SerializeField] DialogsMenuReplace _dialogsMenuReplace;

    private void Awake()
    {
        _inputs = new Sugrob_PlacingObject_Inputs();
    }

    private void OnEnable()
    {
        _inputs.Enable();
        _inputs.Player.DoneClick.performed += DoneClick_performed;
        _inputs.Player.CancelClick.performed += CancelClick_performed;
    }
    private void OnDisable()
    {
        _inputs.Player.DoneClick.performed -= DoneClick_performed;
        _inputs.Player.CancelClick.performed -= CancelClick_performed;
        _inputs.Disable();
    }

    private void CancelClick_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        _inventory.ClearPrefab();
    }

    private void DoneClick_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        _inventory.PlacementPrefab();
        RepeatReplace();
    }
    private void RepeatReplace()
    {
        GameObject hitObject = _inventory.GetHitObject();
        if (hitObject)
        {
            ShowDialogsMenu(hitObject);
        }
    }
    public void ShowDialogsMenu(GameObject hitObject)
    {
        _dialogsMenuReplace.DialogsmenuOpen(hitObject);
    }

    public void ConfirmRepaetReplace(GameObject hitObject)
    {
        ItemsForReplace itemsForReplace = hitObject.GetComponent<ItemParent>().GetParent().GetComponent<ItemsForReplace>();
        if (!itemsForReplace.enabled)
        {
            itemsForReplace.enabled = true;
            itemsForReplace.EnableReplace(_inventory);
        }
    }

    private void Update()
    {
        stepScroll = _inputs.Player.RotationObject.ReadValue<float>();
        _inventory.RotationObject(stepScroll * Time.deltaTime);
    }
}
