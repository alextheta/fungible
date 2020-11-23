using System.Collections;
using UnityEngine;

namespace Fungible
{
    [RequireComponent(typeof(Animator))]
    public class AppearAnimationController : MonoBehaviour
    {
        private const string VisibleStateName = "Appear";
        private const string InvisibleStateName = "Disappear";

        private Animator _animator;
        private static readonly int VisibleValue = Animator.StringToHash("VisibleState");

        public void SetVisibilityState(bool state)
        {
            if (!_animator)
            {
                InitAnimatedObject();
            }

            _animator.SetBool(VisibleValue, state);
        }

        public IEnumerator SetVisibleCoroutine()
        {
            yield return TransitionCoroutine(true, VisibleStateName);
        }

        public IEnumerator SetInvisibleCoroutine()
        {
            yield return TransitionCoroutine(false, InvisibleStateName);
        }

        public void DisappearImmediate()
        {
            AnimateImmidiate(InvisibleStateName);
        }

        public void AppearImmediate()
        {
            AnimateImmidiate(VisibleStateName);
        }

        private void Awake()
        {
            InitAnimator();
        }

        private void AnimateImmidiate(string stateName)
        {
            _animator.Play(stateName, -1, 1.0f);
        }

        private IEnumerator TransitionCoroutine(bool state, string stateName)
        {
            SetVisibilityState(state);

            if (!gameObject.activeInHierarchy)
            {
                AnimateImmidiate(stateName);
                yield break;
            }

            while (!_animator.GetCurrentAnimatorStateInfo(0).IsName(stateName))
            {
                yield return null;
            }

            while (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
            {
                yield return null;
            }
        }

        private void InitAnimator()
        {
            _animator = GetComponent<Animator>();
            _animator.keepAnimatorControllerStateOnDisable = true;
        }

        private void InitAnimatedObject()
        {
            var parent = transform.parent;
            var active = gameObject.activeSelf;

            transform.parent = null;
            gameObject.SetActive(true);

            InitAnimator();

            transform.parent = parent;
            gameObject.SetActive(active);
        }
    }
}