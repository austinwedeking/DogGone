using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    private int health;
    // Start is called before the first frame update

    private LevelChange levelChange;

    void Start()
    {
        health = 10;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void damageEnemy(int incomingDamage)
    {
        health -= incomingDamage;
        Debug.Log("Enemy took a hit!");
        possiblyDestroy();
    }

    private void possiblyDestroy()
    {
        if(health <= 0)
        {
            Debug.Log("Destroying enemy");
            levelChange.DecrementEnemies();
            Destroy(gameObject);
        }
    }
}
