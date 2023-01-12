using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryBag_SO", menuName = "Inventory/InventoryBag_SO")]
public class InventoryBag_SO : ScriptableObject
{
    [field: SerializeField]
    public List<InventoryItem> ItemList { get; set; }
    // may change to dictionary
}
