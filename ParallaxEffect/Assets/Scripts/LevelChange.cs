using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelChange : MonoBehaviour
{
    [SerializeField] private int numEnemies;
    [SerializeField] private int loseSceneIndex;
    [SerializeField] private int winSceneIndex;

    GameObject button1;
    GameObject button2;
    GameObject theGameManager;
    Inventory theInventoryScript;
    GameObject temp;

    private void Awake()
    { //Called before start on object creation, just here to make the value start at 0
        numEnemies = 0;

        button1 = GameObject.Find("FirstOption");
        if (button1 != null)
        {
            button1.SetActive(false);
        }

        button2 = GameObject.Find("SecondOption");
        if (button2 != null)
        {
            button2.SetActive(false);
        }

        theGameManager = GameObject.Find("GameManager");
        theInventoryScript = theGameManager.GetComponent<Inventory>();

        temp = GameObject.Find("thewhale");
        temp.GetComponent<SpriteRenderer>().enabled = false;
        temp = GameObject.Find("map");
        temp.GetComponent<SpriteRenderer>().enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            GameObject whale = theInventoryScript.find("thewhale");

            if (whale != null)
            {
                Debug.Log("Using " + whale.name);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            GameObject themap = theInventoryScript.find("map");

            if (themap != null)
            {
                Debug.Log("Using " + themap.name);
            }
        }
    }

    public void IncrementEnemies()
    { //When an enemy spawns increment the enemy number
        ++numEnemies;
    }

    public void DecrementEnemies()
    { //When an enemy dies decrement the enemy number
        --numEnemies;
        if (numEnemies <= 0)
        { //If no enemies are alive then show the win screen
            Choose();
        }
    }

    //public void GameOver()
    //{ //Loads the loose screen
    //    SceneManager.LoadScene(loseSceneIndex);
    //}

    private void Choose()
    { //Loads the win screen
        Debug.Log("Choose a power");
        button1.SetActive(true);
        button2.SetActive(true);
    }

    public void doThisWhenClicked(int whichButton)
    {
        if (whichButton == 1)
        {
            Debug.Log("chose 1");
            button1.SetActive(false);
            button2.SetActive(false);

            if (theInventoryScript.nextSpot <= theInventoryScript.lastValidSpot)
            {
                theInventoryScript.theInventory[theInventoryScript.nextSpot] = GameObject.Find("thewhale");
                theInventoryScript.nextSpot++;
                Debug.Log("Picked up the whale");
            }
            else
            {
                Debug.Log("Inventory full");
            }
        }
        else if (whichButton == 2)
        {
            Debug.Log("chose 2");
            button1.SetActive(false);
            button2.SetActive(false);
            if (theInventoryScript.nextSpot <= theInventoryScript.lastValidSpot)
            {
                theInventoryScript.theInventory[theInventoryScript.nextSpot] = GameObject.Find("map");
                theInventoryScript.nextSpot++;
                Debug.Log("Picked up map");
            }
            else
            {
                Debug.Log("Inventory full");
            }
        }
    }
}