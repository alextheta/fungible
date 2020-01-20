using System.Collections.Generic;
using UnityEngine;

namespace Fungible.Movement
{
    public class MovementHistoryController : MonoBehaviour
    {
        public static MovementHistoryController instance;

        public Stack<Room> history;

        private void Awake()
        {
            instance = this;
            history = new Stack<Room>();
        }

        public void AddPreviousRoom(Room room)
        {
            if (room != null)
                history.Push(room);
        }

        public void PopRoom()
        {
            if (history.Count != 0)
                Map.instance.ChangeRoom(history.Pop());
        }
    }
}