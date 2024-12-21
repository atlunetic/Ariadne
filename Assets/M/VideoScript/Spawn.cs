using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class Spawn : MonoBehaviour
{
    public GameObject objectToActivate;  // The object to activate
    public float delayTime = 30f;         // Delay before activation (default is 2 seconds)

    [YarnCommand("DelaySpawn")]
    public void SpawnAfterDelay()
    {
        // Start the activation coroutine
        StartCoroutine(ActivateAfterDelay());
    }

    IEnumerator ActivateAfterDelay()
    {
        // Wait for the specified delay time
        yield return new WaitForSeconds(delayTime);

        // Activate the object
        objectToActivate.SetActive(true);
    }

}
