using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerData : MonoBehaviour
{
    [SerializeField] private int bones;

    [SerializeField] private int maxHealth; public int GetMaxPlayerHealth() { return maxHealth; }
    [SerializeField] private int currentHealth; public int GetCurrentPlayerHealth() { return currentHealth; }

    private LevelChange levelChange;
    private SpriteRenderer renderer;
    private Rigidbody2D rigid;

    public HealthBar healthBar;

    public void Start()
    {
        gameObject.transform.position = new Vector3(-32.5f, -1, 0);

        levelChange = FindObjectOfType<LevelChange>();
        if (levelChange == null) { Debug.LogError("bad"); }

        healthBar = FindObjectOfType<HealthBar>();
        if (healthBar == null) { Debug.LogError("bad"); }

        renderer = gameObject.GetComponent<SpriteRenderer>();
        if (renderer == null) { Debug.Log("No sprite renderer (somehow)"); }

        rigid = gameObject.GetComponent<Rigidbody2D>();
        if (rigid == null) { Debug.Log("No rigidbody (somehow)"); }

        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            currentHealth = maxHealth;
            healthBar.SetMaxHealth(currentHealth);
        }
        else
        {
            Debug.Log("hi");
            currentHealth = levelChange.GetTemp();
            healthBar.SetHealth(currentHealth);
        }
    }

    public void takeDamage(int damage, float horiz, float vert)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        StartCoroutine(damageFlash());
        rigid.AddForce(new Vector2(0, vert), ForceMode2D.Impulse);
        rigid.AddForce(new Vector2(horiz, 0), ForceMode2D.Impulse);
        FindObjectOfType<AudioManager>().Play("DogHurt");
        Debug.Log(currentHealth);
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
        Debug.Log("Player has died");
        levelChange.SetTemp(100);
        levelChange.GameOver();
    }
}
