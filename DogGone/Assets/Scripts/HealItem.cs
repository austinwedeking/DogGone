using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealItem : MonoBehaviour
{
    PlayerData playerData;
    HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        playerData = FindObjectOfType<PlayerData>();
        healthBar = FindObjectOfType<HealthBar>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.tag == "Player")
        {
            playerData.SetCurrentPlayerHealth(playerData.GetCurrentPlayerHealth() + 25);

            if (playerData.GetCurrentPlayerHealth() > playerData.GetMaxPlayerHealth())
            {
                playerData.SetCurrentPlayerHealth(playerData.GetMaxPlayerHealth());
            }

            healthBar.SetHealth(playerData.GetCurrentPlayerHealth());

            Debug.Log("current health is now " + playerData.GetCurrentPlayerHealth());

            Destroy(this.gameObject);
        }
    }
}
