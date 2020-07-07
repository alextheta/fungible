using System.Collections.Generic;
using UnityEngine;

namespace Fungible.Movement
{
    public class MovementHistoryController : MonoBehaviour
    {
        public GameObject backButton;
        
        public static MovementHistoryController Instance;

        private Stack<Room> _history;

        private void Awake()
        {
            Instance = this;
            _history = new Stack<Room>();
        }

        public void AddPreviousRoom(Room room)
        {
            if (room != null)
                _history.Push(room);
        }

        public void PopRoom()
        {
            if (_history.Count != 0)
                GameplayController.Instance.ChangeRoom(_history.Pop());
        }

        public void UpdateBackButton()
        {
            backButton.SetActive(_history.Count != 0);
        }
    }
}