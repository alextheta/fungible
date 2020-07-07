using Fungible.Events;
using UnityEngine;

namespace Fungible.Inventory
{
    [RequireComponent(typeof(ItemPlace))]
    public class ItemPlaceEventListener : BaseEventListener
    {
        private ItemPlace _itemPlace;

        private void Awake()
        {
            _itemPlace = GetComponent<ItemPlace>();
        }

        public override void Event()
        {
            if (!_itemPlace.TryToApplySelectedItem() || !_itemPlace.CheckRequiredItems())
                return;

            ItemPlaceEventSender eventSender = GetComponent<ItemPlaceEventSender>();
            if (eventSender)
                eventSender.Invoke();
        }
    }
}