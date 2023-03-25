using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetection : MonoBehaviour
{
    [SerializeField]
    private bool startGroundedState = false;

    private PlayerMovement playerController;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerMovement>();
        if (playerController != null) { playerController.toggleGrounded(startGroundedState); }
        else { Debug.LogError("Could not find and object with a PlayerMovement script"); }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == ("Ground"))
        {
            playerController.toggleGrounded(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == ("Ground"))
        {
            playerController.toggleGrounded(false);
        }
    }
}
