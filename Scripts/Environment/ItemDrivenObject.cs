using Fungible.Inventory;
using UnityEngine;

namespace Fungible.Environment
{
    //[DisallowMultipleComponent]
    public abstract class ItemDrivenObject : MonoBehaviour
    {
        private void Awake()
        {
            Item item = GetComponent<Item>();
            if (item)
                item.ClickEvent += OnClick;
            
            ItemPlaceHandler itemPlaceHandler = GetComponent<ItemPlaceHandler>();
            if (itemPlaceHandler)
                itemPlaceHandler.ClickEvent += OnClick;

            if (!item && !itemPlaceHandler)
            {
                ClickableObject clickableObject = GetComponent<ClickableObject>();
                if (clickableObject)
                    clickableObject.ClickEvent += OnClick;
            }
        }
        
        private void OnDestroy()
        {
            Item item = GetComponent<Item>();
            if (item)
                item.ClickEvent -= OnClick;
            
            ItemPlaceHandler itemPlaceHandler = GetComponent<ItemPlaceHandler>();
            if (itemPlaceHandler)
                itemPlaceHandler.ClickEvent -= OnClick;
            
            ClickableObject clickableObject = GetComponent<ClickableObject>();
            if (clickableObject)
                clickableObject.ClickEvent -= OnClick;
        }

        protected abstract void OnClick();
    }
}
