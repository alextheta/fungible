using System.Collections;
using TMPro;
using UnityEngine;

namespace Fungible.Storytelling
{
    public class StoryLabelController : MonoBehaviour
    {
        public static StoryLabelController Instance;

        private AppearAnimationController _animationController;
        private TextMeshProUGUI _textMesh;

        public void Show()
        {
            StartCoroutine(ShowLabelCoroutine());
        }

        public void SetText(string text)
        {
            _textMesh.text = text;
        }

        public void FinishAnimation()
        {
            _animationController.CompleteAnimationImmediate();
        }
        
        private void Awake()
        {
            Instance = this;
            _animationController = GetComponent<AppearAnimationController>();
            _textMesh = GetComponent<TextMeshProUGUI>();
        }

        private IEnumerator ShowLabelCoroutine()
        {
            yield return null; /* Skip frame for animator purpose */
            _animationController.SetVisible();
            yield return null; /* Skip frame for animator purpose */
            yield return StartCoroutine(_animationController.SetInvisibleCoroutine());
        }
    }
}