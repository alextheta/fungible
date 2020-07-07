using Fungible.Events;
using UnityEngine;

namespace Fungible.Movement
{
    public class Portal : BaseEventListener
    {
        [SerializeField] private Room roomMoveTo;

        public override void Event()
        {
            GameplayController.Instance.EnterRoom(roomMoveTo);
        }
    }
}