using Fungible.Movement;
using UnityEngine;

namespace Fungible.UI
{
    public class BackButton : MonoBehaviour
    {
        public void OnClick()
        {
            InvokeBack();
        }

        private void Update()
        {
            if (Application.platform == RuntimePlatform.Android && Input.GetKeyDown(KeyCode.Escape))
                InvokeBack();
        }

        private static void InvokeBack()
        {
            if (ProxyControlsPanel.Instance.ControlsAllowed())
                MovementHistoryController.Instance.PopRoom();
        }
    }
}