using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacMove : MonoBehaviour
{
    //Vector3 MoveRight;
    //Vector3 MoveLeft;
    //Vector3 MoveUp;
    //Vector3 MoveDown;

    //Vector3 finalLeft;
    //Vector3 finalRight;
    //Vector3 finalUp;
    //Vector3 finalDown;
    //int speed = 10;

    Rigidbody2D pacPhysics;
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
        //Debug.Log("Hello world. This is start method");
        //MoveRight = new Vector3(1, 0, 0);
        //MoveDown = new Vector3(0, -1, 0);
        //MoveUp = new Vector3(0, 1, 0);
        //MoveLeft = new Vector3(-1, 0, 0);

        pacPhysics = GetComponent<Rigidbody2D>();
        rightForce = new Vector3(7, 0, 0);
        upForce = new Vector3(0, 10, 0);
        leftForce = new Vector3(-7, 0, 0);
        canJump = true;
    }

    // Update is called once per frame
    void Update()
    {
        //finalRight = (MoveRight * Time.deltaTime) * speed;
        //finalLeft = (MoveLeft * Time.deltaTime) * speed;
        //finalUp = (MoveUp * Time.deltaTime) * speed;
        //finalDown = (MoveDown * Time.deltaTime) * speed;

        //if (Input.GetKey(KeyCode.RightArrow))
        //{
        //    //Debug.Log("Right arrow detected.");
        //    transform.Translate(finalRight);
        //}

        //if (Input.GetKey(KeyCode.LeftArrow))
        //{
        //    // Debug.Log("Left Arrow Detected");
        //    transform.Translate(finalLeft);
        //}

        //if (Input.GetKey(KeyCode.DownArrow))
        //{
        //    // Debug.Log("Down Arrow detected");
        //    //transform.Translate(finalDown);
        //}

        //if (Input.GetKey(KeyCode.UpArrow))
        //{
        //    // Debug.Log("Up Arrow Detected");
        //    transform.Translate(finalUp);
        //}

        gravitationalForces = pacPhysics.velocity.y;

        if (Input.GetKey(KeyCode.RightArrow))
        {
            //Debug.Log("Right arrow detected.");
            finalRightForce = new Vector3(rightForce.x, gravitationalForces, 0);

            //pacPhysics.AddForce(rightForce);
            pacPhysics.velocity = finalRightForce;

        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //Debug.Log("Left Arrow Detected");

            finalLeftForce = new Vector3(leftForce.x, gravitationalForces, 0);
            pacPhysics.velocity = finalLeftForce;

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
                pacPhysics.AddForce(upForce, ForceMode2D.Impulse);
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
        if (collision.gameObject.name == "Ground")
        {
            canJump = true;
        }
    }
}
