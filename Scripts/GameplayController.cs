using DG.Tweening;
using Fungible.Animation;
using Fungible.Movement;
using Fungible.Storytelling;
using Fungible.UI;
using UnityEngine;

namespace Fungible
{
    public class GameplayController : MonoBehaviour
    {
        public Map map;
        public ImageAnimationController faderAnimationController;

        private Room _transitionRoom;

        public static GameplayController Instance;

        public void EnterRoom(Room room)
        {
            MovementHistoryController.Instance.AddPreviousRoom(map.GetCurrentRoom());
            ChangeRoom(room);
        }

        public void ChangeRoom(Room room)
        {
            _transitionRoom = room;
            ProxyControlsPanel.Instance?.DisableControls();
            Sequence faderSequence = DOTween.Sequence();
            faderSequence.Append(faderAnimationController.AppearTween().OnComplete(FaderAppearPhase));
            faderSequence.Append(faderAnimationController.DisappearTween().OnComplete(FaderDisappearPhase));
            faderSequence.Play();
        }

        private void FaderAppearPhase()
        {
            LightController.Instance.Restore();
            StoryLabelController.Instance.FastForward();
            map.ChangeRoom(_transitionRoom);
            MovementHistoryController.Instance.UpdateBackButton();
        }

        private void FaderDisappearPhase()
        {
            ProxyControlsPanel.Instance.EnableControls();
        }

        private void Awake()
        {
            Instance = this;
        }
    }
}