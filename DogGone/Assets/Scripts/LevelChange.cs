using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChange : MonoBehaviour
{
    [SerializeField] private int numEnemies;
    [SerializeField] private int loseSceneIndex;
    [SerializeField] private int winSceneIndex;

    [Header("References to ability pickup objects")]
    [SerializeField] private GameObject ability1;

    GameObject button1;
    GameObject button2;
    GameObject theGameManager;
    Inventory theInventoryScript;

    private void Awake(){ //Called before start on object creation, just here to make the value start at 0
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
    }

    public void IncrementEnemies(){ //When an enemy spawns increment the enemy number
        ++numEnemies;
    }

    public void DecrementEnemies(){ //When an enemy dies decrement the enemy number
        --numEnemies;
        if(numEnemies <= 0){ //If no enemies are alive then show the win screen
            Choose();
        }
    }

    public void GameOver()
    { //Loads the loose screen
        SceneManager.LoadScene(loseSceneIndex);
    }

    private void Win()
    { //Loads the win screen
        SceneManager.LoadScene(winSceneIndex);
    }

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
                /*theInventoryScript.theInventory[theInventoryScript.nextSpot] = GameObject.Find("thewhale");
                theInventoryScript.nextSpot++;
                Debug.Log("Picked up the whale");*/
                Instantiate(ability1, new Vector3(0, 0, 0), Quaternion.identity);
                //Win();
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
                Win();
            }
            else
            {
                Debug.Log("Inventory full");
            }
        }
    }
}