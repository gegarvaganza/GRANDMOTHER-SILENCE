using System.Collections;
using UnityEngine;

public class RestoreMusicAfterKey : MonoBehaviour
{
    public AudioSource backgroundMusic;

    public float fadeSpeed = 1f;
    public float targetVolume = 0.3f;

    private bool triggered;

    public void RestoreMusic()
    {
        if (triggered) return;

        triggered = true;

        Debug.Log("Restoring music after key pickup");

        StartCoroutine(FadeInMusic());
    }

    private IEnumerator FadeInMusic()
    {
        if (!backgroundMusic.isPlaying)
            backgroundMusic.Play();

        while (backgroundMusic.volume < targetVolume)
        {
            backgroundMusic.volume += Time.deltaTime * fadeSpeed;

            yield return null;
        }

        backgroundMusic.volume = targetVolume;
    }
}