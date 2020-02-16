using System.Collections;
using Fungible.Movement;
using Fungible.UI;
using TMPro;
using UnityEngine;

namespace Fungible
{
    public class GameplayController : MonoBehaviour
    {
        public Map map;
        public GameObject textStoryLabelObject;
        public InOutAnimationController faderAnimationController;

        public static GameplayController Instance;
        
        private TextMeshProUGUI textStoryLabel;
        private InOutAnimationController textStoryLabelAnimationController;

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

            yield return StartCoroutine(faderAnimationController.FadeInCoroutine());
            
            textStoryLabel.text = null;
            map.ChangeRoom(room);
            MovementHistoryController.Instance.UpdateBackButton();
            textStoryLabelAnimationController.Fade();

            yield return StartCoroutine(faderAnimationController.FadeOutCoroutine());
            
            ProxyControlsPanel.Instance.EnableControls();
            textStoryLabelAnimationController.Unfade();
        }

        private void Awake()
        {
            Instance = this;
            textStoryLabel = textStoryLabelObject.GetComponent<TextMeshProUGUI>();
            textStoryLabelAnimationController = textStoryLabelObject.GetComponent<InOutAnimationController>();
        }
    }
}
