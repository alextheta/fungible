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

        private InventoryItemHandler _selectedItemHandler;
        private List<Item> _itemsInInventory;

        public bool AddItem(Item item)
        {
            if (_itemsInInventory.Count >= allowedItemCount || _itemsInInventory.Contains(item) || item.used)
            {
                return false;
            }

            _itemsInInventory.Add(item);
            UpdateInventoryPanel();
            SaveController.SaveItem(item);
            return true;
        }

        public void RemoveItem(Item item)
        {
            if (_itemsInInventory.Count <= 0)
            {
                return;
            }

            _itemsInInventory.Remove(item);
            UpdateInventoryPanel();
        }

        public void SelectItem(InventoryItemHandler itemHandler)
        {
            if (_selectedItemHandler)
            {
                var itemImage = GetItemImageObjectInSlot(_selectedItemHandler.gameObject).GetComponent<Image>();
                itemImage.color = unselectedItemColor;
            }

            _selectedItemHandler = itemHandler != _selectedItemHandler ? itemHandler : null;

            if (_selectedItemHandler)
            {
                var itemImage = GetItemImageObjectInSlot(_selectedItemHandler.gameObject).GetComponent<Image>();
                itemImage.color = _selectedItemHandler ? selectedItemColor : unselectedItemColor;
            }
        }

        public Item GetSelectedItem()
        {
            return _selectedItemHandler ? _selectedItemHandler.item : null;
        }

        private void UpdateInventoryPanel()
        {
            _selectedItemHandler = null;
            for (var i = 0; i < inventoryPanel.transform.childCount; i++)
            {
                var itemSlotObject = GetSlotObject(i);
                var itemImageObject = GetItemImageObjectInSlot(itemSlotObject);
                var itemHandler = itemSlotObject.GetComponent<InventoryItemHandler>();
                var itemImage = itemImageObject.GetComponent<Image>();
                if (i >= _itemsInInventory.Count)
                {
                    itemHandler.item = null;
                    itemImage.sprite = null;
                    itemImageObject.SetActive(false);
                    continue;
                }

                var item = _itemsInInventory[i];
                itemHandler.item = item;
                itemImage.sprite = item.icon;
                itemImage.color = unselectedItemColor;
                itemSlotObject.SetActive(true);
                itemImageObject.SetActive(true);
                itemHandler.item = _itemsInInventory[i];
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

            _itemsInInventory = new List<Item>();

            for (var i = 0; i < allowedItemCount; i++)
            {
                var slot = Instantiate(slotPrefab, inventoryPanel.transform);
                slot.name = slotPrefab.name + i;

                if (slotFixedAspect)
                {
                    slot.GetComponent<AspectRatioFitter>().enabled = true;
                }

                if (itemFixedAspect)
                {
                    GetItemImageObjectInSlot(slot).GetComponent<AspectRatioFitter>().enabled = true;
                }
            }
        }
    }
}