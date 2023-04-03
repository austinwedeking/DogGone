using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseMenu : MonoBehaviour
{
    AudioManager audioManager;
    LevelChange levelChange;

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
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
