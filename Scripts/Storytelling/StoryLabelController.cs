using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Fungible.Storytelling
{
    public class StoryLabelController : MonoBehaviour
    {
        public static StoryLabelController Instance;

        private TextAnimationController _textAnimationController;
        private TextMeshProUGUI _textMesh;
        private Sequence _sequence;

        public void SetText(string text)
        {
            _textMesh.text = text;
        }

        public void Show()
        {
            _sequence.Restart();
            _sequence.Play();
        }

        public void FastForward()
        {
            _sequence.Complete();
        }

        private void Awake()
        {
            Instance = this;
            _textAnimationController = GetComponent<TextAnimationController>();
            _textMesh = GetComponent<TextMeshProUGUI>();
        }

        private void Start()
        {
            _sequence = DOTween.Sequence();
            _sequence.Append(_textAnimationController.AppearTween());
            _sequence.Append(_textAnimationController.DisappearTween());
            _sequence.SetAutoKill(false);
        }
    }
}