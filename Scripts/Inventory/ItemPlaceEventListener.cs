using Fungible.Events;
using UnityEngine;

namespace Fungible.Inventory
{
    [DisallowMultipleComponent]
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
            if (_itemPlace.WrongItemSelected())
            {
                var wrongEventSender = GetComponent<WrongItemPlaceEventSender>();
                if (wrongEventSender)
                    wrongEventSender.Invoke();
                return;
            }
            
            if (!_itemPlace.TryToApplySelectedItem() || !_itemPlace.CheckRequiredItems())
                return;

            var eventSender = GetComponent<ItemPlaceEventSender>();
            if (eventSender)
                eventSender.Invoke();
        }
    }
}