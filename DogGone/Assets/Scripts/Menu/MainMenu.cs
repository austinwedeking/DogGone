using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public AudioManager audioManager;
    public ReadInput readInput;

    public GameObject canvas;
    public GameObject whaleBG;
    public GameObject drakeBG;

    void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
        if (audioManager == null)
        {
            Debug.Log($"Cannot find {audioManager}");
        }

        canvas = GameObject.Find("Background");

        whaleBG = GameObject.Find("WhaleBG");

        drakeBG = GameObject.Find("DrakeBG");

        readInput = FindObjectOfType<ReadInput>();

        if (readInput.whalemode)
        {
            canvas.SetActive(false);
            whaleBG.SetActive(true);
            drakeBG.SetActive(false);
        }

        if (readInput.drakemode)
        {
            canvas.SetActive(false);
            whaleBG.SetActive(false);
            drakeBG.SetActive(true);
        }

        if (readInput.original)
        {
            canvas.SetActive(true);
            whaleBG.SetActive(false);
            drakeBG.SetActive(false);
        }
    }

    void Update()
    {
        if (audioManager.index == 0)
        {
            if (readInput.whalemode)
            {
                canvas.SetActive(false);
                whaleBG.SetActive(true);
                drakeBG.SetActive(false);
            }

            if (readInput.drakemode)
            {
                canvas.SetActive(false);
                whaleBG.SetActive(false);
                drakeBG.SetActive(true);
            }

            if (readInput.original)
            {
                canvas.SetActive(true);
                whaleBG.SetActive(false);
                drakeBG.SetActive(false);
            }
        }
    }

    public void PlayGame()
    {
        audioManager.index++;
        SceneManager.LoadScene(audioManager.index);

        if (readInput.original)
        {
            audioManager.Play("MonkeysSpinningMonkeys");
            audioManager.Play("ForestAmbience");
        }

        if (readInput.whalemode)
        {
            audioManager.Play("SafeReturn");
        }

        if (readInput.drakemode)
        {
            audioManager.Play("GodPlan");
        }
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }


}
