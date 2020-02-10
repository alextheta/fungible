using System;
using System.Collections.Generic;
using UnityEngine;

namespace Fungible.Inventory
{
    public class ItemPlaceHandler : ClickableObject
    {
        [Serializable]
        public struct ItemPlaced
        {
            public bool placed;
            public Item item;
        }

        [SerializeField] private List<ItemPlaced> requiredItems;

        public override void OnClick()
        {
            Item item = InventoryController.Instance.GetSelected();
            Debug.Log("Sel [" + item + "] [" + this + "]");
        }
    }
}
