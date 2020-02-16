using Fungible.Environment;
using UnityEngine;

namespace Fungible.Inventory
{
    public class Item : ClickableObject
    {
        public Sprite icon;
        
        private void Awake()
        {
            GetComponent<SpriteRenderer>().sortingOrder = GlobalConfig.SortOrderItem;
        }

        public override void OnClick()
        {
            if (InventoryController.Instance.AddItem(this))
            {
                gameObject.SetActive(false);

                ObjectActivator activator = GetComponent<ObjectActivator>();
                if (activator != null)
                    activator.Invoke();
            }
        }
    }
}