using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(AudioSource))]
public class DeathTrigger : MonoBehaviour
{
    [SerializeField] private AudioClip deathSoundClip;
    private Collider2D deathTriggerCollider;
    private Collider2D DeathTriggerCollider => deathTriggerCollider == null 
    ? deathTriggerCollider = GetComponent<Collider2D>()
    : deathTriggerCollider;

    private AudioSource audioSource;
    private AudioSource AudioSource => audioSource == null ?
    audioSource = GetComponent<AudioSource>()
    : audioSource;

    void Start()
    {
        DeathTriggerCollider.isTrigger = true;

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.TryGetComponent(out PlayerController player))
        {
            if(!player.IsDied)
            {
                AudioSource.PlayAudioCue(deathSoundClip);
                player.Die();
            }

        }

    }
    
}
