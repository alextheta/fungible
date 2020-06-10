using System;
using System.Collections.Generic;
using System.Linq;
using Fungible.Environment;
using UnityEngine;

namespace Fungible.Inventory
{
    [RequireComponent(typeof(ClickableObject))]
    public class ItemPlaceHandler : MonoBehaviour
    {
        [Serializable]
        public class ItemPlaced
        {
            public bool placed;
            public Item item;
        }

        public List<ItemPlaced> requiredItems;
        public delegate void ClickAction();
        public event ClickAction ClickEvent;

        private ClickableObject clickableObject;

        private void OnClick()
        {
            if (!clickableObject.clickable || !TryToApplySelectedItem() || !CheckRequiredItems())
                return;

            clickableObject.clickable = false;

            ClickEvent?.Invoke();
        }

        private void Awake()
        {
            clickableObject = GetComponent<ClickableObject>();
            clickableObject.ClickEvent += OnClick;
        }

        private void OnDestroy()
        {
            clickableObject.ClickEvent -= OnClick;
        }

        private bool TryToApplySelectedItem()
        {
            bool applied = false;
            Item item = InventoryController.Instance.GetSelected();
            foreach (ItemPlaced checkedItem in requiredItems.Where(checkedItem => checkedItem.item == item))
            {
                checkedItem.placed = true;
                applied = InventoryController.Instance.RemoveItem(item);
                break;
            }

            return applied;
        }

        private bool CheckRequiredItems()
        {
            bool allItemsArePlaced = true;

            foreach (ItemPlaced requiredItem in requiredItems)
                allItemsArePlaced &= requiredItem.placed;

            return allItemsArePlaced;
        }
    }
}