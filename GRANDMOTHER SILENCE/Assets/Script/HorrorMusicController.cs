using UnityEngine;

public class HorrorMusicController : MonoBehaviour
{
    public AudioSource tensionMusic;
    public float fadeSpeed = 0.5f;
    public float targetVolume = 0.4f;

    private bool active;

    public void ActivateHorror()
    {
        if (active) return;

        active = true;

        tensionMusic.Play();
        tensionMusic.volume = 0f;

        Debug.Log("Horror music activated");
    }

    void Update()
    {
        if (!active) return;

        tensionMusic.volume = Mathf.MoveTowards(
            tensionMusic.volume,
            targetVolume,
            Time.deltaTime * fadeSpeed
        );
    }
}