using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Farm.Inventory
{
    public class InventoryUI : MonoBehaviour
    {
        [field: Header("UI of player's bag")]
        [field:SerializeField] private GameObject BagUI { get; set; }
        [field: SerializeField] private SlotUI[] PlayerSlots { get; set; }
        private bool IsBagOpen { get; set; }

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

            IsBagOpen = BagUI.activeInHierarchy;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                ToggleBagUI();
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

        public void ToggleBagUI()
        {
            IsBagOpen = !IsBagOpen;
            BagUI.SetActive(IsBagOpen);
        }
    }
}
