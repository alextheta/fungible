using System.Collections;
using Fungible;
using Fungible.Events;
using Fungible.Inventory;
using UnityEngine;

[RequireComponent(typeof(Item))]
public class ItemPickupEventListener : BaseEventListener
{
    private AppearAnimationController _animationController;
    
    private void Awake()
    {
        _animationController = GetComponent<AppearAnimationController>();
    }
    
    public override void Event()
    {
        Item item = GetComponent<Item>();
        if (!InventoryController.Instance.AddItem(item))
            return;

        ItemPickupEventSender eventSender = GetComponent<ItemPickupEventSender>();
        if (eventSender)
            eventSender.Invoke();
            
        SelfDisable();
    }

    private void SelfDisable()
    {
        StartCoroutine(SelfDisableCoroutine());
    }

    private IEnumerator SelfDisableCoroutine()
    {
        if (!_animationController)
            yield return null;
        else
            yield return _animationController.SetInvisibleCoroutine();

        gameObject.SetActive(false);
    }
}
