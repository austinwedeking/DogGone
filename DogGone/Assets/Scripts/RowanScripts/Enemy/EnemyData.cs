using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData : MonoBehaviour
{
    [Header("Coroutine Timers")]
    public float attackTimer;
    public float wanderTimer;
    public float chaseTimer;

    [Header("Enemy Health")]
    [SerializeField] private int maxHealth;
    [Tooltip("Keep this at zero")] [SerializeField] private int currentHealth;

    private LevelChange gameManager;
    private int damage = 5; public int getDamage() { return damage; }

    private void Start()
    {
        currentHealth = maxHealth;
        gameManager = FindObjectOfType<LevelChange>();
        if (gameManager == null) { Debug.LogError("There is no level change on the game manager in the current scene"); }
        else { gameManager.IncrementEnemies(); }
    }

    public void takeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log($"enemy took {maxHealth - currentHealth} damage");
        if (currentHealth <= 0)
        {
            die();
        }
    }

    public void die()
    {
        Debug.Log("Enemy Died");
        gameManager.DecrementEnemies();
        Destroy(gameObject);
    }
}
