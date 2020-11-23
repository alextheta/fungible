using Fungible.Inventory;
using UnityEngine;

namespace Fungible.Movement
{
    public class Room : MonoBehaviour
    {
        public delegate void RoomEvent();

        public event RoomEvent EnterEvent;
        public event RoomEvent LeaveEvent;

        public string backgroundResourceName;

        private Sprite _backgroundSprite;

        public void OnEnter()
        {
            _backgroundSprite = LoadSprite();
            Map.Instance.GetSpriteRenderer().sprite = _backgroundSprite;
            EnterEvent?.Invoke();
        }

        public void OnLeave()
        {
            Resources.UnloadUnusedAssets();
            InventoryController.Instance.SelectItem(null);
            LeaveEvent?.Invoke();
        }

        public Sprite LoadSprite()
        {
            return Resources.Load<Sprite>("Backgrounds/" + backgroundResourceName);
        }

        public override string ToString()
        {
            return SaveController.RoomPrefix + name;
        }
    }
}