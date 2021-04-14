using DG.Tweening;
using Fungible.Animation;
using Fungible.Events;
using UnityEngine;

namespace Fungible.Inventory
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Item))]
    public class ItemPickupEventListener : BaseEventListener
    {
        private SpriteAnimationController _animationController;

        private void Awake()
        {
            _animationController = GetComponent<SpriteAnimationController>();
        }

        public override void Event()
        {
            var item = GetComponent<Item>();
            if (!InventoryController.Instance.AddItem(item))
            {
                return;
            }

            var eventSender = GetComponent<ItemPickupEventSender>();
            if (eventSender)
            {
                eventSender.Invoke();
            }

            SelfDisable();
        }

        private void SelfDisable()
        {
            if (!SaveController.LoadState && _animationController)
            {
                _animationController.DisappearTween().OnComplete(() => gameObject.SetActive(false));
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}