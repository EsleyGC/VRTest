using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerHandler : MonoBehaviour
{
    #region Fields

    [SerializeField] private InventoryHandler _inventoryHandler;
    [SerializeField] private XROrigin _origin;
    [SerializeField] private TeleportationProvider _teleportationProvider;
    [SerializeField] private GameObject _UIInventoryObject;

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
        
        if(!_UIInventoryObject.activeSelf)
            _UIInventoryObject.SetActive(true);
    }

    private void SetNewInventory(List<ItemData> newItems)
    {
        _inventoryHandler.SetCurrentInventory(newItems);
    }

    private void MovePlayer(Vector3 position)
    {
        _origin.MoveCameraToWorldLocation(position);
    }
    
    private void SubscribeToEvents()
    {
        _inventoryHandler.OnNewItemAddedEvent += OnNewItemAdded;
        CheckpointManager.OnRequestPlayerPositionEvent += MovePlayer;
        CheckpointManager.OnRequestInventorySetEvent += SetNewInventory;
    }

    private void UnsubscribeToEvents()
    {
        _inventoryHandler.OnNewItemAddedEvent -= OnNewItemAdded;
        CheckpointManager.OnRequestPlayerPositionEvent -= MovePlayer;
        CheckpointManager.OnRequestInventorySetEvent -= SetNewInventory;
    }

    #endregion
}
