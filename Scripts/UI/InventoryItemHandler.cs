using Fungible.Inventory;
using UnityEngine;

namespace Fungible.UI
{
    public class InventoryItemHandler : MonoBehaviour
    {
        public Item item;
        
        public void OnClick()
        {
            if (item != null && ProxyControlsPanel.Instance.ControlsAllowed())
                InventoryController.Instance.SelectItem(this);
        }
    }
}