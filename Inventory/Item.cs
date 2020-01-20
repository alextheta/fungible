using UnityEngine;

namespace Fungible.Inventory
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class Item : MonoBehaviour
    {
        private void Awake()
        {
            GetComponent<SpriteRenderer>().sortingOrder = GlobalConfig.SortOrderItem;
        }

        private void OnMouseDown()
        {
            Debug.Log("Item [" + this + "]");
        }
    }
}
