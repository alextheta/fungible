using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Fungible.Inventory
{
    public class InventoryController : MonoBehaviour
    {
        [Header("Configuration")] public GameObject slotPrefab;
        public GameObject inventoryPanel;
        public bool slotFixedAspect;
        public bool itemFixedAspect;
        public int allowedItemCount;

        public static InventoryController Instance;

        [Header("Ingame Inventory")]
        [SerializeField] private List<Item> itemsInInventory;

        public bool AddItem(Item item)
        {
            if (itemsInInventory.Count >= allowedItemCount)
                return false;

            itemsInInventory.Add(item);
            UpdateInventoryPanel();
            return true;
        }

        private void UpdateInventoryPanel()
        {
            for (int i = 0; i < allowedItemCount; i++)
            {
                Image itemImageInSlot = GetItemImageInSlot(GetSlot(i)).GetComponent<Image>();
                if (i < itemsInInventory.Count)
                {
                    itemImageInSlot.sprite = itemsInInventory[i].gameObject.GetComponent<SpriteRenderer>().sprite;
                    itemImageInSlot.gameObject.SetActive(true);
                }
                else
                {
                    itemImageInSlot.gameObject.SetActive(false);
                }
            }
        }

        private GameObject GetSlot(int index)
        {
            if (index > inventoryPanel.transform.childCount)
                return null;

            return inventoryPanel.transform.GetChild(index).gameObject;
        }

        private GameObject GetItemImageInSlot(GameObject slot)
        {
            return slot.transform.GetChild(0).gameObject;
        }

        private void Awake()
        {
            Instance = this;

            itemsInInventory = new List<Item>();

            for (int i = 0; i < allowedItemCount; i++)
            {
                GameObject slot = Instantiate(slotPrefab, inventoryPanel.transform);
                slot.name = slotPrefab.name;

                if (slotFixedAspect)
                    slot.GetComponent<AspectRatioFitter>().enabled = true;

                GameObject itemInSlot = GetItemImageInSlot(slot);

                if (itemFixedAspect)
                    itemInSlot.GetComponent<AspectRatioFitter>().enabled = true;
            }
        }
    }
}