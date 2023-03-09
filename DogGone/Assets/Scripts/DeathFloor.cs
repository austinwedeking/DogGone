using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathFloor : MonoBehaviour
{
    // Start is called before the first frame update
    //GameObject loseScreen;
    void Start()
    {
        //loseScreen = GameObject.Find("loseSprite");
        //loseScreen.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.gameObject.tag == "Player")
        {
            //player ran into deathfloor

            //show lose screen
            //loseScreen.SetActive(true);
            //loseScreen.transform.position = collision.gameObject.transform.position;

            PlayerData playerdata;
            playerdata = collision.GetComponent<PlayerData>();

            EnemyData enemydata;
            enemydata = collision.GetComponent<EnemyData>();

            if(playerdata != null)
            {
                playerdata.takeDamage(1000);
                Debug.Log("Player fell in a pit!");
            }
            else if(enemydata != null)
            {
                enemydata.takeDamage(1000);
                Debug.Log("Enemy fell in a pit!");
            }
        }
    }
}
