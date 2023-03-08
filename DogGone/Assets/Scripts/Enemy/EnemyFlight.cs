using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlight : MonoBehaviour
{
    [SerializeField]
    Transform player; //What object is the player for math

    [SerializeField]
    float agroRange; //How many tiles away it can chace you from

    [SerializeField]
    float moveSpeed; //The speed it moves at

    Rigidbody2D rb2d; //Creates Rigidbody with keyword [rb2d]
    float distToPlayer; //Initalizes the distToPlayer variable

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>(); //Initalizes the Rigidbody
    }

    void Update()
    {
        float distToPlayer = Vector2.Distance(transform.position, player.position); //Track distance between the enemy and the player
        if (distToPlayer < agroRange)
        { //If the player is within the agroRange then chase
            ChasePlayer();
        }
        else
        { //If they're too far away stop
            StopChase();
        }
    }

    void ChasePlayer()
    {
        if (transform.position.x < player.position.x)
        { //Chase player on X axis
            rb2d.velocity = new Vector2(moveSpeed, rb2d.velocity.y);
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            rb2d.velocity = new Vector2(-moveSpeed, rb2d.velocity.y);
            transform.rotation = Quaternion.Euler(180, 0, 180);
        }

        if (transform.position.y < player.position.y)
        { //Chase player on Y axis
            rb2d.velocity = new Vector2(rb2d.velocity.x, moveSpeed);
        }
        else
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, -moveSpeed);
        }
    }

    void StopChase()
    {
        rb2d.velocity = new Vector2(/*rb2d.velocity.x, rb2d.velocity.y*/0, 0); //This allows it to slow down with momentum
    }
}
