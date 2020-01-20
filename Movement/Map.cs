using UnityEngine;

namespace Fungible.Movement
{
    public class Map : MonoBehaviour
    {
        public static Map instance;
    
        [SerializeField] private Room firstRoom;
        [SerializeField] private Room currentRoom;

        private void Awake()
        {
            instance = this;
            GetComponent<SpriteRenderer>().sortingOrder = GlobalConfig.SortOrderBackground;
        }

        private void Start()
        {
            ChangeRoom(firstRoom);
            AdjustBackground();
        }

        public void EnterRoom(Room room)
        {
            MovementHistoryController.instance.AddPreviousRoom(currentRoom);
            ChangeRoom(room);
        }

        public void ChangeRoom(Room room)
        {
            if (currentRoom != null)
            {
                currentRoom.OnLeave();
                currentRoom.gameObject.SetActive(false);
            }

            currentRoom = room;

            currentRoom.OnEnter();
            currentRoom.gameObject.SetActive(true);
        }

        private void AdjustBackground()
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer == null)
                return;

            transform.position = Vector3.zero;
            transform.localScale = Vector3.one;

            Sprite sprite = spriteRenderer.sprite;
            float width = sprite.bounds.size.x;
            float height = sprite.bounds.size.y;

            float worldScreenHeight = Camera.main.orthographicSize * 2.0f;
            float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;
        
            transform.localScale =
                new Vector3(worldScreenWidth / width, worldScreenHeight / height, transform.localScale.z);
        }
    }
}