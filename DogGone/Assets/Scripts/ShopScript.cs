using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopScript : MonoBehaviour
{
    public static bool shopIsOpen = false; public bool GetShopIsOpen() { return shopIsOpen; }
    public GameObject shopUI;

    GameObject theGameManager;
    Inventory theInventoryScript;

    // Start is called before the first frame update
    void Start()
    {
        theGameManager = GameObject.Find("GameManager");
        theInventoryScript = theGameManager.GetComponent<Inventory>();
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
    }
}
