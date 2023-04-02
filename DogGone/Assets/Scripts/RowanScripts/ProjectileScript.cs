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

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>(); 
        if (animator == null) { Debug.Log("No animator is present on this projectile"); }
        else { animator.Play(animationName); } // initilizes the animation

        PlayerMovement temp = FindObjectOfType<PlayerMovement>();
        if (temp != null) {
            if (temp.isFacingRight) { direction = 1; }
            else { direction = -1; }
        }
    }

    private void FixedUpdate()
    {
        gameObject.transform.position = new Vector2(gameObject.transform.position.x + speed * (direction), gameObject.transform.position.y);
        Collider2D collision = Physics2D.OverlapCircle(gameObject.transform.position, detectRadius, groundLayer);
        if (collision != null) { Debug.Log("Fireball has been destroyed");  Destroy(gameObject); }
        Collider2D collision2 = Physics2D.OverlapCircle(gameObject.transform.position, detectRadius, hitLayer);
        if (collision2 != null) { Debug.Log("Enemy was hit with fireball"); }
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

}
