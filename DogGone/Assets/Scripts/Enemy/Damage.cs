using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    private int health;
    void Start(){ //On start sets health to 10
        health = 10;
    }

    //void Update() is unused so commented out   

    public void damageEnemy(int incomingDamage){
        health -= incomingDamage; //Remove the incoming damage from the health
        Debug.Log("Enemy took a hit!");
        possiblyDestroy(); //Will see if it died
    }

    private void possiblyDestroy(){ //If its health is under 0 then will kill the enemy and destroy the object
        if (health <= 0){
            Debug.Log("Destroying enemy");
            Destroy(this.gameObject);
            Debug.Log("Enemy defeated! You win!");
        }
    }
}
