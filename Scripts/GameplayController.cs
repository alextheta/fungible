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
        public AppearAnimationController faderAnimationController;

        public static GameplayController Instance;
        
        private TextMeshProUGUI textStoryLabel;
        //private AppearAnimationController textStoryLabelAnimationController;

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
            
            textStoryLabel.text = null;
            map.ChangeRoom(room);
            MovementHistoryController.Instance.UpdateBackButton();
            //textStoryLabelAnimationController.SetVisible();

            yield return StartCoroutine(faderAnimationController.SetInvisibleCoroutine());
            
            ProxyControlsPanel.Instance.EnableControls();
            //textStoryLabelAnimationController.SetInvisible();
        }

        private void Awake()
        {
            Instance = this;
            textStoryLabel = textStoryLabelObject.GetComponent<TextMeshProUGUI>();
            //textStoryLabelAnimationController = textStoryLabelObject.GetComponent<AppearAnimationController>();
        }
    }
}
