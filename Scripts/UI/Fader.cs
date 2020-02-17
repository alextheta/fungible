using UnityEngine;

namespace Fungible.UI
{
    [RequireComponent(typeof(AppearAnimationController))]
    public class Fader : MonoBehaviour
    {
        public static Fader Instance;

        private void Awake()
        {
            Instance = this;
        }
    }
}
