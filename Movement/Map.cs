using Fungible.UI;
using UnityEngine;

namespace Fungible.Movement
{
    public class Map : MonoBehaviour
    {
        public static Map Instance;

        [SerializeField] private Room firstRoom;
        [SerializeField] private Room currentRoom;

        private void Awake()
        {
            Instance = this;
            GetComponent<SpriteRenderer>().sortingOrder = GlobalConfig.SortOrderBackground;
        }

        private void Start()
        {
            ChangeRoom(firstRoom);
            AdjustBackground();
        }

        public void EnterRoom(Room room)
        {
            MovementHistoryController.Instance.AddPreviousRoom(currentRoom);
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

            CoverPanel coverPanel = FindObjectOfType<CoverPanel>();
            if (coverPanel == null)
                return;

            Sprite sprite = spriteRenderer.sprite;

            transform.position = Vector3.zero;
            transform.localScale = Vector3.one;

            float worldScreenHeight = Camera.main.orthographicSize * 2.0f;
            float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

            Vector2 spriteSize = new Vector2(sprite.bounds.size.x, sprite.bounds.size.y);
            Vector2 coverPanelSize = coverPanel.GetWorldSize();
            Vector3 scaleDiff = new Vector3((worldScreenWidth - coverPanelSize.x) / 2.0f,
                (worldScreenHeight - coverPanelSize.y) / 2.0f, 0);

            transform.localScale = new Vector3(coverPanelSize.x / spriteSize.x, coverPanelSize.y / spriteSize.y,
                transform.localScale.z);

            transform.position -= scaleDiff;
        }
    }
}