using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    GameObject enemyInZone;

    GameObject theGameManager;
    Inventory theInventoryScript;
    
    public GameObject fireballPrefab;
    private GameObject spawnedFireball;
    private GameObject spawnPoint;

    private IEnumerator theCoroutine;
    private bool onTimer = false;
    
    // Start is called before the first frame update
    void Start()
    {
        theGameManager = GameObject.Find("GameManager");
        theInventoryScript = theGameManager.GetComponent<Inventory>();
        spawnPoint = GameObject.Find("SpawnPoint");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            if(enemyInZone != null)
            {
                Debug.Log("Attacking");
                Damage enemyScript = enemyInZone.GetComponent<Damage>();
                enemyScript.damageEnemy(3);
            }
            else
            {
                Debug.Log("Trying to attack, but no enemy in zone");
            }
        }
        else if(Input.GetKeyDown(KeyCode.R))
        {
            GameObject fireball = theInventoryScript.find("drake_collect");

            if(fireball != null)
            {
                Debug.Log("Attacking with " + fireball.name);

                if(onTimer == false)
                {
                    StartCoroutine(fireballDelay());
                }

                //Rigidbody2D fireballPhysics = spawnedFireball.GetComponent<Rigidbody2D>();
                //Vector3 fireballForce = new Vector3(30, 2, 0);
                //fireballPhysics.AddForce(fireballForce, ForceMode2D.Impulse);
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            enemyInZone = collision.gameObject;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            enemyInZone = null;
        }
    }

    private IEnumerator fireballDelay()
    {
        while (true)
        {
            onTimer = true;
            spawnedFireball = Instantiate(fireballPrefab, spawnPoint.transform.position, Quaternion.identity);
            //does this code on the frame it is started
            //yield return null; //waits here until the next frame
            //does this code after the frame has passed

            //does this code on the framed it is called
            yield return new WaitForSeconds(.001f); //waits 3 seconds
                                                //then does the code down here

            Rigidbody2D fireballPhysics = spawnedFireball.GetComponent<Rigidbody2D>();
            Vector3 fireballForce = new Vector3(10, 3, 0);
            fireballPhysics.AddForce(fireballForce, ForceMode2D.Impulse);

            //yield return new WaitForSeconds(3); //waits 3 more seconds

            //fireballForce = new Vector3(-10, 7, 0);
            //fireballPhysics.AddForce(fireballForce, ForceMode2D.Impulse);

            onTimer = false;
            //return "truly" returns
            //finishes here
        }
    }
}