using UnityEngine;

[System.Serializable]
public class ItemDetails
{
    public int ItemID { get; set; }
    public string name;
    public ItemType itemType;
    public Sprite itemIcon;
    public Sprite itemOnWorldSprite;
    public string itemDescription;
    public int itemUseRadius;
    public bool canPickedup;
    public bool canDropped;
    public bool canCarried;
    public int itemPrice;
    [Range(0f, 1f)]
    public float sellPercentage;
}