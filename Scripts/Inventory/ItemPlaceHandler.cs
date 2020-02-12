using System;
using System.Collections.Generic;
using UnityEngine;

namespace Fungible.Inventory
{
    public class ItemPlaceHandler : ClickableObject
    {
        [Serializable]
        public class ItemPlaced
        {
            public bool placed;
            public Item item;
        }

        [SerializeField] private List<ItemPlaced> requiredItems;

        public override void OnClick()
        {
            if (TryToApplySelectedItem() && CheckRequiredItems())
                Debug.Log("Place Handler [" + this + "] is fully filled");
        }

        private bool TryToApplySelectedItem()
        {
            bool applied = false;
            Item item = InventoryController.Instance.GetSelected();
            for (int i = 0; i < requiredItems.Count; i++)
            {
                ItemPlaced checkedItem = requiredItems[i];
                if (checkedItem.item != item)
                    continue;

                checkedItem.placed = true;
                applied = InventoryController.Instance.RemoveItem(item);
                break;
            }

            return applied;
        }

        private bool CheckRequiredItems()
        {
            bool allItemsIsPlaced = true;

            foreach (ItemPlaced requiredItem in requiredItems)
                allItemsIsPlaced &= requiredItem.placed;

            return allItemsIsPlaced;
        }
    }
}
