using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashAffectScript : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(Die());
    }

    private IEnumerator Die()
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }
}
