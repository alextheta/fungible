using UnityEngine;

namespace Fungible.Movement
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class MovementPortal : MonoBehaviour
    {
        public bool movementAllowed;
        [SerializeField] private Room roomMoveTo;
    
        void OnMouseDown()
        {
            if (movementAllowed)
                Map.instance.EnterRoom(roomMoveTo);
        }
    }
}
