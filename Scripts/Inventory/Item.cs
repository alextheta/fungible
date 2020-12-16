using UnityEngine;

namespace Fungible.Inventory
{
    [DisallowMultipleComponent]
    public class Item : MonoBehaviour
    {
        public Sprite icon;
        [HideInInspector] public bool used;

        public override string ToString()
        {
            return SaveController.ItemPrefix + name;
        }
    }
}