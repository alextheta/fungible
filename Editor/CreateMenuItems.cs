#if UNITY_EDITOR
using Fungible.Inventory;
using Fungible.Movement;
using UnityEditor;
using UnityEngine;

namespace Fungible.Editor
{
    public static class CreateMenuItems
    {
        [MenuItem("GameObject/Fungible Adventure/Main Controller", false)]
        public static void CreateMainController()
        {
            GameObject controller = new GameObject("Controller");
            controller.AddComponent<MovementHistoryController>();
            controller.AddComponent<InventoryController>();
            controller.transform.localPosition = Vector3.zero;
            controller.transform.localScale = Vector3.one;
        }

        [MenuItem("GameObject/Fungible Adventure/Map", false)]
        public static void CreateMap()
        {
            GameObject map = new GameObject("Map");
            map.AddComponent<SpriteRenderer>();
            map.AddComponent<Map>();
            map.transform.localPosition = Vector3.zero;
            map.transform.localScale = Vector3.one;
        }

        [MenuItem("GameObject/Fungible Adventure/Portal", false)]
        public static void CreatePortal()
        {
            GameObject portal = new GameObject("Portal");
            portal.AddComponent<Portal>();
            portal.GetComponent<BoxCollider2D>().size = Vector2.one;
            portal.transform.parent = Selection.activeTransform;
            portal.transform.localPosition = Vector3.zero;
            portal.transform.localScale = Vector3.one;
        }

        [MenuItem("GameObject/Fungible Adventure/Item", false)]
        public static void CreateItem()
        {
            GameObject item = new GameObject("Item");
            item.AddComponent<SpriteRenderer>();
            item.AddComponent<Item>();
            item.GetComponent<BoxCollider2D>().size = Vector2.one;
            item.transform.parent = Selection.activeTransform;
            item.transform.localPosition = Vector3.zero;
            item.transform.localScale = Vector3.one;
        }

        [MenuItem("GameObject/Fungible Adventure/Item Place Handler", false)]
        public static void CreateItemPlaceHandler()
        {
            GameObject itemPlaceHandler = new GameObject("ItemPlaceHandler");
            itemPlaceHandler.AddComponent<ItemPlace>();
            itemPlaceHandler.GetComponent<BoxCollider2D>().size = Vector2.one;
            itemPlaceHandler.transform.parent = Selection.activeTransform;
            itemPlaceHandler.transform.localPosition = Vector3.zero;
            itemPlaceHandler.transform.localScale = Vector3.one;
        }
    }
}
#endif