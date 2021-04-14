using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Fungible.Animation
{
    public class ImageAnimationController : MonoBehaviour
    {
        [SerializeField] private Color targetColor;
        [SerializeField] private float duration;
        private Image _image;

        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        public Tween AppearTween()
        {
            return _image.DOColor(targetColor, duration);
        }

        public Tween DisappearTween()
        {
            return _image.DOColor(Color.clear, duration);
        }
    }
}