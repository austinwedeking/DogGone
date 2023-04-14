using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeAffect : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(deathDelay());
    }

    private IEnumerator deathDelay()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
