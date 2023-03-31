using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField] private int bones;

    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;

    private LevelChange levelChange;

    public HealthBar healthBar;

    public void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        levelChange = FindObjectOfType<LevelChange>();
        if (levelChange == null) { Debug.LogError("bad"); }
    }

    public void takeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
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
