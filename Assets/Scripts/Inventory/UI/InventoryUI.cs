using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace Farm.Inventory
{
    public class InventoryUI : MonoBehaviour
    {
        [field: Header("UI of player's bag")]
        [field:SerializeField] private GameObject BagUI { get; set; }
        [field: SerializeField] private SlotUI[] PlayerSlots { get; set; }
        private bool IsBagOpen { get; set; }
        private int SelectedSlotIndex { get; set; } = 0;

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

        /// <summary>
        /// Click event of BagButton
        /// </summary>
        public void ToggleBagUI()
        {
            IsBagOpen = !IsBagOpen;
            BagUI.SetActive(IsBagOpen);
        }

        public void ToggleSlotHighlight(int slotIndex)
        {

            /*if (SelectedSlotIndex == -1)
            {
                PlayerSlots[slotIndex].SlotHighlight.gameObject.SetActive(true);
                SelectedSlotIndex = slotIndex;
            }
            else
            {
                if (SelectedSlotIndex != slotIndex)
                {
                    PlayerSlots[SelectedSlotIndex].SlotHighlight.gameObject.SetActive(false);
                    PlayerSlots[slotIndex].SlotHighlight.gameObject.SetActive(true);
                    SelectedSlotIndex = slotIndex;
                }
                else
                {

                }
            }*/
            PlayerSlots[SelectedSlotIndex].SlotHighlight.gameObject.SetActive(false);
            PlayerSlots[slotIndex].SlotHighlight.gameObject.SetActive(PlayerSlots[slotIndex].IsSelected);
            if (SelectedSlotIndex != slotIndex)
            {
                PlayerSlots[SelectedSlotIndex].IsSelected = false;
            }
            SelectedSlotIndex = slotIndex;
        }
    }
}
