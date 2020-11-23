using UnityEngine;

namespace Fungible.Inventory
{
    [DisallowMultipleComponent]
    public class Item : MonoBehaviour
    {
        public Sprite icon;

        public override string ToString()
        {
            return SaveController.ItemPrefix + name;
        }
    }
}