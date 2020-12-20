using Fungible;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class EnvironmentSoundController : MonoBehaviour
{
    private AudioSource _audioSource;
    public static EnvironmentSoundController Instance;

    private void Awake()
    {
        Instance = this;
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayClip(AudioClip clip)
    {
        if (!SaveController.LoadState && _audioSource.enabled)
        {
            _audioSource.clip = clip;
            _audioSource.Play();
        }
    }
}