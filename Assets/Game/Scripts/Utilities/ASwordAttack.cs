using UnityEngine;

public class ASwordAttack : MonoBehaviour
{
    [Header("Звуки")]
    [SerializeField] private AudioClip _sword;

    private AudioSource _audioSource;
    private bool isSword = false;
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    public void SoundSwordStart()
    {
        if (!isSword)
        {
            _audioSource.clip = _sword;
            _audioSource.Play();
            isSword = true;
        }
    }
    public void SoundSwordStop()
    {
        isSword = false;
        _audioSource.loop = false;
    }
}
