using UnityEngine;

[System.Serializable]
public class ItemDetails
{
    [field: SerializeField]
    public int ItemID { get; set; }
    [field: SerializeField]
    public string Name { get; set; }
    [field: SerializeField]
    public ItemType ItemType { get; set; }
    [field: SerializeField]
    public Sprite ItemIcon { get; set; }
    [field: SerializeField]
    public Sprite ItemOnWorldSprite { get; set; }
    [field: SerializeField]
    public string ItemDescription { get; set; }
    [field: SerializeField]
    public int ItemUseRadius { get; set; }
    [field: SerializeField]
    public bool CanPickedup { get; set; }
    [field: SerializeField]
    public bool CanDropped { get; set; }
    [field: SerializeField]
    public bool CanCarried { get; set; }
    [field: SerializeField]
    public int ItemPrice { get; set; }
    [field: Range(0f, 1f)]
    [field: SerializeField]
    public float SellPercentage { get; set; }
}