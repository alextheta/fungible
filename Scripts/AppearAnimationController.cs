using System.Collections;
using UnityEngine;

namespace Fungible.UI
{
    [RequireComponent(typeof(Animator))]
    public class AppearAnimationController : MonoBehaviour
    {
        private const string VisibleStateName = "Appear";
        private const string InvisibleStateName = "Disappear";
        
        private Animator animator;
        private static readonly int VisibleValue = Animator.StringToHash("VisibleState");

        public void SetVisible()
        {
            animator.SetBool(VisibleValue, true);
        }

        public void SetInvisible()
        {
            animator.SetBool(VisibleValue, false);
        }

        public IEnumerator SetVisibleCoroutine()
        {
            SetVisible();

            while (!animator.GetCurrentAnimatorStateInfo(0).IsName(VisibleStateName))
                yield return null;

            while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
                yield return null;
        }
        
        public IEnumerator SetInvisibleCoroutine()
        {
            SetInvisible();
            
            while (!animator.GetCurrentAnimatorStateInfo(0).IsName(InvisibleStateName))
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
