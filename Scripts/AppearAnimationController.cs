using System.Collections;
using UnityEngine;

namespace Fungible
{
    [RequireComponent(typeof(Animator))]
    public class AppearAnimationController : MonoBehaviour
    {
        private const string VisibleStateName = "Appear";
        private const string InvisibleStateName = "Disappear";
        private const string InvisibleAnimatorState = "Invisible";

        private Animator _animator;
        private static readonly int VisibleValue = Animator.StringToHash("VisibleState");

        public void SetVisible()
        {
            _animator.SetBool(VisibleValue, true);
        }

        public void SetInvisible()
        {
            _animator.SetBool(VisibleValue, false);
        }

        public IEnumerator SetVisibleCoroutine()
        {
            SetVisible();

            while (!_animator.GetCurrentAnimatorStateInfo(0).IsName(VisibleStateName))
                yield return null;

            while (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
                yield return null;
        }

        public IEnumerator SetInvisibleCoroutine()
        {
            SetInvisible();

            while (!_animator.GetCurrentAnimatorStateInfo(0).IsName(InvisibleStateName))
                yield return null;

            while (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
                yield return null;
        }

        public void CompleteAnimationImmediate()
        {
            SetInvisible();
            _animator.Play(InvisibleAnimatorState, -1, 1.0f);
        }

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _animator.keepAnimatorControllerStateOnDisable = true;
        }
    }
}