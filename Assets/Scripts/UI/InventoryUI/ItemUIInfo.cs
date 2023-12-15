using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemUIInfo : MonoBehaviour
{
    #region Fields

    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemDescription;

    private int _itemID;

    #endregion

    #region Methods

    public void SetInfos(ItemData itemData)
    {
        image.sprite = itemData.ItemImage;
        itemName.text = itemData.ItemName;
        itemDescription.text = itemData.ItemDescription;
        _itemID = itemData.ItemId;
    }

    public int GetItemID()
    {
        return _itemID;
    }

    #endregion
}
