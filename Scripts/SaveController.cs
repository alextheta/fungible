using System.Collections.Generic;
using System.Linq;
using Fungible.Inventory;
using Fungible.Movement;
using Fungible.Storytelling;
using UnityEngine;

namespace Fungible
{
    public class SaveController : MonoBehaviour
    {
        public static bool LoadState;
        public const string ItemPrefix = "Item_";
        public const string ItemPlacePrefix = "ItemPlace_";
        public const string RoomPrefix = "Room_";
        public const string StoryLabelRoomPrefix = "StoryLabelRoom_";
        public const string LastRoomKey = "LastRoom";
        private const string MovementHistoryKey = "MovementHistory";
        private const int TakenItemMark = 1;
        private const int UsedItemMark = 2;
        private const char DataSeparator = ' ';

        private Dictionary<string, Item> _itemMap;
        private Dictionary<string, ItemPlace> _itemPlaceMap;
        private Dictionary<string, Room> _roomMap;
        private Dictionary<string, StoryLabelObject> _storyLabelMap;

        private void Awake()
        {
            LoadState = true;

            /* Items */
            _itemMap = new Dictionary<string, Item>();

            Item[] allItems = Resources.FindObjectsOfTypeAll<Item>();

            foreach (var item in allItems)
            {
                _itemMap[item.ToString()] = item;
                LoadItem(item);
            }

            /* Item places */
            _itemPlaceMap = new Dictionary<string, ItemPlace>();

            ItemPlace[] allItemPlaces = Resources.FindObjectsOfTypeAll<ItemPlace>();

            foreach (ItemPlace itemPlace in allItemPlaces)
            {
                _itemPlaceMap[itemPlace.ToString()] = itemPlace;
                LoadItemPlace(itemPlace);
            }

            /* Rooms */
            _roomMap = new Dictionary<string, Room>();

            Room[] allRooms = Resources.FindObjectsOfTypeAll<Room>();
            foreach (var room in allRooms)
            {
                _roomMap[room.ToString()] = room;
            }

            LoadMovementHistory();
            LoadCurrentRoom();

            _storyLabelMap = new Dictionary<string, StoryLabelObject>();
            StoryLabelRoom[] allStoryLabelRooms = Resources.FindObjectsOfTypeAll<StoryLabelRoom>();
            foreach (StoryLabelRoom storyLabel in allStoryLabelRooms)
            {
                _storyLabelMap[storyLabel.ToString()] = storyLabel;
            }

            LoadStoryLabels();

            LoadState = false;
        }

        private static void LoadItem(Item item)
        {
            var key = item.ToString();
            if (!PlayerPrefs.HasKey(key))
            {
                return;
            }

            item.GetComponent<ItemPickupEventListener>().Event();

            if (PlayerPrefs.GetInt(key) == UsedItemMark)
            {
                InventoryController.Instance.RemoveItem(item);
            }
        }

        public static void SaveItem(Item item)
        {
            if (LoadState)
            {
                return;
            }

            PlayerPrefs.SetInt(item.ToString(), TakenItemMark);
        }

        public static bool IsPickedItem(GameObject item)
        {
            return PlayerPrefs.HasKey(ItemPrefix + item.name);
        }

        public static void SaveItemPlace(ItemPlace itemPlace)
        {
            if (LoadState)
            {
                return;
            }

            var placedItems = "";
            foreach (var item in itemPlace.requiredItems.Where(item => item.placed))
            {
                placedItems += item.item.ToString() + DataSeparator;
                PlayerPrefs.SetInt(item.item.ToString(), UsedItemMark);
            }

            if (string.IsNullOrEmpty(placedItems))
            {
                return;
            }

            PlayerPrefs.SetString(itemPlace.ToString(), placedItems.Trim());
        }

        private void LoadItemPlace(ItemPlace itemPlace)
        {
            var key = itemPlace.ToString();
            if (!PlayerPrefs.HasKey(key))
            {
                return;
            }

            var itemPlaceEventListener = itemPlace.GetComponent<ItemPlaceEventListener>();
            string[] savedItems = PlayerPrefs.GetString(itemPlace.ToString()).Split(DataSeparator);

            foreach (string savedItem in savedItems)
            {
                itemPlace.TryToApplyItem(_itemMap[savedItem]);

                if (itemPlaceEventListener)
                {
                    itemPlaceEventListener.Event();
                }
            }
        }

        public static void SaveMovementHistory(IEnumerable<Room> history)
        {
            if (LoadState)
            {
                return;
            }

            var historyData = history.Aggregate("", (current, room) => current + (room.ToString() + DataSeparator));

            PlayerPrefs.SetString(MovementHistoryKey, historyData.Trim());
        }

        private void LoadMovementHistory()
        {
            string historyData = PlayerPrefs.GetString(MovementHistoryKey);
            if (string.IsNullOrEmpty(historyData))
            {
                return;
            }

            string[] savedRooms = historyData.Split(DataSeparator).Reverse().ToArray();
            foreach (string room in savedRooms)
            {
                MovementHistoryController.Instance.AddPreviousRoom(_roomMap[room]);
            }

            MovementHistoryController.Instance.UpdateBackButton();
        }

        public static void SaveCurrentRoom(Room room)
        {
            if (LoadState)
            {
                return;
            }

            PlayerPrefs.SetString(LastRoomKey, room.ToString());
        }

        private void LoadCurrentRoom()
        {
            if (!PlayerPrefs.HasKey(LastRoomKey))
            {
                return;
            }

            GameplayController.Instance.map.firstRoom = _roomMap[PlayerPrefs.GetString(LastRoomKey)];
        }

        public static void SaveStoryLabel(StoryLabelObject storyLabel)
        {
            PlayerPrefs.SetInt(storyLabel.ToString(), TakenItemMark);
        }

        private void LoadStoryLabels()
        {
            var storyLabels = _storyLabelMap.Where(storyLabelRoom =>
                PlayerPrefs.HasKey(storyLabelRoom.Key) && storyLabelRoom.Value.showOnce);

            foreach (var storyLabel in storyLabels)
            {
                storyLabel.Value.showed = true;
            }
        }
    }
}