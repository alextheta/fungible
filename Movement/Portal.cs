using UnityEngine;

namespace Fungible.Movement
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class Portal : ClickableObject
    {
        public bool movementAllowed;
        [SerializeField] private Room roomMoveTo;

        public override void OnClick()
        {
            if (movementAllowed)
                Map.Instance.EnterRoom(roomMoveTo);
        }
    }
}
