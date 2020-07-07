using Fungible.Events;
using UnityEngine;

namespace Fungible.Inventory
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(ItemPlaceEventListener))]
    public class ItemPlaceEventSender : BaseEventSender
    {
    }
}