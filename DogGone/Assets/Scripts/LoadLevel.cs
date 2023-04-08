using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    GameObject button1;
    GameObject button2;
    GameObject eatPoster;
    LevelChange levelChange;

    private void Start()
    {
        levelChange = FindObjectOfType<LevelChange>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Debug.Log("Loading new level...");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

            levelChange.PsuedoStart();

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

            //eatPoster = GameObject.Find("eat_poster2");
            //if (eatPoster != null)
            //{
            //    eatPoster.SetActive(false);
            //}
        }
    }
}
