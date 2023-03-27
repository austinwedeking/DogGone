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
    [SerializeField] private KeyCode ability1;
    [SerializeField] private KeyCode ability2;
    [SerializeField] private KeyCode ability3;
    [SerializeField] private KeyCode ability4;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerMovement>();
        if (playerController == null) { Debug.LogError("Could not find nd object with a PlayerMovement script"); }
    }

    public void GetInputs()
    {
        // movement controlls ***
        if (Input.GetKey(right))
        {
            playerController.processRightDown();
        }

        if (Input.GetKey(left))
        {
            playerController.processLeftDown();
        }

        if (Input.GetKeyDown(jump))
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
        // end movement controlls ***

        // attack/abilities controlls ***
        if (Input.GetKeyDown(attack))
        {
            playerController.processAttackDown();
        }

        if (Input.GetKeyDown(ability1))
        {
            playerController.processAbilityDown(1);
        }

        if (Input.GetKeyDown(ability2))
        {
            playerController.processAbilityDown(2);
        }

        if (Input.GetKeyDown(ability3))
        {
            playerController.processAbilityDown(3);
        }

        if (Input.GetKeyDown(ability4))
        {
            playerController.processAbilityDown(4);
        }

        // end attack/abilities controlls ***
    }
}
