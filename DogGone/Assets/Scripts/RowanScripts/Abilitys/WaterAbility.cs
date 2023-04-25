using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterAbility : BaseAbility
{
    [Header("Circle Collider Options")]
    [SerializeField] private float radius;
    [SerializeField] private float force;

    [Space(10)]
    [SerializeField] GameObject waterAffect;

    private GameObject player;

    private void Settup()
    {
        player = FindObjectOfType<PlayerData>().gameObject;
    }

    public override void Cast()
    {
        Settup();
        Collider2D[] collision = Physics2D.OverlapCircleAll(player.transform.position, radius, enemyLayer);
        EnemyData temp;
        for (int i = 0; i < collision.Length; ++i)
        {
            temp = null;
            temp = collision[i].GetComponent<EnemyData>();
            if (temp != null)
            {
                // do stuf for water affect

                Instantiate(waterAffect, temp.transform.position, Quaternion.identity);
                temp.takeDamage(damage, 0f, force);
            }
        }
    }

    public override void Upgrade()
    {
        damage += 5;
        cooldownTimer -= 1;
        radius += 1;
        force += 3;
    }
}

