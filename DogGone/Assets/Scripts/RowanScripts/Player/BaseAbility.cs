using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAbility : MonoBehaviour
{
    [Tooltip("WARNING: The damage value here is overriden by the projectile if the ability fires a projectile")]
    [SerializeField] protected int damage; // the list of abilitied will sore game objects, each will have a script derived form this one
    [SerializeField] protected float cooldownTimer; // this will allow the same functions to be called on the list to use/modify the abilities

    protected Animator animator;
    [SerializeField] protected LayerMask enemyLayer;

    public abstract void Cast();
    public abstract void Upgrade();
}
