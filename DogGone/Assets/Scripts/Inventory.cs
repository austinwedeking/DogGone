using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject[] theInventory;
    public int nextSpot;
    public int lastValidSpot;
    
    void Start(){
        //Stops screen change from purging inventory
        DontDestroyOnLoad(this.gameObject); 
        
        //Sets the next open slot of inventory, the last valid slot of inventory, and greates the inventory object
        nextSpot = 0;
        lastValidSpot = 9;
        theInventory = new GameObject[10];
    }

    //void Update() was unused so removed for now

    public GameObject find(string thingToFind){
        GameObject thingFound = null;
        for (int i = 0; i < theInventory.Length; i++){ //Iterates through the inventory slots
            if (theInventory[i] != null){ //Make sure we don't get a null pointer
                if (theInventory[i].name == thingToFind){
                    thingFound = theInventory[i];
                }
            }
        }
        return thingFound;
    }
}
