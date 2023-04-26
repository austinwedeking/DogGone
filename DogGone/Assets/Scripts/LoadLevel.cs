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

    PlayerData playerData;
    HealthBar healthBar;
    ShopScript shopScript;

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        levelChange = FindObjectOfType<LevelChange>();
        eatPoster = GameObject.Find("eat_poster");
        playerData = FindObjectOfType<PlayerData>();
        healthBar = FindObjectOfType<HealthBar>();
        shopScript = FindObjectOfType<ShopScript>();
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            this.gameObject.transform.position = new Vector2(175.18f, -0.62f);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Debug.Log("Loading new level...");

            //if (eatPoster != null)
            //{
            //    eatPoster.SetActive(false);
            //}
            //Debug.Log(playerData.GetCurrentPlayerHealth());
            //levelChange.SetTemp(playerData.GetCurrentPlayerHealth());
            //playerData.SetCurrentPlayerHealth(levelChange.GetTemp());
            //healthBar.SetHealth(playerData.GetCurrentPlayerHealth());
            //Debug.Log(playerData.GetCurrentPlayerHealth());

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

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

            levelChange.PsuedoStart();

            //StartCoroutine(Wait());

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

    //private IEnumerator Wait()
    //{
    //    yield return new WaitForSeconds(0.5f);

    //    levelChange.PsuedoStart();
    //}
}
