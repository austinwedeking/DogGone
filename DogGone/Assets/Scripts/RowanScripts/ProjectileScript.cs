using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    private Animator animator;

    [SerializeField] private int damage;
    [SerializeField] private float speed;
    [SerializeField] private string animationName;
    [SerializeField] private string tagToDamage;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask hitLayer;
    [SerializeField] private float detectRadius;

    private int direction;
    private GameObject owner;
    private PlayerMovement playerMovement;
    private PlayerData data;
    private EnemyData data2;

    [Space(10)]
    [Header("Projectile Options")]
    [SerializeField] private bool firedFromPlayer;

    private void Start()
    {
        FindObjectOfType<AudioManager>().Play("FireCast");

        playerMovement = FindObjectOfType<PlayerMovement>();

        animator = gameObject.GetComponent<Animator>(); 
        if (animator == null) { Debug.Log("No animator is present on this projectile"); }
        // else { animator.Play(animationName); } // initilizes the animation

        if (firedFromPlayer)
        {
            PlayerMovement temp = FindObjectOfType<PlayerMovement>();
            if (temp != null)
            {
                if (temp.isFacingRight) { direction = 1; }
                else { direction = -1; }
            }
        } else
        {
            if (owner != null)
            {
                if (owner.GetComponent<SnakeAI>().IsFacingRight()) { direction = 1; }
                else { direction = -1; } // instaid look for BaseAI and then get costom AI via function
            }
        }
    }

    private void FixedUpdate()
    {
        gameObject.transform.position = new Vector2(gameObject.transform.position.x + speed * (direction), gameObject.transform.position.y);
        Collider2D collision = Physics2D.OverlapCircle(gameObject.transform.position, detectRadius, groundLayer);
        if (collision != null) { Debug.Log("Fireball has been destroyed");  Destroy(gameObject); }
        Collider2D collision2 = Physics2D.OverlapCircle(gameObject.transform.position, detectRadius, hitLayer);
        if (collision2 != null) {

            Debug.Log($"A collision has happened with {collision2.tag}");

            if (collision2.tag == "Player")
            {
                data = collision2.GetComponent<PlayerData>();
                if (data != null)
                {
                    Debug.Log("The player was hit by the fireball");
                    data.takeDamage(damage, 30 * direction, 4);
                    Destroy(gameObject);
                }
            }
            else if (collision2.tag == "Enemy")
            {
                data2 = collision2.GetComponent<EnemyData>();
                if (data2 != null)
                {
                    float direction;
                    if (playerMovement.isFacingRight) { direction = 1; } else { direction = -1; }
                    Debug.Log("The enemy was hit by the fireball");
                    data2.takeDamage(damage, 8f * direction, 5f);
                    Destroy(gameObject);
                }
            }
            //if (data != null)
            //{
            //    Debug.Log("take damage is being called");
            //    data.takeDamage(damage);
            //}
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Collision");
        //Debug.Log(collision.tag);
        //if (collision.tag == tagToDamage)
        //{
        //    if (tagToDamage == "Enemy") // damage enemy
        //    {
        //        collision.gameObject.GetComponent<EnemyData>().takeDamage(damage);
        //        Destroy(gameObject);
        //    }
        //    else if (tagToDamage == "Player") // damage player
        //    {
        //        collision.gameObject.GetComponent<PlayerData>().takeDamage(damage);
        //        Destroy(gameObject);
        //    }
        //}

        //if (collision.tag == "Ground") { Destroy(gameObject); }
    }

    public void SetOwner(GameObject obj)
    {
        owner = obj;
    }
}
