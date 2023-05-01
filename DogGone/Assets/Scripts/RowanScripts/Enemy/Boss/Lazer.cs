using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : MonoBehaviour
{
    [SerializeField] private float lifespan;
    [SerializeField] int damage;
    private SpriteRenderer rend;

    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        StartCoroutine(Destroy());
    }

    public void Flip()
    {
        rend.flipX = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerData temp;
        temp = collision.GetComponent<PlayerData>();
        if (temp != null)
        {
            temp.takeDamage(damage, 10f, 5f);
        }
    }

    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(lifespan);
        Destroy(gameObject);
    }
}
