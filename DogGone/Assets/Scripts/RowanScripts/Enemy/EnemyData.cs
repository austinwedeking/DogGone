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

    private LevelChange levelChange;
    private Rigidbody2D rigid;
    private SpriteRenderer renderer;
    private BaseAI baseAI;
    private int damage = 5; public int getDamage() { return damage; }

    private void Start()
    {
        currentHealth = maxHealth;
        levelChange = FindObjectOfType<LevelChange>();
        if (levelChange == null) { Debug.LogError("No Level change present in the current scene"); }
        else { levelChange.IncrementEnemies(); }
        rigid = gameObject.GetComponent<Rigidbody2D>();
        if (rigid == null) { Debug.Log("No rigidbody on this enemy"); }
        renderer = gameObject.GetComponent<SpriteRenderer>();
        if (renderer == null) { Debug.Log("No renderer (somehow)"); }
        baseAI = GetComponent<BaseAI>();
    }

    public void takeDamage(int damage, float horiz, float vert)
    {
        currentHealth -= damage;
        StartCoroutine(damageFlash());
        rigid.AddForce(new Vector2(horiz, vert/2), ForceMode2D.Impulse);
        Debug.Log($"enemy took {maxHealth - currentHealth} damage");
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private IEnumerator damageFlash()
    {
        renderer.color = Color.red;
        yield return new WaitForSeconds(0.15f);
        renderer.color = Color.white;
    }

    public void Die()
    {
        Debug.Log("Enemy Died");
        levelChange.DecrementEnemies();
        baseAI.Die();
    }
}
