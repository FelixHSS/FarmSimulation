using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Farm.Inventory
{
    public class ItemManager : MonoBehaviour
    {
        [field: SerializeField] public Item ItemPrefab { get; set; }
        private Transform ItemParent { get; set; }

        private void OnEnable()
        {
            EventHandler.InstantiateItemInScene += OnInstantiateItemInScene;
        }

        private void OnDisable()
        {
            EventHandler.InstantiateItemInScene -= OnInstantiateItemInScene;
        }

        private void Start()
        {
            ItemParent = GameObject.FindWithTag("ItemParent").transform;
        }

        private void OnInstantiateItemInScene(int ID, Vector3 position)
        {
            Item item = Instantiate(ItemPrefab, position, Quaternion.identity, ItemParent);

            item.ItemID = ID; // Then Init(...) will be called in Start() in Item.cs
        }
    }
}
