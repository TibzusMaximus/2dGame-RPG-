using UnityEngine;

public class AStepsOrBlock : MonoBehaviour
{
    [Header("Звуки")]
    [SerializeField] private AudioClip _step;
    [SerializeField] private AudioClip _block;

    private AudioSource _audioSource;
    private bool isWalk = false;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    public void SoundWalkStart()
    {
        if (!isWalk)
        {
            _audioSource.clip = _step;
            _audioSource.loop = true;
            _audioSource.pitch = 0.85f;
            _audioSource.volume = 0.5f;
            _audioSource.Play();
            isWalk = true;
        }
    }
    public void SoundWalkStop()
    {
        if (isWalk)
        {
            _audioSource.Stop();
            _audioSource.clip = null;
            _audioSource.loop = false;
            isWalk = false;
        }
    }
    public void SoundBlockStart()
    {
        _audioSource.clip = _block;
        _audioSource.volume = 0.2f;
        _audioSource.Play();
    }
}
