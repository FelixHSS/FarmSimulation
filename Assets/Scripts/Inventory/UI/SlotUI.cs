using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
namespace Farm.Inventory
{
    public class SlotUI : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [field: Header("Retrive Components")]
        [field: SerializeField] private Image ItemImage { get; set; }
        [field: SerializeField] private TextMeshProUGUI AmountText { get; set; }
        [field: SerializeField] public Image SlotHighlight { get; private set; }
        [field: SerializeField] private Button Button { get; set; }
        [field: Header("Slot Type")]
        [field: SerializeField] public SlotType SlotType { get; set; }
        [field: Header("Item Info")]
        [field: SerializeField] internal bool IsSelected { get; set; }
        [field: SerializeField] internal ItemDetails ItemDetails { get; private set; }
        [field: SerializeField] internal int ItemAmount { get; private set; }
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
        /// <param name="itemDetails">item details</param>
        /// <param name="amount">quantity held</param>
        public void UpdateSlot(ItemDetails itemDetails, int amount)
        {
            ItemDetails = itemDetails;
            ItemImage.sprite = itemDetails.ItemIcon;
            ItemAmount = amount;
            ItemImage.enabled = true;
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

            ItemImage.enabled = false;
            AmountText.text = string.Empty;
            Button.interactable = false;
            ItemAmount = 0;
            ItemDetails = new();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (ItemAmount == 0) return;
            IsSelected = !IsSelected;
            
            InventoryUI.ToggleSlotHighlight(SlotIndex);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (ItemAmount > 0) // && ItemDetials.ItemID = 0
            {
                InventoryUI.draggedItem.enabled = true;
                InventoryUI.draggedItem.sprite = ItemImage.sprite;
                InventoryUI.draggedItem.SetNativeSize();

                IsSelected = true;
                InventoryUI.ToggleSlotHighlight(SlotIndex);
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            InventoryUI.draggedItem.transform.position = Input.mousePosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            InventoryUI.draggedItem.enabled = false;

            if (eventData.pointerCurrentRaycast.gameObject != null)
            {
                // types of invalid drag
                if (!eventData.pointerCurrentRaycast.gameObject.TryGetComponent<SlotUI>(out var targetSlot))
                    return;

                int targetIndex = targetSlot.SlotIndex;

                if (targetIndex == SlotIndex) return;
                if (ItemDetails.ItemID == 0) return;

                // Swap items in player's bag (backpack and action bar)
                if (SlotType == SlotType.Bag && targetSlot.SlotType == SlotType.Bag)
                {
                    InventoryManager.GetInstance.SwapItemsInPlayerBag(SlotIndex, targetIndex);
                }

                //update slot highlight
                InventoryUI.ToggleSlotHighlight(SlotIndex);
                targetSlot.IsSelected = true;
                InventoryUI.ToggleSlotHighlight(targetIndex);
                
            }
            else // Drop items on the ground
            {
                if (ItemDetails.CanDropped)
                {

                }
            }
        }

        
    }
}
