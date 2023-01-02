using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Farm.Inventory
{
    public class InventoryManager : Singleton<InventoryManager>
    {
        [field: SerializeField]
        public ItemDataList_SO ItemDataList_SO { get; set; }
        /// <summary>
        /// Get item details using item ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public ItemDetails GetItemDetails(int ID)
        {
            return ItemDataList_SO.ItemDetailsList.Find(item => item.ItemID == ID);
        }

        /// <summary>
        /// Add the item to player's bag
        /// </summary>
        /// <param name="item"></param>
        /// <param name="toDestory">if need to destroy the item</param>
        public void AddItem(Item item, bool toDestory)
        {
            Debug.Log(item.ItemDetails.ItemID + "Name: " + item.ItemDetails.ItemName);
            if (toDestory)
            {
                Destroy(item.gameObject);
            }
        }
    }
}

