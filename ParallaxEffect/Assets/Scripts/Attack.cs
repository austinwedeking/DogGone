using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    GameObject enemyInZone;

    // Start is called before the first frame update
    void Start()
    {
        
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
}
