using UnityEngine;

public static class AudioUtility
{



    public static void PlayAudioCue(this AudioSource audioSource, AudioClip clip)
    {
        if(audioSource.outputAudioMixerGroup != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
            return;
        }
        Debug.LogError("ERROR: Every AudioSource need the AudioMixer Assigned");

    }

    
}
