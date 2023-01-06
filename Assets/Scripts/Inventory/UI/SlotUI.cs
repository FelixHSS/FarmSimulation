using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotUI : MonoBehaviour
{
    [field: Header("retrive components")]
    [field: SerializeField] private Image SlotImage { get; set; }
    [field: SerializeField] private TextMeshProUGUI AmountText { get; set; }
    [field: SerializeField] private Image SlotHighlight { get; set; }
    [field: SerializeField] private Button Button { get; set; }
    [field: Header("Slot Type")]
    [field: SerializeField] public SlotType SlotType { get; set; }
    [field: Header("Item Info")]
    [field: SerializeField] public bool IsSelected { get; set; }
    [field: SerializeField] public ItemDetails ItemDetails { get; set; }
    [field:SerializeField] public int ItemAmount { get; set; }

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
}
