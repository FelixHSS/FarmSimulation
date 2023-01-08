using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Farm.Inventory
{
    public class InventoryManager : Singleton<InventoryManager>
    {
        [field: Header("Item Data")]
        [field: SerializeField]
        public ItemDataList_SO ItemDataList_SO { get; set; }
        [field: Header("Player Bag Data")]
        [field: SerializeField]
        public InventoryBag_SO PlayerBag { get; set; }

        private void Start()
        {
            EventHandler.CallUpdateInventoryUI(InventoryLocation.Player, PlayerBag.ItemList);
        }

        /// <summary>
        /// Get item details using item ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public ItemDetails GetItemDetails(int ID)
        {
            return ItemDataList_SO.ItemDetailsList.Find(itemDetails => itemDetails.ItemID == ID);
        }

        /// <summary>
        /// Add the item to player's bag
        /// </summary>
        /// <param name="item"></param>
        /// <param name="toDestory">if need to destroy the item</param>
        public void AddItem(Item item, bool toDestory)
        {
            AddItem(item.ItemID, 1);

            Debug.Log(item.ItemDetails.ItemID + "Name: " + item.ItemDetails.ItemName);
            if (toDestory)
            {
                Destroy(item.gameObject);
            }

            // update UI
            EventHandler.CallUpdateInventoryUI(InventoryLocation.Player, PlayerBag.ItemList);
        }

        /// <summary>
        /// Check if player's bag has an empoty slot
        /// </summary>
        /// <returns></returns>
        private bool CheckBagCapacity()
        {
            for (int i = 0; i < PlayerBag.ItemList.Count; i++)
            {
                if (PlayerBag.ItemList[i].ItemID == 0)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Find the position of the item in the bag
        /// </summary>
        /// <param name="ID"></param>
        /// <returns>-1 means the item doesn't exist in the bag</returns>
        private int GetItemIndexInBag(int ID)
        {
            for (int i = 0; i < PlayerBag.ItemList.Count; i++)
            {
                if (PlayerBag.ItemList[i].ItemID == ID) 
                    return i;
            }
            return -1;
        }

        /// <summary>
        /// Add a specific amount of the item to player's bag
        /// </summary>
        /// <param name="ID">the ID of the item that need to be added in player's bag</param>
        /// <param name="amount"></param>
        private void AddItem(int ID, int amount)
        {
            int itemPosition = GetItemIndexInBag(ID);

            if (itemPosition == -1 && CheckBagCapacity()) // the item is not in player's bag and the bag has an empty slot
            {
                InventoryItem item = new() { ItemID = ID, ItemAmount = amount };
                for (int i = 0; i < PlayerBag.ItemList.Count; i++)
                {
                    if (PlayerBag.ItemList[i].ItemID == 0)
                    {
                        PlayerBag.ItemList[i] = item;
                        break;
                    }
                }
            }
            else
            {
                InventoryItem item = PlayerBag.ItemList[itemPosition];
                item.ItemAmount += amount;
                PlayerBag.ItemList[itemPosition] = item;
            }
        }

        /// <summary>
        /// In the range of player's bag
        /// </summary>
        /// <param name="fromIndex"></param>
        /// <param name="toIndex"></param>
        public void SwapItemsInPlayerBag(int fromIndex, int toIndex)
        {
            InventoryItem currentItem = PlayerBag.ItemList[fromIndex];
            InventoryItem targetItem = PlayerBag.ItemList[toIndex];

            PlayerBag.ItemList[fromIndex] = targetItem;
            PlayerBag.ItemList[toIndex] = currentItem;

            EventHandler.CallUpdateInventoryUI(InventoryLocation.Player, PlayerBag.ItemList);
        }
    }
}

