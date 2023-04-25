using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneScript : MonoBehaviour
{
    [SerializeField] private int value;
    [SerializeField] private float pickupDelay;

    private Rigidbody2D rigid;
    private GameObject player;

    private bool canPickup;
    private bool canMove;

    private void Start()
    {
        player = FindObjectOfType<PlayerData>().gameObject;
    }

    private void Update()
    {
        if (canMove)
        {
            Vector2 pos = gameObject.transform.position;
            Vector2 playerPos = player.transform.position;
            rigid.AddForce((playerPos - pos) * 4);
        }
    }

    // this effectively acts as a start function
    public void Spawn(Vector2 force)
    {
        canPickup = false;
        canMove = false;
        rigid = gameObject.GetComponent<Rigidbody2D>();
        rigid.AddForce(force, ForceMode2D.Impulse);
        StartCoroutine(Delay());
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(pickupDelay);
        canPickup = true;
        canMove = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (canPickup && collision.gameObject.tag == "Player")
        {
            collision.GetComponent<PlayerData>().AquireBones(value);
            Destroy(gameObject);
        }
    }
}
