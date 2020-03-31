using System.Collections;
using Fungible.Movement;
using Fungible.UI;
using UnityEngine;

namespace Fungible
{
    public class GameplayController : MonoBehaviour
    {
        public Map map;
        public AppearAnimationController faderAnimationController;

        public static GameplayController Instance;

        public void EnterRoom(Room room)
        {
            MovementHistoryController.Instance.AddPreviousRoom(map.GetCurrentRoom());
            ChangeRoom(room);
        }
        
        public void ChangeRoom(Room room)
        {
            StartCoroutine(RoomTransitionCoroutine(room));
        }
        
        private IEnumerator RoomTransitionCoroutine(Room room)
        {
            ProxyControlsPanel.Instance.DisableControls();

            yield return StartCoroutine(faderAnimationController.SetVisibleCoroutine());

            map.ChangeRoom(room);
            MovementHistoryController.Instance.UpdateBackButton();

            yield return StartCoroutine(faderAnimationController.SetInvisibleCoroutine());
            
            ProxyControlsPanel.Instance.EnableControls();
        }

        private void Awake()
        {
            Instance = this;
        }
    }
}
