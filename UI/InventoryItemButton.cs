using UnityEngine;

namespace Fungible.UI
{
    public class InventoryItemButton : MonoBehaviour
    {
        public void OnClick()
        {
            Debug.Log("Slot index [" + transform.parent.GetSiblingIndex() + "]");
        }
    }
}