using UnityEngine;

namespace Fungible.UI
{
    [RequireComponent(typeof(InOutAnimationController))]
    public class Fader : MonoBehaviour
    {
        public static Fader Instance;

        private void Awake()
        {
            Instance = this;
        }
    }
}
