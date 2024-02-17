using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerAudio : MonoBehaviour
{
    [SerializeField] private AudioClip flapAudioClip;

    private AudioSource audioSource;

    private AudioSource AudioSource => audioSource == null 
    ? audioSource = GetComponent<AudioSource>()
    : audioSource;

    public void PlayFlapAudio()
    {
        AudioSource.PlayAudioCue(flapAudioClip);
    }

}
