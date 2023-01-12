using UnityEngine;

[System.Serializable]
public class ItemDetails
{
    [field: SerializeField]
    public int ItemID { get; set; }
    [field: SerializeField]
    public string ItemName { get; set; }
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
    public bool CanPicked { get; set; }
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

[System.Serializable]
public struct InventoryItem
{
    [field: SerializeField]
    public int ItemID { get; set; }
    [field: SerializeField]
    public int ItemAmount { get; set; }
}

[System.Serializable]
public class AnimatorType
{
    [field: SerializeField] public CharacterPart CharacterPart { get; set; }
    [field: SerializeField] public ActionType ActionType { get; set; }
    [field: SerializeField] public AnimatorOverrideController OverrideController { get; set; }
}