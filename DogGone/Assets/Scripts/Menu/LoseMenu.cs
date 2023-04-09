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
    }

    public void RetryLevel()
    {
        Debug.Log("Reloading last level...");
        levelChange.ResetEnemies();
        SceneManager.LoadScene(levelChange.GetComponent<LevelChange>().index);
    }

    public void LoadMenu()
    {
        Debug.Log("Loading menu...");
        audioManager.StopPlaying("MonkeysSpinningMonkeys");

        for (int i = 0; i < theInventoryScript.lastValidSpot; i++)
        {
            theInventoryScript.theInventory[i] = null;
        }

        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
