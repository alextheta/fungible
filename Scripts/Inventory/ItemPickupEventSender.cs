using Fungible.Events;
using UnityEngine;

namespace Fungible.Inventory
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(ItemPickupEventListener))]
    public class ItemPickupEventSender : BaseEventSender
    {
    }
}