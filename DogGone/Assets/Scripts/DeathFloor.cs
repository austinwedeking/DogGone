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

            Destroy(collision.gameObject);
            Debug.Log("Player fell in! You lose!");
        }
    }
}
