using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDataList_SO", menuName = "Inventory/ItemDataList")]
public class ItemDataList_SO : ScriptableObject
{
    [field: SerializeField]
    public List<ItemDetails> ItemDetailsList { get; set; }
}
