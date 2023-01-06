using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Farm.Inventory
{
    public class InventoryUI : MonoBehaviour
    {
        [field: SerializeField] private SlotUI[] PlayerSlots { get; set; }

        private void OnEnable()
        {
            EventHandler.UpdateInventoryUI += OnUpdateInventoryUI;
        }

        private void OnDisable()
        {
            EventHandler.UpdateInventoryUI -= OnUpdateInventoryUI;
        }
        private void Start()
        {
            // initialize slot index
            for (int i = 0; i < PlayerSlots.Length; i++)
            {
                PlayerSlots[i].SlotIndex = i;
            }
        }
        private void OnUpdateInventoryUI(InventoryLocation location, List<InventoryItem> items)
        {
            switch (location)
            {
                case InventoryLocation.Player:
                    for (int i = 0; i < PlayerSlots.Length; i++)
                    {
                        if (items[i].ItemAmount > 0)
                        {
                            ItemDetails item = InventoryManager.GetInstance.GetItemDetails(items[i].ItemID);
                            PlayerSlots[i].UpdateSlot(item, items[i].ItemAmount);
                        }
                        else
                        {
                            PlayerSlots[i].UpdateEmptySlot();
                        }
                    }
                    break;
            }
        }
    }
}
