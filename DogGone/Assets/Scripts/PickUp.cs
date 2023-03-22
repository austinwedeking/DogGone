using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    GameObject theGameManager;
    Inventory theInventoryScript;
    GameObject inventorySlot1;
    SpriteRenderer inventorySlot1Renderer;

    void Start(){
        //On start will get the GameManager component and the Inventory component
        theGameManager = GameObject.Find("GameManager");
        theInventoryScript = theGameManager.GetComponent<Inventory>();
        inventorySlot1 = GameObject.Find("InventorySlot1");
        inventorySlot1Renderer = inventorySlot1.GetComponent<SpriteRenderer>();
    }

    //void Update() is never used, commented out
    
    public void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.name == "Player"){ //If your touching an item we can pick up
            if (theInventoryScript.nextSpot <= theInventoryScript.lastValidSpot){ //As long as the next spot that is empty and its not past the last valid slot then put in inventory
                theInventoryScript.theInventory[theInventoryScript.nextSpot] = this.gameObject; //Insert it into the spot
                theInventoryScript.nextSpot++; //Increment the next slot
                Debug.Log("Picked up by player");
                //Destroy(this.gameObject);
                inventorySlot1Renderer.sprite = this.gameObject.GetComponent<SpriteRenderer>().sprite; //Gets the sprite of the item
                this.gameObject.GetComponent<SpriteRenderer>().enabled = false; //Makes the item that you grabbed become invisible

            }
            else{ //If every inventory slot is full then it will fail
                Debug.Log("Inventory full");
            }
        }
    }
}