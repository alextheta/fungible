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
        _audioSource.clip = clip;
        _audioSource.Play();
    }
}