using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseMenu : MonoBehaviour
{
    AudioManager audioManager;
    LevelChange levelChange;

    GameObject theGameManager;
    Inventory theInventoryScript;
    ShopScript shopScript;

    // Start is called before the first frame update
    void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
        if (audioManager == null)
        {
            Debug.Log($"Cannot find {audioManager}");
        }

        levelChange = FindObjectOfType<LevelChange>();
        if (levelChange == null)
        {
            Debug.Log($"Cannot find {levelChange}");
        }

        theGameManager = GameObject.Find("GameManager");
        theInventoryScript = theGameManager.GetComponent<Inventory>();
        shopScript = FindObjectOfType<ShopScript>();
    }

    public void RetryLevel()
    {
        Debug.Log("Reloading last level...");

        //if (levelChange.GetComponent<LevelChange>().index == 2)
        //{
        //    for (int i = 0; i < theInventoryScript.lastValidSpot; i++)
        //    {
        //        theInventoryScript.theInventory[i] = null;
        //    }
        //}

        for (int i = levelChange.GetComponent<LevelChange>().index; i < levelChange.GetComponent<LevelChange>().index + 1; i++)
        {
            if (theInventoryScript.theInventory[i - 2] != null)
            {
                Debug.Log(theInventoryScript.theInventory[i - 2].name + " is in this spot");
                theInventoryScript.theInventory[i - 2] = null;
                if (theInventoryScript.theInventory[i - 2] == null)
                {
                    Debug.Log("same spot is now null");
                }
                theInventoryScript.nextSpot--;
            }
        }

        if (levelChange.GetTemp() <= 0)
        {
            if (shopScript.timesPurchased == 0)
            {
                levelChange.SetTemp(100);
            }
            else if (shopScript.timesPurchased == 1)
            {
                levelChange.SetTemp(150);
            }
            else if (shopScript.timesPurchased == 2)
            {
                levelChange.SetTemp(200);
            }
        }

        

        levelChange.ResetEnemies();
        SceneManager.LoadScene(levelChange.GetComponent<LevelChange>().index);
        levelChange.PsuedoStart();
    }

    public void LoadMenu()
    {
        Debug.Log("Loading menu...");
        audioManager.StopPlaying("MonkeysSpinningMonkeys");

        for (int i = 0; i < theInventoryScript.lastValidSpot; i++)
        {
            theInventoryScript.theInventory[i] = null;
        }

        levelChange.ResetEnemies();
        theInventoryScript.nextSpot = 0;
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
