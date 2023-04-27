using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopScript : MonoBehaviour
{
    public static bool shopIsOpen = false; public bool GetShopIsOpen() { return shopIsOpen; }
    public int timesPurchased = 0;
    private int price = 0;
    public GameObject shopUI;

    [SerializeField] GameObject fireUI;
    [SerializeField] GameObject waterUI;
    [SerializeField] GameObject airUI;
    [SerializeField] GameObject earthUI;


    [SerializeField] GameObject healthUpgradeInfo;
    [SerializeField] GameObject healInfo;

    GameObject theGameManager;
    Inventory theInventoryScript;
    PlayerData playerData;
    HealthBar healthBar;
    LevelChange levelChange;

    // Start is called before the first frame update
    void Start()
    {
        theGameManager = GameObject.Find("GameManager");
        theInventoryScript = theGameManager.GetComponent<Inventory>();
        levelChange = FindObjectOfType<LevelChange>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CloseShop()
    {
        shopUI.SetActive(false);
        shopIsOpen = false;

        fireUI.SetActive(true);
        waterUI.SetActive(true);
        airUI.SetActive(true);
        earthUI.SetActive(true);
    }

    public void OpenShop()
    {
        shopUI.SetActive(true);
        shopIsOpen = true;

        fireUI.SetActive(false);
        waterUI.SetActive(false);
        airUI.SetActive(false);
        earthUI.SetActive(false);

        playerData = FindObjectOfType<PlayerData>();
        healthBar = FindObjectOfType<HealthBar>();
    }

    public void Heal()
    {
        if (playerData.GetCurrentPlayerHealth() < playerData.GetMaxPlayerHealth())
        {
            price = 100;

            if ((playerData.GetBones() - price) >= 0)
            {
                playerData.SetBonesAmount(playerData.GetBones() - price);
                levelChange.SetTempBones(playerData.GetBones());
                playerData.AquireBones(0);

                playerData.SetCurrentPlayerHealth(playerData.GetCurrentPlayerHealth() + 50);

                if (playerData.GetCurrentPlayerHealth() > playerData.GetMaxPlayerHealth())
                {
                    playerData.SetCurrentPlayerHealth(playerData.GetMaxPlayerHealth());
                }

                healthBar.SetHealth(playerData.GetCurrentPlayerHealth());

                Debug.Log("current health is now " + playerData.GetCurrentPlayerHealth());
            }
            else
            {
                StartCoroutine(NoBones(healInfo, "You do not have enough bones to purchase this item!"));
            }
        }
        else
        {
            StartCoroutine(NoBones(healInfo, "You are already at max health."));
        }
    }

    public void UpgradeMaxHealth()
    {
        if (timesPurchased == 0)
        {
            price = 250;

            if ((playerData.GetBones() - price) >= 0)
            {
                playerData.SetBonesAmount(playerData.GetBones() - price);
                levelChange.SetTempBones(playerData.GetBones());
                playerData.AquireBones(0);

                playerData.SetMaxPlayerHealth(150);
                healthBar.SetMaxHealth(playerData.GetMaxPlayerHealth());
                healthBar.SetHealth(playerData.GetCurrentPlayerHealth());
                Debug.Log("max health is now " + playerData.GetMaxPlayerHealth());
                playerData.healthUpgrade = true;
                timesPurchased++;
            }
            else
            {
                StartCoroutine(NoBones(healthUpgradeInfo, "You do not have enough bones to purchase this item!"));
            }
        }
        else if (timesPurchased == 1)
        {
            price = 250;

            if ((playerData.GetBones() - price) >= 0)
            {
                playerData.SetBonesAmount(playerData.GetBones() - price);
                levelChange.SetTempBones(playerData.GetBones());
                playerData.AquireBones(0);

                playerData.SetMaxPlayerHealth(200);
                healthBar.SetMaxHealth(playerData.GetMaxPlayerHealth());
                healthBar.SetHealth(playerData.GetCurrentPlayerHealth());
                Debug.Log("max health is now " + playerData.GetMaxPlayerHealth());
                timesPurchased++;
            }
            else
            {
                StartCoroutine(NoBones(healthUpgradeInfo, "You do not have enough bones to purchase this item!"));
            }
        }
        else if (timesPurchased == 2)
        {
            StartCoroutine(NoBones(healthUpgradeInfo, "You cannot purchase this upgrade anymore."));
        }
    }

    private IEnumerator NoBones(GameObject UIObject, string text)
    {
        string tempText = UIObject.GetComponent<Text>().text;
        UIObject.GetComponent<Text>().text = text;
        yield return new WaitForSeconds(3f);
        UIObject.GetComponent<Text>().text = tempText;
    }
}
