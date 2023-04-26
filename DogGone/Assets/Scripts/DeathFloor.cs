using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathFloor : MonoBehaviour
{
    //GameObject loseScreen;
    void Start(){ //Leftovers for testing, IDK if save to remove
        //loseScreen = GameObject.Find("loseSprite");
        //loseScreen.SetActive(true);
    }
    //void Update() was empty, removed for now
    public void OnTriggerEnter2D(Collider2D collision){
        //A lot of this could be deleted as its commented out

        //if (collision.gameObject.tag == "Player"){
            //player ran into deathfloor
            //show lose screen
            //loseScreen.SetActive(true);
            //loseScreen.transform.position = collision.gameObject.transform.position;


        //Gets the collision component for the player and enemy(s)
        PlayerData playerdata;
        playerdata = collision.GetComponent<PlayerData>();

        EnemyData enemydata;
        enemydata = collision.GetComponent<EnemyData>();
        
        //If either the player or an enemy hits the death plane then make them take 1000 damage to kill them
        if(playerdata != null){
            playerdata.takeDamage(10, 0, 0);
            playerdata.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 25);
            Debug.Log("Player fell in a pit!");
        }
        else if(enemydata != null){
            enemydata.takeDamage(1000, 0, 0);
            Debug.Log("Enemy fell in a pit!");
        }
        //} commented out from the top IF statement
    }
}
