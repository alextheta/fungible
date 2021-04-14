using Fungible.Events;
using UnityEngine;

namespace Fungible.Environment
{
    public class SoundEventListener : BaseEventListener
    {
        [SerializeField] private AudioClip _clip;

        public override void Event()
        {
            EnvironmentSoundController.Instance?.PlayClip(_clip);
        }
    }
}