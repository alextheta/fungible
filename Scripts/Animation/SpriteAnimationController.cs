using DG.Tweening;
using UnityEngine;

namespace Fungible.Animation
{
    public class SpriteAnimationController : MonoBehaviour
    {
        [SerializeField] private Color targetColor;
        [SerializeField] private float duration;
        private SpriteRenderer _spriteRenderer;
        private Color _hiddenColor;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _hiddenColor = targetColor;
            _hiddenColor.a = 0;
        }

        public Tween AppearTween()
        {
            return _spriteRenderer.DOColor(targetColor, duration);
        }

        public Tween DisappearTween()
        {
            return _spriteRenderer.DOColor(_hiddenColor, duration);
        }
    }
}