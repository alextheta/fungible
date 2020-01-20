using System;
using System.Collections.Generic;
using UnityEngine;

namespace Fungible.Inventory
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private List<Item> fullItemList;

        private void Awake()
        {
            fullItemList = new List<Item>(Resources.FindObjectsOfTypeAll<Item>());
        }
    }
}
