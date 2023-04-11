using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAI : MonoBehaviour
{
    // This exists as a way to allow different enemies to have different behaviors, this should NOT be attached as a component
    abstract public void Attack();
    abstract public void ChaseWithinRange();
    abstract public void Chase();
    abstract public void Wander();
    abstract public void Die();
    abstract public void FaceLeft(bool faceRight);

}
