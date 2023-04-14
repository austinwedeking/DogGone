using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthAbility : BaseAbility
{
    [Header("Circle Collider Options")]
    [SerializeField] private float radius;
    [SerializeField] private float force;

    [Space(10)]
    [SerializeField] GameObject spikeAffect;

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
                // do stuff for ground ability

                Instantiate(spikeAffect, temp.transform.position, Quaternion.identity);
                temp.takeDamage(damage, 0f, force);
            }
        }
    }

    public override void Upgrade()
    {
        throw new System.NotImplementedException();
    }
}
