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

    [SerializeField] private int bonesValue;
    [SerializeField] private GameObject bonePrefab;

    protected void DropBones(int direction)
    {
        GameObject temp;
        for (int i = 0; i < bonesValue; ++i)
        {
            temp = Instantiate(bonePrefab, new Vector2(gameObject.transform.position.x + Random.Range(-0.4f, 0.4f), gameObject.transform.position.y + Random.Range(-0.4f, 0.4f)), Quaternion.identity);
            temp.GetComponent<BoneScript>().Spawn(new Vector2(8f * direction, Random.Range(-2f, 2f)));
        }
    }

}
