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
            Collider2D temp;
            temp = Physics2D.OverlapCircle(transform.position, 3f, playerLayer);

            if (temp != null)
            {
                if (shopScript.GetShopIsOpen())
                {
                    shopScript.CloseShop();
                }
                else
                {
                    shopScript.OpenShop();
                }
            }
        }
    }
}
