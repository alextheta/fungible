using Fungible.Events;
using UnityEngine;

namespace Fungible.Inventory
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(ItemPlace))]
    public class ItemPlaceEventListener : BaseEventListener
    {
        private ItemPlace _itemPlace;

        private ItemPlace ItemPlaceInstance
        {
            get
            {
                if (!_itemPlace)
                {
                    _itemPlace = GetComponent<ItemPlace>();
                }

                return _itemPlace;
            }
        }

        public override void Event()
        {
            if (!SaveController.LoadState && ItemPlaceInstance.WrongItemSelected())
            {
                var wrongEventSender = GetComponent<WrongItemPlaceEventSender>();
                if (wrongEventSender)
                {
                    wrongEventSender.Invoke();
                }
            }

            if (!SaveController.LoadState && !ItemPlaceInstance.TryToApplySelectedItem() || !ItemPlaceInstance.CheckRequiredItems())
            {
                return;
            }

            var eventSender = GetComponent<ItemPlaceEventSender>();
            if (eventSender)
            {
                eventSender.Invoke();
            }
        }
    }
}