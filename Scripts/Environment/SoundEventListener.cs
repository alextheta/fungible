using Fungible;
using Fungible.Events;
using UnityEngine;

public class SoundEventListener : BaseEventListener
{
    [SerializeField] private AudioClip _clip;

    public override void Event()
    {
        if (!SaveController.LoadState)
        {
            EnvironmentSoundController.Instance.PlayClip(_clip);
        }
    }
}