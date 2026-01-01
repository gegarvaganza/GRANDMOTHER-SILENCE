using UnityEngine;
using System.Collections;

public class BrokenLight : MonoBehaviour
{
    public Light flickerLight;
    public float minOffTime = 0.05f;
    public float maxOffTime = 0.3f;

    void Start()
    {
        if (flickerLight == null)
            flickerLight = GetComponent<Light>();

        StartCoroutine(Flicker());
    }

    IEnumerator Flicker()
    {
        while (true)
        {
            flickerLight.enabled = false;
            yield return new WaitForSeconds(Random.Range(minOffTime, maxOffTime));

            flickerLight.enabled = true;
            yield return new WaitForSeconds(Random.Range(0.05f, 0.2f));
        }
    }
}