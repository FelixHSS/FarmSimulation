using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
namespace Farm.Inventory
{
    public class SlotUI : MonoBehaviour, IPointerClickHandler
    {
        [field: Header("Retrive Components")]
        [field: SerializeField] private Image SlotImage { get; set; }
        [field: SerializeField] private TextMeshProUGUI AmountText { get; set; }
        [field: SerializeField] public Image SlotHighlight { get; private set; }
        [field: SerializeField] private Button Button { get; set; }
        [field: Header("Slot Type")]
        [field: SerializeField] public SlotType SlotType { get; set; }
        [field: Header("Item Info")]
        [field: SerializeField] internal bool IsSelected { get; set; }
        [field: SerializeField] private ItemDetails ItemDetails { get; set; }
        [field: SerializeField] private int ItemAmount { get; set; }
        [field: SerializeField] public int SlotIndex { get; set; }
        
        private InventoryUI InventoryUI => GetComponentInParent<InventoryUI>();
        

        private void Start()
        {
            IsSelected = false;

            if (ItemDetails.ItemID == 0)
            {
                UpdateEmptySlot();
            }

        }

        /// <summary>
        /// Update the slot and item info
        /// </summary>
        /// <param name="item">item details</param>
        /// <param name="amount">quantity held</param>
        public void UpdateSlot(ItemDetails item, int amount)
        {
            ItemDetails = item;
            SlotImage.sprite = item.ItemIcon;
            ItemAmount = amount;
            SlotImage.enabled = true;
            AmountText.text = amount.ToString();
            Button.interactable = true;
        }

        /// <summary>
        /// create empty slot
        /// </summary>
        public void UpdateEmptySlot()
        {
            if (IsSelected)
            {
                IsSelected = false;
            }

            SlotImage.enabled = false;
            AmountText.text = string.Empty;
            Button.interactable = false;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (ItemAmount == 0) return;
            IsSelected = !IsSelected;

            InventoryUI.ToggleSlotHighlight(SlotIndex);
        }
    }
}
