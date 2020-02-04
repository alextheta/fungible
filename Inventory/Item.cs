using UnityEngine;

namespace Fungible.Inventory
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class Item : ClickableObject
    {
        private void Awake()
        {
            GetComponent<SpriteRenderer>().sortingOrder = GlobalConfig.SortOrderItem;
        }

        public override void OnClick()
        {
            if (InventoryController.Instance.AddItem(this))
                gameObject.SetActive(false);
        }
    }
}