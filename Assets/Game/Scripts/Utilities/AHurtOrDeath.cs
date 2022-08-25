using UnityEngine;

public class AHurtOrDeath : MonoBehaviour
{
    [Header("Звуки")]
    [SerializeField] private AudioClip _hurt;

    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    public void SoundHurtStart(int objectType)
    {//0 - игрок, 1 - враг
        _audioSource.clip = _hurt;
        if (objectType == 0) _audioSource.volume = 0.2f;
        if (objectType == 1) _audioSource.volume = 1;
        _audioSource.Play();
    }
}
