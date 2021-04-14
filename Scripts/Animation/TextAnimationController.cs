using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Fungible.Animation
{
    public class TextAnimationController : MonoBehaviour
    {
        [SerializeField] private Color targetColor;
        [SerializeField] private float appearDuration;
        [SerializeField] private float disappearDuration;
        private TMP_Text _text;

        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
        }

        public Tween AppearTween()
        {
            return _text.DOColor(targetColor, appearDuration);
        }

        public Tween DisappearTween()
        {
            return _text.DOColor(Color.clear, disappearDuration);
        }
    }
}