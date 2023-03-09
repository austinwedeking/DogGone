using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField] private int bones;

    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;

    private LevelChange gameManager;

    public void Start()
    {
        currentHealth = maxHealth;
        gameManager = FindObjectOfType<LevelChange>();
        if (gameManager == null) { Debug.LogError("There is no level change on the game manager in the current scene"); }
    }

    public void takeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log(currentHealth);
        if (currentHealth <= 0)
        {
            Debug.Log("Player has died");
        }
    }

    private void Die()
    {
        Debug.Log("Player has died");
        gameManager.Win();
    }
}
