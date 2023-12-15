using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class InventoryHandler : MonoBehaviour
{
    #region Fields
    [SerializeField] private Inventory currentInventory;

    [SerializeField] private XRBaseInteractor rightInteractor;
    [SerializeField] private XRBaseInteractor leftInteractor;

    #endregion

    #region Events

    public Action<ItemData> OnNewItemAddedEvent;
    public Action<ItemData> OnItemRemovedEvent;

    #endregion

    #region Messages

    private void OnEnable()
    {
        rightInteractor.selectEntered.AddListener(OnSelectObject);
        leftInteractor.selectEntered.AddListener(OnSelectObject);
    }

    private void OnDisable()
    {
        rightInteractor.selectEntered.RemoveListener(OnSelectObject);
        leftInteractor.selectEntered.RemoveListener(OnSelectObject);
    }

    private void Awake()
    {
        //currentInventory.SetInventory(new List<ItemData>());
    }

    #endregion

    #region Methods

    public List<ItemData> GetAllItems()
    {
        return currentInventory.GetAllItems();
    }
    
    public void SetCurrentInventory(List<ItemData> newItems)
    {
        currentInventory.SetInventory(newItems);
    }

    private void OnSelectObject(SelectEnterEventArgs args)
    {
        if (args.interactableObject.transform.TryGetComponent(out ItemObject newItem))
        {
            currentInventory.AddItem(newItem);
            OnNewItemAddedEvent?.Invoke(newItem.ItemData);
            newItem.HandleGetItem(args.interactorObject.transform);
        }

        if (args.interactableObject.transform.TryGetComponent(out DropBox newDropBox))
        {
            newDropBox.TryOpenBox(currentInventory);
        }
    }

    #endregion
}
