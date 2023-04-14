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

    [SerializeField] GameObject button1;
    [SerializeField] GameObject button2;

    [Header("References to ability pickup objects")]
    [SerializeField] private GameObject[] abilityList;

    [Header("Ability Sprites")]
    [SerializeField] private Sprite ability1Sprite;
    [SerializeField] private Sprite ability2Sprite;
    [SerializeField] private Sprite ability3Sprite;
    [SerializeField] private Sprite ability4Sprite;

    GameObject theGameManager;
    Inventory theInventoryScript;

    GameObject eatPoster;
    public int index;

    EnemyData[] enemies = new EnemyData[100];
    GameObject[] enemyList; // = new GameObject[100];

    PlayerData playerData;
    private int temp = 0; public int GetTemp() { return temp; } public void SetTemp(int i) { temp = i; }
    private int tempBones = 0; public int GetTempBones() { return tempBones; } public void SetTempBones(int i) { tempBones = i; }


    private void Start()
    {
        player = FindObjectOfType<PlayerData>().gameObject;
        if (player == null) { Debug.Log("No player found in active scene"); }

        playerData = FindObjectOfType<PlayerData>();
    }

    private void Update()
    {
       if (Input.GetKeyDown(KeyCode.K))
        {
            enemyList = GameObject.FindGameObjectsWithTag("Enemy");
            for (int i = 0; i < enemyList.Length; i++)
            {
                if (enemyList[i] != null)
                {
                    enemyList[i].GetComponent<EnemyData>().takeDamage(1000, 0, 0);
                }
            }
        }
    }

    public void PsuedoStart()
    {
        //button1 = GameObject.Find("FirstOption");
        //if (button1 != null)
        //{
        //    button1.SetActive(false);
        //}

        //button2 = GameObject.Find("SecondOption");
        //if (button2 != null)
        //{
        //    button2.SetActive(false);
        //}

        Debug.Log("psuedo start ran");

        button1.SetActive(false);
        button2.SetActive(false);

        //eatPoster = FindObjectOfType<LoadLevel>().gameObject;
 
        if (eatPoster != null)
        {
            eatPoster.SetActive(false);
        }
    }

    private void Awake()
    { //Called before start on object creation, just here to make the value start at 0
        numEnemies = 0;

        eatPoster = FindObjectOfType<LoadLevel>().gameObject;

        PsuedoStart();

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
            Debug.Log("temp = " + temp);
            //player = FindObjectOfType<PlayerData>().gameObject;
        }
    }

    public void ResetEnemies()
    {
        numEnemies = 0;
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

        if (button1 == null)
        {
            Debug.Log("button1 is null");
        }
        if (button2 == null)
        {
            Debug.Log("button2 is null");
        }

        button1.SetActive(true);
        button2.SetActive(true);
    }

    public void doThisWhenClicked(int whichButton)
    {
        // Begin script to randomize abilities ***
        // initialize variables to hold random abilities
        GameObject randAbility1;
        GameObject randAbility2;
        int firstPos;
        int secondPos;

        // randomize the abilities
        firstPos = Random.Range(0, abilityList.Length);
        randAbility1 = abilityList[firstPos];

        secondPos = firstPos; // initialize second pos for propper use in the loop
        while (secondPos == firstPos)
        {
            secondPos = Random.Range(0, abilityList.Length);
        }
        randAbility2 = abilityList[secondPos]; // set after loop, ability 1 and 2 will never be the same
        // End script to randomize abilities ***

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

                if (player == null)
                {
                    player = GameObject.Find("Player");
                }

                Vector3 tempVector = new Vector3(player.transform.position.x, player.transform.position.y + 2, player.transform.position.z);
                Instantiate(randAbility1, tempVector, Quaternion.identity);
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
                if (player == null)
                {
                    player = GameObject.Find("Player");
                }

                Vector3 tempVector = new Vector3(player.transform.position.x, player.transform.position.y + 2, player.transform.position.z);
                Instantiate(randAbility2, tempVector, Quaternion.identity);
            }
            else
            {
                Debug.Log("Inventory full");
            }

            eatPoster.SetActive(true);
        }
    }
}