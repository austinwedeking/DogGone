using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    AudioManager audioManager;
    ReadInput readInput;
    public static PauseMenu instance;

    GameObject theGameManager;
    Inventory theInventoryScript;
    LevelChange levelChange;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);

        audioManager = FindObjectOfType<AudioManager>();
        if (audioManager == null)
        {
            Debug.Log($"Cannot find {audioManager}");
        }
        readInput = FindObjectOfType<ReadInput>();

        theGameManager = GameObject.Find("GameManager");
        theInventoryScript = theGameManager.GetComponent<Inventory>();
        levelChange = FindObjectOfType<LevelChange>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        Debug.Log("Loading menu...");
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;

        audioManager.StopPlaying("MonkeysSpinningMonkeys");
        audioManager.StopPlaying("ForestAmbience");
        audioManager.StopPlaying("CityTheme");
        audioManager.StopPlaying("GodPlan");
        audioManager.StopPlaying("SafeReturn");

        readInput.original = true;
        readInput.drakemode = false;
        readInput.whalemode = false;

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
