using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject[] theInventory;
    public int nextSpot;
    public int lastValidSpot;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        nextSpot = 0;
        lastValidSpot = 9;
        theInventory = new GameObject[10];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject find(string thingToFind)
    {
        GameObject thingFound = null;
        for(int i = 0; i < theInventory.Length; i++)
        {
            if(theInventory[i] != null)
            {
                if (theInventory[i].name == thingToFind)
                {
                    thingFound = theInventory[i];
                } 
            }
        }
        return thingFound;
    }
}
