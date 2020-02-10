using UnityEngine;

namespace Fungible.UI
{
    public class InventoryItemButton : MonoBehaviour
    {
        public void OnClick()
        {
            if (ProxyControlsPanel.Instance.ControlsAllowed())
                Debug.Log("Slot index [" + transform.parent.GetSiblingIndex() + "]");
        }
    }
}