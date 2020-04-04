using Fungible.Environment;
using UnityEngine;

namespace Fungible.Movement
{
    [RequireComponent(typeof(ClickableObject))]
    public class Portal : MonoBehaviour
    {
        private ClickableObject clickableObject;
        [SerializeField] private Room roomMoveTo;

        private void Awake()
        {
            clickableObject = GetComponent<ClickableObject>();
            clickableObject.ClickEvent += OnClick;
        }

        private void OnDestroy()
        {
            clickableObject.ClickEvent -= OnClick;
        }

        private void OnClick()
        {
            GameplayController.Instance.EnterRoom(roomMoveTo);
        }
    }
}