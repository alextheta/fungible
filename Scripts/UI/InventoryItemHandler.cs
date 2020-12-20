using Fungible.Inventory;
using UnityEngine;
using UnityEngine.UI;

namespace Fungible.UI
{
    public class InventoryItemHandler : MonoBehaviour
    {
        public Item item;

        public Image Image
        {
            get
            {
                if (_image)
                {
                    return _image;
                }

                _image = GetComponent<Image>();
                return _image;
            }

            set => _image = value;
        }

        private Image _image;

        public void OnClick()
        {
            if (item != null && ProxyControlsPanel.Instance.ControlsAllowed())
            {
                InventoryController.Instance.SelectItem(this);
            }
        }
    }
}