using UnityEngine;
using System.Collections;

public class GhostLightTrigger : MonoBehaviour
{
    public Light targetLight;

    public float flickerDuration = 2f;
    public float minDelay = 0.05f;
    public float maxDelay = 0.2f;

    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ghost") && !triggered)
        {
            triggered = true;
            StartCoroutine(FlickerAndDie());
        }
    }

    IEnumerator FlickerAndDie()
    {
        float timer = 0f;

        while (timer < flickerDuration)
        {
            targetLight.enabled = false;
            yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));

            targetLight.enabled = true;
            yield return new WaitForSeconds(Random.Range(0.05f, 0.15f));

            timer += Time.deltaTime;
        }

        // LIGHT DIES FOREVER
        targetLight.enabled = false;
    }
}