using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerHandler : MonoBehaviour
{
    #region Fields

    [SerializeField] private InventoryHandler _inventoryHandler;
    [SerializeField] private XROrigin _origin;
    [SerializeField] private GameObject _UIInventoryObject;
    [SerializeField] private Inventory _startInventory;
    [SerializeField] private InputActionReference _inventoryAction;

    #endregion

    #region Messages

    void Start()
    {
        SubscribeToEvents();
    }

    private void OnDestroy()
    {
        UnsubscribeToEvents();
    }

    #endregion

    #region Methods

    private void OnNewItemAdded(ItemData itemData)
    {
        CheckpointManager.Instance.SaveNewCheckpoint(Camera.main.transform, _inventoryHandler.GetAllItems());

        ModalTextsHandler.RequestModalText(itemData.ItemName + " added to inventory");
        ModalTextsHandler.RequestModalText("Checkpoint created");
        
        if(!_UIInventoryObject.activeSelf)
            _UIInventoryObject.SetActive(true);
    }

    private void OnItemRemoved(ItemData itemData)
    {
        ModalTextsHandler.RequestModalText(itemData.ItemName + " was removed from inventory");
    }

    private void OpenInventoryInput(InputAction.CallbackContext context)
    {
        _UIInventoryObject.SetActive(!_UIInventoryObject.activeSelf);
    }

    private void SetNewInventory(List<ItemData> newItems)
    {
        _inventoryHandler.SetCurrentInventory(newItems);
    }

    private void MovePlayer(Vector3 position)
    {
        _origin.MoveCameraToWorldLocation(position);
        ModalTextsHandler.RequestModalText("Last checkpoint loaded");
    }

    public void RestartGame()
    {
        SetNewInventory(_startInventory.GetAllItems());
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    private void SubscribeToEvents()
    {
        _inventoryHandler.OnNewItemAddedEvent += OnNewItemAdded;
        _inventoryHandler.OnItemRemovedEvent += OnItemRemoved;
        CheckpointManager.OnRequestPlayerPositionEvent += MovePlayer;
        CheckpointManager.OnRequestInventorySetEvent += SetNewInventory;
        
        if(_inventoryAction)
            _inventoryAction.action.performed += OpenInventoryInput;
    }

    private void UnsubscribeToEvents()
    {
        _inventoryHandler.OnNewItemAddedEvent -= OnNewItemAdded;
        _inventoryHandler.OnItemRemovedEvent -= OnItemRemoved;
        CheckpointManager.OnRequestPlayerPositionEvent -= MovePlayer;
        CheckpointManager.OnRequestInventorySetEvent -= SetNewInventory;
        
        if(_inventoryAction)
            _inventoryAction.action.performed -= OpenInventoryInput;
    }

    #endregion
}
