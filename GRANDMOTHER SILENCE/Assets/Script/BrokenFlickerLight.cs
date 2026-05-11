using UnityEngine;
using System.Collections;

public class BrokenFlickerLight : MonoBehaviour
{
    [Header("Light")]
    public Light flickerLight;

    [Header("Emission")]
    public Renderer lampRenderer;
    private Material lampMaterial;

    [Header("Audio")]
    public AudioClip normalHum;
    public AudioClip flickerSound;
    public AudioClip bulbBreakSound;

    private AudioSource audioSource;

    [Header("Before Flicker")]
    public float normalLightTime = 3f;

    [Header("Flicker Settings")]
    public float flickerDuration = 2f;
    public float minOffTime = 0.05f;
    public float maxOffTime = 0.2f;

    void Start()
    {
        if (flickerLight == null)
            flickerLight = GetComponent<Light>();

        if (lampRenderer != null)
            lampMaterial = lampRenderer.material;

        audioSource = GetComponent<AudioSource>();

        StartCoroutine(BreakLight());
    }

    IEnumerator BreakLight()
    {
        flickerLight.enabled = true;
        SetEmission(true);

        if (normalHum != null)
        {
            audioSource.clip = normalHum;
            audioSource.loop = true;
            audioSource.Play();
        }

        yield return new WaitForSeconds(normalLightTime);

        audioSource.Stop();

        if (flickerSound != null)
        {
            audioSource.clip = flickerSound;
            audioSource.loop = true;
            audioSource.Play();
        }

        float timer = 0f;

        while (timer < flickerDuration)
        {
            flickerLight.enabled = false;
            SetEmission(false);

            yield return new WaitForSeconds(Random.Range(minOffTime, maxOffTime));

            flickerLight.enabled = true;
            SetEmission(true);

            yield return new WaitForSeconds(Random.Range(0.05f, 0.15f));

            timer += Time.deltaTime;
        }

        flickerLight.enabled = false;
        SetEmission(false);

        audioSource.Stop();

        if (bulbBreakSound != null)
            audioSource.PlayOneShot(bulbBreakSound);
    }

    void SetEmission(bool state)
    {
        if (lampMaterial == null)
            return;

        if (state)
            lampMaterial.EnableKeyword("_EMISSION");
        else
            lampMaterial.DisableKeyword("_EMISSION");
    }
}