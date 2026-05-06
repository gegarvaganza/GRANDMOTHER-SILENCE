using System.Collections;
using UnityEngine;

public class AudioDripTrigger : MonoBehaviour
{
    public AudioSource bathroomDrip;
    public float delayBeforeStart = 2f;

    private bool triggered;
    private bool insideBathroom;

    public void OnFlashlightPicked()
    {
        if (triggered) return;

        triggered = true;

        Debug.Log("Flashlight picked - starting drip sequence");

        StartCoroutine(StartDrip());
    }

    private IEnumerator StartDrip()
    {
        yield return new WaitForSeconds(delayBeforeStart);

        if (bathroomDrip == null) yield break;

        bathroomDrip.Play();

        Debug.Log("Bathroom drip started");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (insideBathroom) return;

        insideBathroom = true;

        Debug.Log("Player entered bathroom - stopping drip");

        if (bathroomDrip != null)
        {
            bathroomDrip.Stop();
        }
    }
}