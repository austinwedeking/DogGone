using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetection : MonoBehaviour
{
    [SerializeField]
    private bool startGroundedState = false;

    private PlayerMovement playerController;

    private int count = 0;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerMovement>();
        if (playerController != null) { playerController.toggleGrounded(startGroundedState); }
        else { Debug.LogError("Could not find an object with a PlayerMovement script"); }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == ("Ground"))
        {
            playerController.toggleGrounded(true);
            ++count;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == ("Ground"))
        {
            if (--count == 0)
            {
                playerController.toggleGrounded(false);
            }
        }
    }
}
