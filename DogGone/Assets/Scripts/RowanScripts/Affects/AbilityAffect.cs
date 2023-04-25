using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityAffect : MonoBehaviour
{
    [SerializeField] private float deathDelayTime;

    void Start()
    {
        StartCoroutine(deathDelay());
    }

    private IEnumerator deathDelay()
    {
        yield return new WaitForSeconds(deathDelayTime);
        Destroy(gameObject);
    }
}
