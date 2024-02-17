using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class UiAudioController : MonoBehaviour
{

    [SerializeField] private AudioClip buttonAudio;

    private AudioSource audioSource;

    private AudioSource AudioSource => audioSource == null 
    ? audioSource = GetComponent<AudioSource>()
    : audioSource;



    public void PlayButtonAudio()
    {
        AudioSource.PlayAudioCue(buttonAudio);
    }

}
