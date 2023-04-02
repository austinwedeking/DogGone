using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChange : MonoBehaviour
{
    [SerializeField] private int numEnemies;
    [SerializeField] private int loseSceneIndex;
    [SerializeField] private int winSceneIndex;

    private GameObject player;

    [Header("References to ability pickup objects")]
    [SerializeField] private GameObject ability1;

    GameObject button1;
    GameObject button2;
    GameObject theGameManager;
    Inventory theInventoryScript;

    GameObject eatPoster;
    public int index;

    private void Start()
    {
        player = FindObjectOfType<PlayerData>().gameObject;
        if (player == null) { Debug.Log("No player found in active scene"); }
    }

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

        eatPoster = GameObject.Find("eat_poster");
        if (eatPoster != null)
        {
            eatPoster.SetActive(false);
        }

        theGameManager = GameObject.Find("GameManager");
        theInventoryScript = theGameManager.GetComponent<Inventory>();
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

    public void GameOver()
    { //Loads the lose screen
        index = SceneManager.GetActiveScene().buildIndex;
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
                Vector3 tempVector = new Vector3(player.transform.position.x, player.transform.position.y + 2, player.transform.position.z);
                Instantiate(ability1, tempVector, Quaternion.identity);
            }
            else
            {
                Debug.Log("Inventory full");
            }

            eatPoster.SetActive(true);
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

            eatPoster.SetActive(true);
        }
    }
}