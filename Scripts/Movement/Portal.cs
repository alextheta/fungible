using Fungible.Environment;
using UnityEngine;

namespace Fungible.Movement
{
    [RequireComponent(typeof(ClickableObject))]
    public class Portal : MonoBehaviour
    {
        [SerializeField] private bool movementAllowed;
        [SerializeField] private Room roomMoveTo;

        private void Awake()
        {
            GetComponent<ClickableObject>().ClickEvent += OnClick;
        }

        private void OnDestroy()
        {
            GetComponent<ClickableObject>().ClickEvent -= OnClick;
        }

        private void OnClick()
        {
            if (movementAllowed)
                GameplayController.Instance.EnterRoom(roomMoveTo);
        }
    }
}