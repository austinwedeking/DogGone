using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    public static LoadLevel instance;

    GameObject button1;
    GameObject button2;
    GameObject eatPoster;
    LevelChange levelChange;

    PlayerData playerData;
    HealthBar healthBar;
    ShopScript shopScript;
    AudioManager audioManager;
    TextAnimation textAnim;

    ReadInput readInput;

    [SerializeField] private LayerMask playerLayer;

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

        DontDestroyOnLoad(gameObject);
        levelChange = FindObjectOfType<LevelChange>();
        eatPoster = GameObject.Find("eat_poster");
        playerData = FindObjectOfType<PlayerData>();
        healthBar = FindObjectOfType<HealthBar>();
        shopScript = FindObjectOfType<ShopScript>();
        audioManager = FindObjectOfType<AudioManager>();
        textAnim = FindObjectOfType<TextAnimation>();
        readInput = FindObjectOfType<ReadInput>();
    }

    void Update()
    {
        if (audioManager.index >= 3)
        {
            this.gameObject.transform.position = new Vector2(175.18f, -0.43f);
        }
        else
        {
            this.gameObject.transform.position = new Vector2(135.15f, -0.182f);
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Collider2D[] temp;
            temp = Physics2D.OverlapCircleAll(transform.position, 2f, playerLayer);

            foreach (Collider2D collision in temp)
            {
                if (collision != null && collision.tag == "Player")
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

                    if (audioManager.index + 1 == 3 && readInput.original)
                    {
                        audioManager.StopPlaying("MonkeysSpinningMonkeys");
                        audioManager.StopPlaying("ForestAmbience");
                        audioManager.Play("CityTheme");
                    }

                    audioManager.index++;
                    SceneManager.LoadScene(audioManager.index);
                    Debug.Log("build index after load level: " + audioManager.index);

                    levelChange.PsuedoStart();
                }
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {

        }
    }
}
