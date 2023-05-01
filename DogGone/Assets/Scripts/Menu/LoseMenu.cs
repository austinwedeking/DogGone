using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoseMenu : MonoBehaviour
{
    AudioManager audioManager;
    LevelChange levelChange;
    GameObject canvas;
    ReadInput readInput;

    GameObject theGameManager;
    Inventory theInventoryScript;
    ShopScript shopScript;

    public GameObject normalLose;
    public GameObject drakeLose;
    public GameObject whaleLose;
    public GameObject screenText;

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
        canvas = GameObject.Find("GameCanvas");
        readInput = FindObjectOfType<ReadInput>();

        normalLose = GameObject.Find("NormalScreen");
        drakeLose = GameObject.Find("DrakeScreen");
        whaleLose = GameObject.Find("WhaleScreen");
        screenText = GameObject.Find("ScreenText");

        if (readInput.original)
        {
            normalLose.GetComponent<Image>().color = new Color(255, 255, 255, 1f);
            drakeLose.GetComponent<Image>().color = new Color(255, 255, 255, 0f);
            whaleLose.GetComponent<Image>().color = new Color(255, 255, 255, 0f);
            screenText.GetComponent<Text>().color = new Color(255, 255, 255, 0f);

            //normalLose.SetActive(true);
            //drakeLose.SetActive(false);
            //whaleLose.SetActive(false);
            //screenText.SetActive(false);
        }

        if (readInput.whalemode)
        {
            normalLose.GetComponent<Image>().color = new Color(255, 255, 255, 0f);
            drakeLose.GetComponent<Image>().color = new Color(255, 255, 255, 0f);
            whaleLose.GetComponent<Image>().color = new Color(255, 255, 255, 1f);

            if (audioManager.index == 5)
            {
                screenText.GetComponent<Text>().color = new Color(212, 23, 23, 1f);
            }
            else if (audioManager.index == 6)
            {
                screenText.GetComponent<Text>().color = new Color(52, 255, 0, 1f);
            }

            //normalLose.SetActive(false);
            //drakeLose.SetActive(false);
            //whaleLose.SetActive(true);
            //screenText.SetActive(true);
        }

        if (readInput.drakemode)
        {
            normalLose.GetComponent<Image>().color = new Color(255, 255, 255, 0f);
            drakeLose.GetComponent<Image>().color = new Color(255, 255, 255, 1f);
            whaleLose.GetComponent<Image>().color = new Color(255, 255, 255, 0f);

            if (audioManager.index == 5)
            {
                screenText.GetComponent<Text>().color = new Color(212, 23, 23, 1f);
            }
            else if (audioManager.index == 6)
            {
                screenText.GetComponent<Text>().color = new Color(52, 255, 0, 1f);
            }

            //normalLose.SetActive(false);
            //drakeLose.SetActive(true);
            //whaleLose.SetActive(false);
            //screenText.SetActive(true);
        }
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

        //for (int i = levelChange.GetComponent<LevelChange>().index; i < levelChange.GetComponent<LevelChange>().index + 1; i++)
        //{
        //    if (theInventoryScript.theInventory[i - 1] != null)
        //    {
        //        Debug.Log(theInventoryScript.theInventory[i - 1].name + " is in this spot");
        //        theInventoryScript.theInventory[i - 1] = null;
        //        if (theInventoryScript.theInventory[i - 1] == null)
        //        {
        //            Debug.Log("same spot is now null");
        //        }
        //        theInventoryScript.nextSpot--;
        //    }
        //}

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
        SceneManager.LoadScene(audioManager.index);
        //levelChange.PsuedoStart();
    }

    public void LoadMenu()
    {
        Debug.Log("Loading menu...");
        audioManager.StopPlaying("MonkeysSpinningMonkeys");
        audioManager.StopPlaying("ForestAmbience");
        audioManager.StopPlaying("CityTheme");
        audioManager.StopPlaying("GodPlan");
        audioManager.StopPlaying("SafeReturn");
        audioManager.StopPlaying("BossTheme");
        //Destroy(canvas);

        readInput.original = true;
        readInput.whalemode = false;
        readInput.drakemode = false;

        for (int i = 0; i < theInventoryScript.lastValidSpot; i++)
        {
            theInventoryScript.theInventory[i] = null;
        }

        levelChange.ResetEnemies();
        theInventoryScript.nextSpot = 0;
        audioManager.index = 0;
        SceneManager.LoadScene(audioManager.index);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
