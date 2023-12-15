using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIHandler : MonoBehaviour
{
    #region Fields

    [SerializeField] private InventoryHandler _inventoryHandler;
    [SerializeField] private ItemUIInfo _itemUIInfoPrefab;
    
    [SerializeField] private GameObject _weaponSection;
    [SerializeField] private GameObject _pointsSection;
    [SerializeField] private GameObject _InstrumentsSection;
    
    [SerializeField] private Image _weaponSelectionImage;
    [SerializeField] private Image _pointsSelectionImage;
    [SerializeField] private Image _InstrumentsSelectionImage;

    [SerializeField] private List<ItemData> _itemDatas = new List<ItemData>();

    [SerializeField] private List<ItemUIInfo> _currentSpawnedUiItemDatas = new List<ItemUIInfo>();

    #endregion

    #region Messages

    private void Awake()
    {
        _inventoryHandler.OnNewItemAddedEvent += OnInventoryUpdates;
        _inventoryHandler.OnItemRemovedEvent += OnInventoryUpdates;
    }

    private void OnEnable()
    {
        UpdateInventoryUI();
    }

    private void Start()
    {
        PopulateInventoryUI(_inventoryHandler.GetAllItems());
    }

    private void OnDestroy()
    {
        _inventoryHandler.OnNewItemAddedEvent -= OnInventoryUpdates;
        _inventoryHandler.OnItemRemovedEvent -= OnInventoryUpdates;
    }

    #endregion

    #region Methods

    #region SectionsSelection

    public void SelectWeaponSection()
    {
        SelectSection(_weaponSection);
    }

    public void SelectPointsSection()
    {
        SelectSection(_pointsSection);
    }

    public void SelectInstrumentsSection()
    {
        SelectSection(_InstrumentsSection);
    }

    private void SelectSection(GameObject sectionToSelect)
    {
        SetSectionActive(_weaponSection, _weaponSelectionImage, sectionToSelect == _weaponSection);
        SetSectionActive(_pointsSection, _pointsSelectionImage, sectionToSelect == _pointsSection);
        SetSectionActive(_InstrumentsSection, _InstrumentsSelectionImage, sectionToSelect == _InstrumentsSection);
    }

    private void SetSectionActive(GameObject section, Image selectionImage, bool isActive)
    {
        if (section.activeSelf == isActive)
            return;
        
        selectionImage.enabled = !isActive;
        section.SetActive(isActive);
    }

    #endregion

    #region ItemUI

    private void PopulateInventoryUI(List<ItemData> itemDatas)
    {
        foreach (var itemData in itemDatas)
        {
            if(itemDatas.Contains(itemData))
                continue;
            
            AddItemUIInfo(itemData);
            _itemDatas.Add(itemData);
        }
    }

    private void OnInventoryUpdates(ItemData arg)
    {
        UpdateInventoryUI();
    }

    private void UpdateInventoryUI()
    {
        List<ItemData> currentInventoryItems = _inventoryHandler.GetAllItems();
        
        List<ItemData> itemsToAdd = currentInventoryItems.Except(_itemDatas).ToList();
        foreach (var itemToAdd in itemsToAdd)
        {
            AddItemUIInfo(itemToAdd);
            _itemDatas.Add(itemToAdd);
        }
        
        List<ItemData> itemsToRemove = _itemDatas.Except(currentInventoryItems).ToList();
        foreach (var itemToRemove in itemsToRemove)
        {
            RemoveItemUIInfo(itemToRemove);
            _itemDatas.Remove(itemToRemove);
        }
    }

    private void AddItemUIInfo(ItemData itemData)
    {
        var parentTransform = itemData.ItemType switch
        {
            ItemType.Weapon => _weaponSection.transform,
            ItemType.Instrument => _InstrumentsSection.transform,
            _ => _weaponSection.transform
        };
        
        var newItemUI = Instantiate(_itemUIInfoPrefab, parentTransform);
        newItemUI.SetInfos(itemData);
        _currentSpawnedUiItemDatas.Add(newItemUI);
    }

    private void RemoveItemUIInfo(ItemData itemData)
    {
        var itemToRemove = _currentSpawnedUiItemDatas.Find(item => item.GetItemID() == itemData.ItemId);
        if (itemToRemove == null)
            return;

        _currentSpawnedUiItemDatas.Remove(itemToRemove);
        Destroy(itemToRemove.gameObject);
    }

    #endregion

    #endregion
}
