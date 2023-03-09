using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField] private int bones;

    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;

    private LevelChange levelChange;

    public void Start()
    {
        currentHealth = maxHealth;
        levelChange = FindObjectOfType<LevelChange>();
        if (levelChange == null) { Debug.LogError("bad"); }
    }

    public void takeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log(currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("Player has died");
        levelChange.GameOver();
    }
}
