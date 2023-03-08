using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    GameObject theGameManager;
    Inventory theInventoryScript;
    GameObject inventorySlot1;
    SpriteRenderer inventorySlot1Renderer;

    // Start is called before the first frame update
    void Start()
    {
        theGameManager = GameObject.Find("GameManager");
        theInventoryScript = theGameManager.GetComponent<Inventory>();
        inventorySlot1 = GameObject.Find("InventorySlot1");
        inventorySlot1Renderer = inventorySlot1.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            if (theInventoryScript.nextSpot <= theInventoryScript.lastValidSpot)
            {
                theInventoryScript.theInventory[theInventoryScript.nextSpot] = this.gameObject;
                theInventoryScript.nextSpot++;
                Debug.Log("Picked up by player");
                //Destroy(this.gameObject);
                inventorySlot1Renderer.sprite = this.gameObject.GetComponent<SpriteRenderer>().sprite;
                this.gameObject.GetComponent<SpriteRenderer>().enabled = false;

            }
            else
            {
                Debug.Log("Inventory full");
            }
        }
    }
}