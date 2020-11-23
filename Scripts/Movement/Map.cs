﻿using Fungible.UI;
using UnityEngine;

namespace Fungible.Movement
{
    public class Map : MonoBehaviour
    {
        public Room firstRoom;

        public static Map Instance;

        private SpriteRenderer _spriteRenderer;
        private Room _currentRoom;

        public Room GetCurrentRoom()
        {
            return _currentRoom;
        }

        public SpriteRenderer GetSpriteRenderer()
        {
            return _spriteRenderer;
        }

        public void ChangeRoom(Room room)
        {
            if (_currentRoom)
            {
                _currentRoom.OnLeave();
                _currentRoom.gameObject.SetActive(false);
            }

            _currentRoom = room;

            _currentRoom.gameObject.SetActive(true);
            _currentRoom.OnEnter();
            SaveController.SaveCurrentRoom(_currentRoom);
        }

        private void Awake()
        {
            Instance = this;
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            _currentRoom = firstRoom;

            _currentRoom.gameObject.SetActive(true);
            _currentRoom.OnEnter();

            AdjustBackground();
        }

        public void AdjustBackground()
        {
            var mainPanel = FindObjectOfType<MainPanel>();
            var mainCamera = Camera.main;
            if (!mainPanel)
            {
                Debug.LogError("Main panel is not set to " + this);
                return;
            }

            Sprite sprite = GetComponent<SpriteRenderer>().sprite;
            if (!sprite)
            {
                Debug.LogError("Sprite is not set to " + _spriteRenderer);
                return;
            }

            if (!mainCamera)
            {
                Debug.LogError("Main camera is not exist");
                return;
            }

            transform.position = Vector3.zero;
            transform.localScale = Vector3.one;

            var worldScreenHeight = mainCamera.orthographicSize * 2.0f;
            var worldScreenWidth = worldScreenHeight * mainCamera.aspect;

            var spriteSize = new Vector2(sprite.bounds.size.x, sprite.bounds.size.y);
            var coverPanelSize = mainPanel.GetWorldSize();
            var scaleDiff = new Vector3((worldScreenWidth - coverPanelSize.x) / 2.0f,
                (worldScreenHeight - coverPanelSize.y) / 2.0f, 0);

            transform.localScale = new Vector3(coverPanelSize.x / spriteSize.x, coverPanelSize.y / spriteSize.y,
                transform.localScale.z);

            transform.position -= scaleDiff;
        }
    }
}