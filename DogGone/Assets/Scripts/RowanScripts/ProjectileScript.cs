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

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>(); 
        if (animator == null) { Debug.Log("No animator is present on this projectile"); }
        else { animator.Play(animationName); } // initilizes the animation
    }

    private void FixedUpdate()
    {
        gameObject.transform.position = new Vector2(gameObject.transform.position.x + speed, gameObject.transform.position.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.tag);
        if (collision.tag == tagToDamage)
        {
            if (tagToDamage == "Enemy") // damage enemy
            {
                collision.gameObject.GetComponent<EnemyData>().takeDamage(damage);
                Destroy(gameObject);
            }
            else if (tagToDamage == "Player") // damage player
            {
                collision.gameObject.GetComponent<PlayerData>().takeDamage(damage);
                Destroy(gameObject);
            }
        }

        if (collision.tag == "Ground") { Destroy(gameObject); }
    }

}
