using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    GameObject theGameManager;
    Inventory theInventoryScript;
    GameObject inventorySlot1;
    SpriteRenderer inventorySlot1Renderer;

    [Tooltip("This should be a reference to an ability prefab")]
    [SerializeField] GameObject abilityReference;
    [SerializeField] string abilityName;

    void Start(){
        //On start will get the GameManager component and the Inventory component
        theGameManager = GameObject.Find("GameManager");
        theInventoryScript = theGameManager.GetComponent<Inventory>();
        inventorySlot1 = GameObject.Find("InventorySlot1");
        //inventorySlot1Renderer = inventorySlot1.GetComponent<SpriteRenderer>();
    }

    //void Update() is never used, commented out
    
    public void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.name == "Player"){ //If your touching an item we can pick up
            //oldFunction();
            if (abilityReference == null) // do this if it is an upgrade orb
            {
                if (theInventoryScript.nextSpot > 0) // upgrades the last picked-up ability
                {
                    theInventoryScript.theInventory[Random.Range(0, theInventoryScript.nextSpot - 1)].GetComponent<BaseAbility>().Upgrade();
                } else { Debug.Log("There is no ability to upgrade"); }
            }
            else
            {
                theInventoryScript.theInventory[theInventoryScript.nextSpot] = abilityReference; //Insert it into the spot
                theInventoryScript.nextSpot++; //Increment the next slot
                                               //collision.GetComponent<PlayerMovement>().addAbility(abilityName);
            }
            Debug.Log("Picked up by player");
            Destroy(this.gameObject);
        }
    }

    private void oldFunction()
    {
        if (theInventoryScript.nextSpot <= theInventoryScript.lastValidSpot)
        { //As long as the next spot that is empty and its not past the last valid slot then put in inventory
            theInventoryScript.theInventory[theInventoryScript.nextSpot] = this.gameObject; //Insert it into the spot
            theInventoryScript.nextSpot++; //Increment the next slot
            Debug.Log("Picked up by player");
            //Destroy(this.gameObject);
            inventorySlot1Renderer.sprite = this.gameObject.GetComponent<SpriteRenderer>().sprite; //Gets the sprite of the item
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false; //Makes the item that you grabbed become invisible
            this.gameObject.GetComponent<Collider2D>().enabled = false; //Makes the items you grabbed unable to grab again

        }
        else
        { //If every inventory slot is full then it will fail
            Debug.Log("Inventory full");
        }
    }
}