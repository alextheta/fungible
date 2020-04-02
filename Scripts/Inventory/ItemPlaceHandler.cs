using System;
using System.Collections.Generic;
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

        private void OnClick()
        {
            if (!TryToApplySelectedItem() || !CheckRequiredItems())
                return;

            ObjectActivator activator = GetComponent<ObjectActivator>();
            if (activator != null)
                activator.Invoke();
        }

        private void Awake()
        {
            GetComponent<ClickableObject>().ClickEvent += OnClick;
        }

        private void OnDestroy()
        {
            GetComponent<ClickableObject>().ClickEvent -= OnClick;
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
