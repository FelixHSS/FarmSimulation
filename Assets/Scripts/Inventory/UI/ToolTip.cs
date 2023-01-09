using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToolTip : MonoBehaviour
{
    // Start is called before the first frame update
    [field: SerializeField] private TextMeshProUGUI NameText;
    [field: SerializeField] private TextMeshProUGUI TypeText;
    [field: SerializeField] private TextMeshProUGUI DescriptionText;
    [field: SerializeField] private Text PriceText;
    [field: SerializeField] private GameObject BottomPart;

    public void SetUpTooltip(ItemDetails itemDetails, SlotType slotType)
    {
        NameText.text = itemDetails.ItemName;
        TypeText.text = GetItemType(itemDetails.ItemType);
        DescriptionText.text = itemDetails.ItemDescription;

        if (itemDetails.ItemType is ItemType.Seed or ItemType.Commodity or ItemType.Furniture)
        {
            BottomPart.SetActive(true);

            var price = itemDetails.ItemPrice;
            if (slotType == SlotType.Bag)
            {
                price = (int)(price * itemDetails.SellPercentage);
            }

            PriceText.text = price.ToString();
        }
        else
        {
            BottomPart.SetActive(false);
        }

        LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
    }

    private string GetItemType(ItemType itemtype)
    {
        return itemtype switch
        {
            ItemType.Seed => "Seed",
            ItemType.Commodity => "Commodity",
            ItemType.Furniture => "Furniture",
            ItemType.HoeTool => "Tool",
            ItemType.ChopTool => "Tool",
            ItemType.BreakTool => "Tool",
            ItemType.ReapTool => "Tool",
            ItemType.WaterTool => "Tool",
            ItemType.CollectTool => "Tool",
            ItemType.ReapableScenery => "Tool",
            _ => "others",
        };
    }
}
