using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class GameModeAudioController : MonoBehaviour
{
    [SerializeField] private AudioClip gameOverAudio;

    private AudioSource audioSource;

    private AudioSource AudioSource => audioSource == null 
    ? audioSource = GetComponent<AudioSource>()
    : audioSource;

    public void PlayGameOverAudio()
    {
        AudioSource.PlayAudioCue(gameOverAudio);

    }

}
