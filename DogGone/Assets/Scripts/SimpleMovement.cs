using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMovement : MonoBehaviour
{
    Rigidbody2D dogPhysics;
    Vector3 rightForce;
    Vector3 upForce;
    Vector3 leftForce;
    Vector3 finalRightForce;
    Vector3 finalLeftForce;

    [SerializeField]
    private bool canJump;

    float gravitationalForces;

    // Start is called before the first frame update
    void Start()
    {
        dogPhysics = GetComponent<Rigidbody2D>();
        rightForce = new Vector3(7, 0, 0);
        upForce = new Vector3(0, 18, 0);
        leftForce = new Vector3(-7, 0, 0);
        canJump = true;
    }

    // Update is called once per frame
    void Update()
    {
        gravitationalForces = dogPhysics.velocity.y;

        if (Input.GetKey(KeyCode.RightArrow))
        {
            //Debug.Log("Right arrow detected.");
            finalRightForce = new Vector3(rightForce.x, gravitationalForces, 0);

            //dogPhysics.AddForce(rightForce);
            dogPhysics.velocity = finalRightForce;
            transform.rotation = Quaternion.Euler(0, 0, 0);

        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //Debug.Log("Left Arrow Detected");

            finalLeftForce = new Vector3(leftForce.x, gravitationalForces, 0);
            dogPhysics.velocity = finalLeftForce;
            transform.rotation = Quaternion.Euler(180, 0, 180);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            //Debug.Log("Down Arrow detected");

        }

        if (Input.GetKeyDown(KeyCode.UpArrow)) //only returns true on first frame
        {
            if (canJump == true)
            {
                Debug.Log("Jumping!");
                canJump = false;
                dogPhysics.AddForce(upForce, ForceMode2D.Impulse);
            }
            else
            {
                Debug.Log("trying to jump, but can't :)");
            }
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("ran into" + collision.gameObject.name);
        if (collision.gameObject.name == "Tilemap")
        {
            canJump = true;
        }
    }
}