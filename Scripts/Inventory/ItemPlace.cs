using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Fungible.Inventory
{
    [DisallowMultipleComponent]
    public class ItemPlace : MonoBehaviour
    {
        [Serializable]
        public class ItemPlaced
        {
            public bool placed;
            public Item item;
        }

        public List<ItemPlaced> requiredItems;
        public List<Item> wrongItems;

        public bool TryToApplySelectedItem()
        {
            bool applied = false;
            Item item = InventoryController.Instance.GetSelectedItem();
            foreach (ItemPlaced checkedItem in requiredItems.Where(checkedItem => checkedItem.item == item))
            {
                checkedItem.placed = true;
                applied = InventoryController.Instance.RemoveItem(item);
                break;
            }

            return applied;
        }

        public bool WrongItemSelected()
        {
            Item item = InventoryController.Instance.GetSelectedItem();
            foreach (var wrongItem in wrongItems)
            {
                if (wrongItem == item)
                    return true;
            }

            return false;
        }

        public bool CheckRequiredItems()
        {
            return requiredItems.Aggregate(true, (current, requiredItem) => current & requiredItem.placed);
        }
    }
}