using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Farm.Inventory
{
    public class Item : MonoBehaviour
    {
        [field: SerializeField]
        public int ItemID { get; set; }
        private SpriteRenderer SpriteRenderer { get; set; }
        private BoxCollider2D BoxCollider2D { get; set; }
        public ItemDetails ItemDetails { get; set; }

        private void Awake()
        {
            SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
            BoxCollider2D= GetComponent<BoxCollider2D>();
        }

        private void Start()
        {
            if (ItemID != 0)
            {
                Init(ItemID);
            }
        }

        public void Init(int ID)
        {
            ItemID = ID;

            ItemDetails = InventoryManager.GetInstance.GetItemDetails(ItemID);

            if (ItemDetails != null)
            {
                SpriteRenderer.sprite = ItemDetails.ItemOnWorldSprite != null ? ItemDetails.ItemOnWorldSprite : ItemDetails.ItemIcon;

                // adjust collider box size to adapt to the item size
                Vector2 newSize = new Vector2(SpriteRenderer.sprite.bounds.size.x, SpriteRenderer.sprite.bounds.size.y);
                BoxCollider2D.size = newSize;
                BoxCollider2D.offset = new Vector2(0, SpriteRenderer.sprite.bounds.center.y); //may change later
            }
        }
    }
}
