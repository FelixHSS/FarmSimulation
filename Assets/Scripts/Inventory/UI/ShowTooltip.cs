using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Farm.Inventory
{
    [RequireComponent(typeof(SlotUI))]
    public class ShowTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private SlotUI SlotUI { get; set; }
        private InventoryUI InventoryUI => GetComponentInParent<InventoryUI>();

        private void Awake()
        {
            SlotUI = GetComponent<SlotUI>();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (SlotUI.ItemAmount > 0)
            {
                InventoryUI.ToolTip.gameObject.SetActive(true);
                InventoryUI.ToolTip.SetUpTooltip(SlotUI.ItemDetails, SlotUI.SlotType);

                InventoryUI.ToolTip.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0);
                InventoryUI.ToolTip.transform.position = transform.position + Vector3.up * Settings.TooltipOffset;
            }
            else
            {
                InventoryUI.ToolTip.gameObject.SetActive(false);
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            InventoryUI.ToolTip.gameObject.SetActive(false);
        }
    }
}
