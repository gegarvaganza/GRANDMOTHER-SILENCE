using System.Collections;
using UnityEngine;

public class GhostLightFlicker : MonoBehaviour
{
    [Header("Light")]
    public Light targetLight;

    [Header("Flicker Settings")]
    public float minFlickerTime = 0.03f;
    public float maxFlickerTime = 0.08f;

    private Coroutine flickerRoutine;

    private void Start()
    {
        // Light OFF by default
        targetLight.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Ghost"))
            return;

        // Start flickering
        if (flickerRoutine == null)
        {
            flickerRoutine = StartCoroutine(FlickerRoutine());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Ghost"))
            return;

        // Stop flicker
        if (flickerRoutine != null)
        {
            StopCoroutine(flickerRoutine);
            flickerRoutine = null;
        }

        // Force light OFF
        targetLight.enabled = false;
    }

    IEnumerator FlickerRoutine()
    {
        while (true)
        {
            targetLight.enabled = !targetLight.enabled;

            yield return new WaitForSeconds(
                Random.Range(minFlickerTime, maxFlickerTime)
            );
        }
    }
}