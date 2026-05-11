using UnityEngine;
using System.Collections;

public class FadeAudio : MonoBehaviour
{
    public AudioSource audioSource;

    public void FadeOut()
    {
        StartCoroutine(FadeOutCoroutine());
    }

    public void FadeIn()
    {
        StartCoroutine(FadeInCoroutine());
    }

    IEnumerator FadeOutCoroutine()
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / 3f;
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }

    IEnumerator FadeInCoroutine()
    {
        float targetVolume = audioSource.volume;

        audioSource.volume = 0;
        audioSource.Play();

        while (audioSource.volume < targetVolume)
        {
            audioSource.volume += targetVolume * Time.deltaTime / 3f;
            yield return null;
        }

        audioSource.volume = targetVolume;
    }
}