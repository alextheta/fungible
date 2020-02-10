using Fungible.Movement;
using UnityEngine;

namespace Fungible.UI
{
    public class BackButton : MonoBehaviour
    {
        public void OnClick()
        {
            if (ProxyControlsPanel.Instance.ControlsAllowed())
                MovementHistoryController.Instance.PopRoom();
        }
    }
}