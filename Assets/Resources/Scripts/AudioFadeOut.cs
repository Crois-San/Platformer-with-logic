using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFadeOut : MonoBehaviour
{

    private static IEnumerator FadeOut (AudioSource audioSource, float FadeTime)
    {
        float startVolume = audioSource.volume;
 
        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;
 
            yield return null;
        }
 
        audioSource.Pause();
        audioSource.volume = startVolume;
    }

    public void FadeOutStart(AudioSource audioSource, float fadeTime)
    {
        StartCoroutine (FadeOut(audioSource, fadeTime));
    }

    public void FadeOutStop(AudioSource audioSource, float fadeTime)
    {
        StopCoroutine (FadeOut(audioSource, fadeTime));
    }
 
}
