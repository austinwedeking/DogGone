using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballAbility : BaseAbility
{
    private GameObject player;
    private GameObject playerAttackPosition;
    private PlayerMovement directionBool;

    [SerializeField] private GameObject projectile;

    bool startBool1 = false;

    private void PseudoStart()
    {
        directionBool = FindObjectOfType<PlayerMovement>();
        if (directionBool == null) { Debug.Log("No player present in the current scene"); }
        else { player = directionBool.gameObject; playerAttackPosition = directionBool.attackPosition; }
        Debug.Log("Start was run");
    }

    public override void Cast()
    {
        //if (!startBool1)
        //{
        //    PseudoStart();
        //    startBool1 = true;
        //}

        PseudoStart();

        if (player == null) { Debug.Log("Bad"); }
        if (playerAttackPosition == null) { Debug.Log("Double Bad"); }

        directionBool = FindObjectOfType<PlayerMovement>(); // have to get a new refference every time, no idea why
        if (directionBool.isFacingRight)
        {
            Debug.Log("Player fired the fire ball");
            Instantiate(projectile, playerAttackPosition.transform.position,
                Quaternion.Euler(0, 0, 0));
        }
        else if (!directionBool.isFacingRight)
        {
            Debug.Log("Player fired the fire ball");
            Instantiate(projectile, playerAttackPosition.transform.position,
                Quaternion.Euler(180, 0, 180));
        }
    }

    public override void Upgrade()
    {
        throw new System.NotImplementedException();
    }
}
