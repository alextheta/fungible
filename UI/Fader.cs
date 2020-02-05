using System.Collections;
using UnityEngine;

namespace Fungible.UI
{
    public class Fader : MonoBehaviour
    {
        public string fadeInStateName;
        public string fadeOutStateName;
        public static Fader Instance;

        private Animator animator;
        private static readonly int FadeTrigger = Animator.StringToHash("FadeTrigger");
        private static readonly int UnfadeTrigger = Animator.StringToHash("UnfadeTrigger");

        public void Fade()
        {
            animator.SetTrigger(FadeTrigger);
        }

        public void Unfade()
        {
            animator.ResetTrigger(FadeTrigger);
        }

        public IEnumerator FadeInCoroutine()
        {
            animator.ResetTrigger(UnfadeTrigger);
            animator.SetTrigger(FadeTrigger);

            ProxyControlsPanel.Instance.DisableControls();

            while (!animator.GetCurrentAnimatorStateInfo(0).IsName(fadeInStateName))
                yield return null;

            while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
                yield return null;
        }
        
        public IEnumerator FadeOutCoroutine()
        {
            animator.ResetTrigger(FadeTrigger);
            animator.SetTrigger(UnfadeTrigger);

            while (!animator.GetCurrentAnimatorStateInfo(0).IsName(fadeOutStateName))
                yield return null;

            while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
                yield return null;
            
            ProxyControlsPanel.Instance.EnableControls();
        }

        private void Awake()
        {
            Instance = this;
            animator = GetComponent<Animator>();
        }
    }
}
