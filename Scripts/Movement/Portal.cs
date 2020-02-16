using Fungible.Environment;
using UnityEngine;

namespace Fungible.Movement
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class Portal : ClickableObject
    {
        public bool movementAllowed;
        public Room roomMoveTo;

        public override void OnClick()
        {
            if (movementAllowed)
                GameplayController.Instance.EnterRoom(roomMoveTo);
        }
    }
}