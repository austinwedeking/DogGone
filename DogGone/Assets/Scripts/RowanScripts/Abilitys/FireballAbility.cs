using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballAbility : BaseAbility
{
    private GameObject player;
    private GameObject playerAttackPosition;

    [SerializeField] private GameObject projectile;

    private void Start()
    {
        PlayerMovement temp = FindObjectOfType<PlayerMovement>();
        if (temp == null) { Debug.Log("No player present in the current scene"); }
        else { player = temp.gameObject; playerAttackPosition = temp.attackPosition; }
    }

    public override void Cast()
    {
        Instantiate(projectile, playerAttackPosition.transform.position, 
            Quaternion.Euler(player.transform.rotation.x, 0, player.transform.rotation.z));
    }

    public override void Upgrade()
    {
        throw new System.NotImplementedException();
    }
}
