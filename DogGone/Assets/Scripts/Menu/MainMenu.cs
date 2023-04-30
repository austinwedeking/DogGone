using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioManager audioManager;


    void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
        if (audioManager == null)
        {
            Debug.Log($"Cannot find {audioManager}");
        }
    }

    public void PlayGame()
    {
        audioManager.index++;
        SceneManager.LoadScene(audioManager.index);
        audioManager.Play("MonkeysSpinningMonkeys");
        audioManager.Play("ForestAmbience");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }


}
