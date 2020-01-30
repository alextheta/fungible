using Fungible.Movement;
using UnityEngine;

namespace Fungible.UI
{
    public class BackButton : MonoBehaviour
    {
        public void OnClick()
        {
            MovementHistoryController.Instance.PopRoom();
        }
    }
}