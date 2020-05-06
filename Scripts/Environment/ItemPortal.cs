using Fungible.Environment;
using UnityEngine;

namespace Fungible.Movement
{
    public class ItemPortal : ItemDrivenObject
    {
        [SerializeField] private Room roomMoveTo;

        protected override void OnClick()
        {
            GameplayController.Instance.EnterRoom(roomMoveTo);
        }
    }
}
