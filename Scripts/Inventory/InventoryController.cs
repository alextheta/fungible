using System.Collections.Generic;
using Fungible.UI;
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

        public Color selectedItemColor;
        public Color unselectedItemColor;

        public static InventoryController Instance;

        private InventoryItemHandler selectedItemHandler;
        private List<Item> itemsInInventory;

        public bool AddItem(Item item)
        {
            if (itemsInInventory.Count >= allowedItemCount)
                return false;
            
            itemsInInventory.Add(item);
            UpdateInventoryPanel();
            return true;
        }
        
        public bool RemoveItem(Item item)
        {
            if (itemsInInventory.Count <= 0)
                return false;

            itemsInInventory.Remove(item);
            UpdateInventoryPanel();
            return true;
        }

        public void SelectItem(InventoryItemHandler itemHandler)
        {
            if (selectedItemHandler != null)
            {
                Image itemImage = GetItemImageObjectInSlot(selectedItemHandler.gameObject).GetComponent<Image>();
                itemImage.color = unselectedItemColor;
            }

            selectedItemHandler = itemHandler != selectedItemHandler ? itemHandler : null;

            if (selectedItemHandler != null)
            {
                Image itemImage = GetItemImageObjectInSlot(selectedItemHandler.gameObject).GetComponent<Image>();
                itemImage.color = selectedItemHandler ? selectedItemColor : unselectedItemColor;
            }
        }

        public Item GetSelected()
        {
            return selectedItemHandler != null ? selectedItemHandler.item : null;
        }

        private void UpdateInventoryPanel()
        {
            selectedItemHandler = null;
            for (int i = 0; i < inventoryPanel.transform.childCount; i++)
            {
                GameObject itemSlotObject = GetSlotObject(i);
                GameObject itemImageObject = GetItemImageObjectInSlot(itemSlotObject);
                InventoryItemHandler itemHandler = itemSlotObject.GetComponent<InventoryItemHandler>();
                Image itemImage = itemImageObject.GetComponent<Image>();
                if (i >= itemsInInventory.Count)
                {
                    itemHandler.item = null;
                    itemImage.sprite = null;
                    itemImageObject.SetActive(false);
                    continue;
                }

                Item item = itemsInInventory[i];
                itemHandler.item = item;
                itemImage.sprite = item.GetComponent<SpriteRenderer>().sprite;
                itemImage.color = unselectedItemColor;
                itemSlotObject.SetActive(true);
                itemImageObject.SetActive(true);
                itemHandler.item = itemsInInventory[i];
            }
        }
        
        private GameObject GetSlotObject(int index)
        {
            return index > inventoryPanel.transform.childCount
                ? null
                : inventoryPanel.transform.GetChild(index).gameObject;
        }

        private GameObject GetItemImageObjectInSlot(GameObject slot)
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
                slot.name = slotPrefab.name + i;

                if (slotFixedAspect)
                    slot.GetComponent<AspectRatioFitter>().enabled = true;

                if (itemFixedAspect)
                    GetItemImageObjectInSlot(slot).GetComponent<AspectRatioFitter>().enabled = true;
            }
        }
    }
}