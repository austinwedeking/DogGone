using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField] private int bones;

    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;

    public void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        currentHealth = maxHealth;
    }

    public void takeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log(currentHealth);
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            Debug.Log("Player has died");
        }
    }
}
