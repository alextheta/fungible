using System;
using System.Collections.Generic;
using Fungible.Environment;

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

        public List<ItemPlaced> requiredItems;

        public override void OnClick()
        {
            if (TryToApplySelectedItem() && CheckRequiredItems())
            {
                ObjectActivator activator = GetComponent<ObjectActivator>();
                if (activator != null)
                    activator.Invoke();
            }
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
            bool allItemsArePlaced = true;

            foreach (ItemPlaced requiredItem in requiredItems)
                allItemsArePlaced &= requiredItem.placed;

            return allItemsArePlaced;
        }
    }
}
