using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerData : MonoBehaviour
{
    [SerializeField] private int bones; public int GetBonesAmount() { return bones; } public void SetBonesAmount(int i) { bones = i; }
    private Text bonesTextRefference;

    [SerializeField] private int maxHealth = 100; public int GetMaxPlayerHealth() { return maxHealth; }
    [SerializeField] private int currentHealth = 100; public int GetCurrentPlayerHealth() { return currentHealth; } public void SetCurrentPlayerHealth(int i) { currentHealth = i; }

    private LevelChange levelChange;
    private SpriteRenderer renderer;
    private Rigidbody2D rigid;
    private PlayerMovement playerMovement;

    public HealthBar healthBar;

    public void Start()
    {
        bonesTextRefference = GameObject.Find("BonesAmount").GetComponent<Text>();

        //AquireBones(0);

        gameObject.transform.position = new Vector3(-32.5f, -1, 0);

        levelChange = FindObjectOfType<LevelChange>();
        if (levelChange == null) { Debug.LogError("bad"); }

        healthBar = FindObjectOfType<HealthBar>();
        if (healthBar == null) { Debug.LogError("bad"); }

        renderer = gameObject.GetComponent<SpriteRenderer>();
        if (renderer == null) { Debug.Log("No sprite renderer (somehow)"); }

        rigid = gameObject.GetComponent<Rigidbody2D>();
        if (rigid == null) { Debug.Log("No rigidbody (somehow)"); }

        playerMovement = gameObject.GetComponent<PlayerMovement>();

        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            currentHealth = maxHealth;
            healthBar.SetMaxHealth(currentHealth);
            levelChange.SetTemp(maxHealth);

            bones = 0;
            AquireBones(bones);
            levelChange.SetTempBones(bones);
        }

        Debug.Log("health = " + currentHealth);
        currentHealth = levelChange.GetTemp();
        Debug.Log("health = " + currentHealth);
        healthBar.SetHealth(currentHealth);

        bones = levelChange.GetTempBones();
    }

    public void takeDamage(int damage, float horiz, float vert)
    {
        if (!playerMovement.GetIsDashing())
        {
            currentHealth -= damage;
            SetCurrentPlayerHealth(currentHealth);
            levelChange.SetTemp(currentHealth);
            Debug.Log("after damage temp = " + levelChange.GetTemp());
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
    }

    private IEnumerator damageFlash()
    {
        renderer.color = Color.red;
        yield return new WaitForSeconds(0.15f);
        renderer.color = Color.white;
    }

    public void AquireBones(int numBones)
    {
        bones += numBones;
        SetBonesAmount(bones);
        levelChange.SetTempBones(bones);
        bonesTextRefference.text = bones.ToString();
    }

    public int GetBones()
    {
        return bones;
    }

    public void Die()
    {
        Debug.Log("Player has died");
        levelChange.SetTemp(100);
        levelChange.GameOver();
    }
}
