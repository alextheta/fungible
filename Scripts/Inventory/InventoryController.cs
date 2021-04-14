using System.Collections.Generic;
using Assets.SimpleLocalization;
using Fungible.Storytelling;
using Fungible.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Fungible.Inventory
{
    public class InventoryController : MonoBehaviour
    {
        [Header("Configuration")] public GameObject slotPrefab;
        [SerializeField] private GameObject inventoryPanel;
        [SerializeField] private bool slotFixedAspect;
        [SerializeField] private bool itemFixedAspect;
        [SerializeField] private int allowedItemCount;
        [SerializeField] private bool showItemTextOnSelect;
        [SerializeField] private bool tintIconFrame;

        [SerializeField] private Color itemSelectedColor;
        [SerializeField] private Color itemUnselectedColor;
        [SerializeField] private Color frameSelectedColor;
        [SerializeField] private Color frameUnselectedColor;

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
            Image itemImage;
            if (_selectedItemHandler)
            {
                itemImage = GetItemImageObjectInSlot(_selectedItemHandler.gameObject);
                itemImage.color = itemUnselectedColor;
                if (tintIconFrame)
                {
                    _selectedItemHandler.Image.color = frameUnselectedColor;
                }
            }

            _selectedItemHandler = itemHandler != _selectedItemHandler ? itemHandler : null;

            if (!_selectedItemHandler)
            {
                return;
            }

            itemImage = GetItemImageObjectInSlot(_selectedItemHandler.gameObject);
            itemImage.color = itemSelectedColor;
            if (tintIconFrame)
            {
                _selectedItemHandler.Image.color = frameSelectedColor;
            }

            if (!showItemTextOnSelect)
            {
                return;
            }

            string itemKey = itemHandler.item.GetComponent<LocalizedFungibleObject>().localizationKey;
            string itemText = LocalizationManager.Localize(itemKey);
            StoryLabelController.Instance.SetText(itemText);
            StoryLabelController.Instance.Show();
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
                GameObject itemSlotObject = GetSlotObject(i);
                Image itemImage = GetItemImageObjectInSlot(itemSlotObject);
                var itemHandler = itemSlotObject.GetComponent<InventoryItemHandler>();

                if (tintIconFrame)
                {
                    itemHandler.Image.color = frameUnselectedColor;
                }

                if (i >= _itemsInInventory.Count)
                {
                    itemHandler.item = null;
                    itemImage.sprite = null;
                    itemImage.gameObject.SetActive(false);
                    continue;
                }

                Item item = _itemsInInventory[i];
                itemHandler.item = item;
                itemImage.sprite = item.icon;
                itemImage.color = itemUnselectedColor;
                itemSlotObject.SetActive(true);
                itemImage.gameObject.SetActive(true);
                itemHandler.item = _itemsInInventory[i];
            }
        }

        private GameObject GetSlotObject(int index)
        {
            return index > inventoryPanel.transform.childCount
                ? null
                : inventoryPanel.transform.GetChild(index).gameObject;
        }

        private static Image GetItemImageObjectInSlot(GameObject slot)
        {
            return slot.transform.GetChild(0).gameObject.GetComponent<Image>();
        }

        private void Awake()
        {
            Instance = this;

            _itemsInInventory = new List<Item>();

            for (var i = 0; i < allowedItemCount; i++)
            {
                GameObject slot = Instantiate(slotPrefab, inventoryPanel.transform);
                slot.name = slotPrefab.name + i;

                if (slotFixedAspect)
                {
                    slot.GetComponent<AspectRatioFitter>().enabled = true;
                }

                if (itemFixedAspect)
                {
                    GetItemImageObjectInSlot(slot).GetComponent<AspectRatioFitter>().enabled = true;
                }

                if (tintIconFrame)
                {
                    slot.GetComponent<Image>().color = frameUnselectedColor;
                }
            }
        }
    }
}