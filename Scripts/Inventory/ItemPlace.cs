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
            var item = InventoryController.Instance.GetSelectedItem();
            return TryToApplyItem(item);
        }

        public bool TryToApplyItem(Item item)
        {
            var checkItem = requiredItems.Find(x => x.item == item);
            if (checkItem == null)
            {
                return false;
            }

            checkItem.placed = true;
            item.used = true;
            InventoryController.Instance.RemoveItem(item);
            SaveController.SaveItemPlace(this);
            return true;
        }

        public bool WrongItemSelected()
        {
            var item = InventoryController.Instance.GetSelectedItem();
            return wrongItems.Any(wrongItem => wrongItem == item);
        }

        public bool CheckRequiredItems()
        {
            return requiredItems.Aggregate(true, (current, requiredItem) => current & requiredItem.placed);
        }

        public override string ToString()
        {
            return SaveController.ItemPlacePrefix + name;
        }
    }
}