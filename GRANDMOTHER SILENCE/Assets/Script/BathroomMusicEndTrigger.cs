using System.Collections;
using UnityEngine;

public class BathroomMusicTrigger : MonoBehaviour
{
    public AudioSource backgroundMusic;

    public float fadeSpeed = 1f;
    public float minVolume = 0.05f; // lowest volume (not 0)

    private bool triggered;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered) return;
        if (!other.CompareTag("Player")) return;

        triggered = true;

        Debug.Log("Player entered bathroom - lowering music");

        if (backgroundMusic != null)
        {
            StartCoroutine(FadeToLowVolume());
        }
    }

    private IEnumerator FadeToLowVolume()
    {
        while (backgroundMusic.volume > minVolume)
        {
            backgroundMusic.volume -= Time.deltaTime * fadeSpeed;
            yield return null;
        }

        backgroundMusic.volume = minVolume;

        Debug.Log("Music now at low volume");
    }
}