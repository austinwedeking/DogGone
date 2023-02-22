using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    private PlayerMovement playerController;

    [Header("Player Controlls")]
    [SerializeField] private KeyCode right;
    [SerializeField] private KeyCode left;
    [SerializeField] private KeyCode jump;
    [SerializeField] private KeyCode attack;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerMovement>();
        if (playerController == null) { Debug.LogError("Could not find nd object with a PlayerMovement script"); }
    }

    public void GetInputs()
    {
        if (Input.GetKey(right))
        {
            playerController.processRightDown();
        }

        if (Input.GetKey(left))
        {
            playerController.processLeftDown();
        }

        if (Input.GetKey(jump))
        {
            playerController.processJump();
        }

        if (!Input.GetKey(right))
        {
            playerController.processRightUp();
        }

        if (!Input.GetKey(left))
        {
            playerController.processLeftUp();
        }

        if (Input.GetKeyDown(attack))
        {
            playerController.processAttackDown();
        }

        if (Input.GetKeyUp(attack))
        {
            playerController.processAttackUp();
        }
    }
}
