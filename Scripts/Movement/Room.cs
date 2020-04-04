using UnityEngine;

namespace Fungible.Movement
{
    public class Room : MonoBehaviour
    {
        public delegate void RoomEvent();

        public event RoomEvent EnterEvent;
        public event RoomEvent LeaveEvent;
        
        public string backgroundResourceName;

        private Sprite backgroundSprite;

        public void OnEnter()
        {
            backgroundSprite = LoadSprite();
            Map.Instance.GetSpriteRenderer().sprite = backgroundSprite;
            EnterEvent?.Invoke();
        }

        public void OnLeave()
        {
            Resources.UnloadAsset(backgroundSprite);
            LeaveEvent?.Invoke();
        }

        public Sprite LoadSprite()
        {
            return Resources.Load<Sprite>("Backgrounds/" + backgroundResourceName);
        }
    }
}