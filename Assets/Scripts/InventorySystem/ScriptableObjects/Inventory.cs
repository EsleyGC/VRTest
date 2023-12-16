using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory", menuName = "Inventory/Inventory", order = 0)]
public class Inventory : ScriptableObject
{
    #region Variables

    [SerializeField] private List<ItemData> _items = new List<ItemData>();

    #endregion

    #region Events

    public event Action<ItemData> OnItemAddedEvent;
    public event Action<ItemData> OnItemRemovedEvent;

    #endregion

    #region Methods

    public bool HasItem(int itemId)
    {
        return _items.Any(item => item.ItemId == itemId);
    }

    public void AddItem(ItemObject itemObject)
    {
        _items.Add(itemObject.ItemData);
        OnItemAddedEvent?.Invoke(itemObject.ItemData);
    }

    public void RemoveItem(ItemObject itemObject)
    {
        if (!_items.Contains(itemObject.ItemData))
            return;
        
        _items.Remove(itemObject.ItemData);
        OnItemRemovedEvent?.Invoke(itemObject.ItemData);
    }

    public void RemoveItem(int itemId)
    {
        var itemToRemove = _items.Find(item => item.ItemId == itemId);

        if (!_items.Contains(itemToRemove))
            return;
        
        _items.Remove(itemToRemove);
        OnItemRemovedEvent?.Invoke(itemToRemove);
    }

    public void SetInventory(List<ItemData> newItems)
    {
        _items = new List<ItemData>(newItems);
    }

    public List<ItemData> GetAllItems()
    {
        return _items;
    }

    #endregion
}