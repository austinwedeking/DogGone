using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopScript : MonoBehaviour
{
    public static bool shopIsOpen = false; public bool GetShopIsOpen() { return shopIsOpen; }
    public int timesPurchased = 0;
    private int price = 0;
    public GameObject shopUI;

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
    }

    public void OpenShop()
    {
        shopUI.SetActive(true);
        shopIsOpen = true;

        playerData = FindObjectOfType<PlayerData>();
        healthBar = FindObjectOfType<HealthBar>();
    }

    public void Heal()
    {
        if (playerData.GetCurrentPlayerHealth() < playerData.GetMaxPlayerHealth())
        {
            price = 100;
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
            Debug.Log("player is at max health");
        }
    }

    public void UpgradeMaxHealth()
    {
        if (timesPurchased == 0)
        {
            price = 200;
            playerData.SetBonesAmount(playerData.GetBones() - price);
            levelChange.SetTempBones(playerData.GetBones());
            playerData.AquireBones(0);

            playerData.SetMaxPlayerHealth(150);
            healthBar.SetMaxHealth(playerData.GetMaxPlayerHealth());
            healthBar.SetHealth(playerData.GetCurrentPlayerHealth());
            Debug.Log("max health is now " + playerData.GetMaxPlayerHealth());
            timesPurchased++;
        }
        else if (timesPurchased == 1)
        {
            price = 400;
            playerData.SetBonesAmount(playerData.GetBones() - price);
            levelChange.SetTempBones(playerData.GetBones());
            playerData.AquireBones(0);

            playerData.SetMaxPlayerHealth(200);
            healthBar.SetMaxHealth(playerData.GetMaxPlayerHealth());
            healthBar.SetHealth(playerData.GetCurrentPlayerHealth());
            Debug.Log("max health is now " + playerData.GetMaxPlayerHealth());
            timesPurchased++;
        }
        else if (timesPurchased == 2)
        {
            Debug.Log("max health achieved");
        }
    }
}
