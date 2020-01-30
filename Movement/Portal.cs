using UnityEngine;

namespace Fungible.Movement
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class Portal : MonoBehaviour
    {
        public bool movementAllowed;
        [SerializeField] private Room roomMoveTo;
    
        private void OnMouseDown()
        {
            if (movementAllowed)
                Map.Instance.EnterRoom(roomMoveTo);
        }
    }
}
