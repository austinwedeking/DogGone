using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnterShop : MonoBehaviour
{
    [SerializeField] private LayerMask playerLayer;
    ShopScript shopScript;
    GameObject tip;

    // Start is called before the first frame update
    void Start()
    {
        shopScript = FindObjectOfType<ShopScript>();
        tip = GameObject.Find("TipText");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Collider2D[] temp;
            temp = Physics2D.OverlapCircleAll(transform.position, 3f, playerLayer);

            foreach (Collider2D collision in temp)
            {
                if (collision != null && collision.tag == "Player")
                {
                    if (shopScript.GetShopIsOpen())
                    {
                        collision.GetComponent<PlayerMovement>().ToggleMovement();
                        shopScript.CloseShop();
                    }
                    else
                    {
                        collision.GetComponent<PlayerMovement>().ToggleMovement();
                        shopScript.OpenShop();
                    }
                }
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            tip.GetComponent<Text>().text = "Press 'E' to enter the shop!";
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            tip.GetComponent<Text>().text = "";
        }
    }
}
