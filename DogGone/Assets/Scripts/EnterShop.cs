using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterShop : MonoBehaviour
{
    [SerializeField] private LayerMask playerLayer;
    ShopScript shopScript;

    // Start is called before the first frame update
    void Start()
    {
        shopScript = FindObjectOfType<ShopScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
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
}
