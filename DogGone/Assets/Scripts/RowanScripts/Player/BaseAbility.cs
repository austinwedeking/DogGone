using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAbility : MonoBehaviour
{
    [SerializeField] protected int damage; // the list of abilitied will sore game objects, each will have a script derived form this one
    [SerializeField] protected float cooldownTimer; // this will allow the same functions to be called on the list to use/modify the abilities

    protected SpriteRenderer spriteRend;
    protected Animator animator;
    protected LayerMask enemyLayer;

    public abstract void Cast();
    public abstract void Upgrade();
}
