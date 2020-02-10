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

        private int currentItemCount;
        private InventoryItemHandler selectedItemHandler;

        public bool AddItem(Item item)
        {
            if (currentItemCount >= allowedItemCount - 1)
                return false;

            GameObject itemSlotObject = GetSlotObject(currentItemCount);
            GameObject itemImageObject = GetItemImageObjectInSlot(itemSlotObject);
            InventoryItemHandler itemHandler = itemSlotObject.GetComponent<InventoryItemHandler>();
            Image itemImage = itemImageObject.GetComponent<Image>();

            itemHandler.item = item;
            itemImage.sprite = item.GetComponent<SpriteRenderer>().sprite;
            itemImage.color = unselectedItemColor;
            itemSlotObject.SetActive(true);
            itemImageObject.SetActive(true);

            currentItemCount++;
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

            if (selectedItemHandler != null) Debug.Log("Select [" + selectedItemHandler.item + "]");
        }

        public Item GetSelected()
        {
            return selectedItemHandler != null ? selectedItemHandler.item : null;
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