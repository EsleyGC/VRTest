using UnityEngine;

[System.Serializable]
public struct ItemData
{
    public int ItemId;
    public string ItemName;
    public string ItemDescription;
    public Sprite ItemImage;
    public string TooltipText;
    public ItemType ItemType;
}
