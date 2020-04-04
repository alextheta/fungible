using System.Collections;
using TMPro;
using UnityEngine;

namespace Fungible.Storytelling
{
    public class StoryLabelController : MonoBehaviour
    {
        public static StoryLabelController Instance;

        private AppearAnimationController animationController;
        private TextMeshProUGUI textMesh;

        public void Show()
        {
            StartCoroutine(ShowLabelCoroutine());
        }

        public void SetText(string text)
        {
            textMesh.text = text;
        }
    
        private void Awake()
        {
            Instance = this;
            animationController = GetComponent<AppearAnimationController>();
            textMesh = GetComponent<TextMeshProUGUI>();
        }

        private IEnumerator ShowLabelCoroutine()
        {
            yield return null; /* Skip frame for animator purpose */
            animationController.SetVisible();
            yield return null; /* Skip frame for animator purpose */
            yield return StartCoroutine(animationController.SetInvisibleCoroutine());
        }
    }
}
