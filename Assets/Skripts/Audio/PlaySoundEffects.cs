using UnityEngine;

public class PlaySoundEffects : MonoBehaviour
{
    private AudioSource m_AudioSource;

    [SerializeField] private AudioClip popSound;
    [SerializeField] private AudioClip hitSound;

    private void Awake()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        ColliderInformer.wasCollided += PlayPopSound;
        ColliderInformer.wasDropped += PlayHitSound;
    }

    private void OnDisable()
    {
        ColliderInformer.wasCollided += PlayPopSound;
        ColliderInformer.wasDropped -= PlayHitSound;
    }

    private void PlayPopSound(int noneed)
    {
        m_AudioSource.PlayOneShot(popSound);
    }

    private void PlayHitSound()
    {
        m_AudioSource.PlayOneShot(hitSound);
    }
}
