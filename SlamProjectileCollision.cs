using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlamProjectileCollision : MonoBehaviour{
    private float yPos; //Stores the inital y position
    Rigidbody2D rb2d;
    //Note that gravity for this is x4 so that it notices its off a ledge faster
    private void Start(){
        yPos = transform.position.y; //Gets inital y position
        rb2d = GetComponent<Rigidbody2D>(); //Make a rigidbody
    }
    private void Update(){
        if (yPos > transform.position.y + 0.055){ //If its inital y is more then the current position plus the buffer then delete it
            Destroy(this.gameObject);
        }

        if(Mathf.Abs(rb2d.velocity.x) < 1){ //If it slows down then it hit something so delete it
            Destroy(this.gameObject);
        }
    }
}