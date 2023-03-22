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
    [SerializeField] private int currentHealth;

    private LevelChange levelChange;

    private int damage = 5; public int getDamage() { return damage; }

    private void Start()
    {
        currentHealth = maxHealth;
        levelChange = FindObjectOfType<LevelChange>();
        if (levelChange == null) { Debug.LogError("No Level change present in the current scene"); }
        else { levelChange.IncrementEnemies(); }
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
        levelChange.DecrementEnemies();
        Destroy(gameObject);
    }
}
