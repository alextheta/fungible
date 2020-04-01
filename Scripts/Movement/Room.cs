using UnityEngine;

namespace Fungible.Movement
{
    public class Room : MonoBehaviour
    {
        public delegate void RoomEvent();

        public event RoomEvent enterEvent;
        public event RoomEvent leaveEvent;
        
        public string backgroundResourceName;

        private Sprite backgroundSprite;

        public void OnEnter()
        {
            backgroundSprite = LoadSprite();
            Map.Instance.GetSpriteRenderer().sprite = backgroundSprite;
            enterEvent?.Invoke();
        }

        public void OnLeave()
        {
            Resources.UnloadAsset(backgroundSprite);
            leaveEvent?.Invoke();
        }

        public Sprite LoadSprite()
        {
            return Resources.Load<Sprite>("Backgrounds/" + backgroundResourceName);
        }
    }
}