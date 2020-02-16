using System.Collections;
using UnityEngine;

namespace Fungible.UI
{
    public class InOutAnimationController : MonoBehaviour
    {
        public string fadeInStateName;
        public string fadeOutStateName;

        private Animator animator;
        private static readonly int FadeValue = Animator.StringToHash("InAnimation");

        public void Fade()
        {
            animator.SetBool(FadeValue, true);
        }

        public void Unfade()
        {
            animator.SetBool(FadeValue, false);
        }

        public IEnumerator FadeInCoroutine()
        {
            Fade();

            while (!animator.GetCurrentAnimatorStateInfo(0).IsName(fadeInStateName))
                yield return null;

            while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
                yield return null;
        }
        
        public IEnumerator FadeOutCoroutine()
        {
            Unfade();
            
            while (!animator.GetCurrentAnimatorStateInfo(0).IsName(fadeOutStateName))
                yield return null;

            while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
                yield return null;
        }
        
        private void Awake()
        {
            animator = GetComponent<Animator>();
        }
    }
}
