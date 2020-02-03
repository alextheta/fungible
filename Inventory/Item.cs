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
            Debug.Log("Item [" + this + "]");
        }
    }
}
