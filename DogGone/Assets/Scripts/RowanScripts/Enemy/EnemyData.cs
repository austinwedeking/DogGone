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

    private int damage = 5; public int getDamage() { return damage; }

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void takeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log($"enemy took {maxHealth - currentHealth} damage");
        if (currentHealth <= 0)
        {
            Debug.Log("Enemy Died");
            Destroy(gameObject);
        }
    }

    public void die()
    {

    }
}
