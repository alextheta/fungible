using Fungible.Environment;
using UnityEngine;

namespace Fungible.Movement
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class Portal : MonoBehaviour, IClickableObject
    {
        [SerializeField] private bool movementAllowed;
        [SerializeField] private Room roomMoveTo;

        public void OnClick()
        {
            if (movementAllowed)
                GameplayController.Instance.EnterRoom(roomMoveTo);
        }
    }
}