using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySlam : MonoBehaviour{
    //Serialize Field lets us type in a variable directly in Unity and not need to open it for testing reasons
    [SerializeField]
    Transform player;

    [SerializeField]
    float agroRange;

    [SerializeField]
    float moveSpeed;

    Rigidbody2D rb2d;
    [SerializeField]
    bool onGround = false;
    bool Left = false;
    bool Right = false;
    [SerializeField]
    bool spawned = true;

    public GameObject prefab;
    private GameObject spawnedObject1;
    private GameObject spawnedObject2;
    Rigidbody2D rb2dO1;
    Rigidbody2D rb2dO2;
    void Start(){
        rb2d = GetComponent<Rigidbody2D>(); //Make a rigidbody
    }

    void Update(){
        float distToPlayer = Vector2.Distance(transform.position, player.position);

        if (onGround == true && spawned == false){
            spawned = true; //Sets spawned to true and then runs spawning so it only spawns things once
            spawnedObject1 = Instantiate(prefab, new Vector2(transform.position.x - 1, transform.position.y + 0.05f), Quaternion.identity); //Spawns a projectile on the left and right of the enemy
            spawnedObject2 = Instantiate(prefab, new Vector2(transform.position.x + 1, transform.position.y + 0.05f), Quaternion.identity);
            rb2dO1 = spawnedObject1.GetComponent<Rigidbody2D>(); //Makes the spawned projectiles have a Rigidbody2D
            rb2dO2 = spawnedObject2.GetComponent<Rigidbody2D>();
            rb2dO1.velocity = new Vector2(-moveSpeed*2, 0); //Makes one projectile go left and the other goes right
            rb2dO2.velocity = new Vector2(moveSpeed*2, 0);
        }

        if (distToPlayer < agroRange){ //If the player is within the agroRange then chace
            ChasePlayer();
        }
        else{ //If they're too far away stop
            StopChase();
        }
        //This will cause the enemy to slam down once its above the player and is at least 3 tiles above the player
        //Eventually make a subroutine for this so its not always checking this
        if (Right == true && transform.position.x > player.position.x && transform.position.y > player.position.y + 1.5){
            Slam();
        }
        if (Left == true && transform.position.x < player.position.x && transform.position.y > player.position.y + 1.5){
            Slam();
        }
    }

    void ChasePlayer(){
        if (transform.position.x < player.position.x){ //If they're on your right go right
            if (onGround == true)
                rb2d.velocity = new Vector2(moveSpeed, rb2d.velocity.y);
        } 
        else if (transform.position.x > player.position.x){ //If they're on your left go left
            if (onGround == true)
                rb2d.velocity = new Vector2(-moveSpeed, rb2d.velocity.y);
        }

        //If its on the ground and the difference between its position and the player position is less then 4 then jump
        if (onGround == true && Mathf.Abs(Mathf.Abs(transform.position.x) - Mathf.Abs(player.position.x)) < 4){
            Jump();
        }
    }

    void StopChase(){ //Maybe make a slowdown as it just stops all movement out of range
        rb2d.velocity = new Vector2(rb2d.velocity.x, rb2d.velocity.y);
    }

    void Jump(){
        //Jumps and flags itsself that its in midair
        onGround = false;
        rb2d.velocity = new Vector2(rb2d.velocity.x, moveSpeed * 4);
        spawned = false;

        //If its airborne and on the right of the player go right and flag its on the right so it cannot go left until landing
        if (Left != true && transform.position.x < player.position.x){
            Right = true;
            rb2d.velocity = new Vector2(moveSpeed*2, rb2d.velocity.y);
        }

        //If its airborne and on the left of the player go left and flag its on the left so it cannot go right until landing
        if (Right != true && transform.position.x > player.position.x){
            Left = true;
            rb2d.velocity = new Vector2(moveSpeed * -2, rb2d.velocity.y);
        }
    }

    void Slam(){
        //Makes the enemy unable to move left or right and then move down rapidly
        Left = true;
        Right = true; 
        rb2d.velocity = new Vector2(0, moveSpeed * -3);
    }
    
    void OnCollisionEnter2D(Collision2D Ground){ //If it touches the ground then tag that it is on the ground
        if (Ground.gameObject.tag == "Ground"){
            onGround = true;
            Left = false;
            Right = false;
        }
    }
}
