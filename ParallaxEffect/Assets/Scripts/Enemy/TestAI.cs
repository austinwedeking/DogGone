using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAI : BaseAI
{
    private Rigidbody2D AIRigid; // use boolean check in update to make  enemy move in update
    private Transform playerTransform; // attach other colliders to their own child objects so the enemys can be passed through
    private SpriteRenderer AIRenderer; // cange the x and z my 180 degrees to flip the transform fully

    [Header("Attack Management")]
    [SerializeField] private GameObject leftAttack;
    [SerializeField] private GameObject rightAttack;
    private Transform leftAttackTransform;
    private Transform rightAttackTransform;
    [SerializeField] private float attackRadius;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private int damage;

    /*[Tooltip("This is the ammount of time the game waits before triggering the colliders, THIS SHOULD BE SHORTER THAN THE BASE ATTACK TIMER")]
    [SerializeField] private float attackDelay;
    [Tooltip("This is the ammount of time the colliders remain on, THIS TIME PLUS THE ATTACK DELAY SHOULD BE SHORTER THAN THE BASE ATTACK TIMER")]
    [SerializeField] private float attackLength;*/


    private void Start()
    {
        playerTransform = FindObjectOfType<PlayerMovement>().gameObject.transform;
        AIRigid = gameObject.GetComponent<Rigidbody2D>();
        if (AIRigid == null) { Debug.Log("no Rigid Body 2D component found"); }
        AIRenderer = gameObject.GetComponent<SpriteRenderer>();
        if (AIRenderer == null) { Debug.Log("no Sprite Renderer component found"); }
        if (leftAttack == null) { Debug.Log("no object found for left attack position"); }
        else { leftAttackTransform = leftAttack.GetComponent<Transform>(); }
        if (rightAttack == null) { Debug.Log("no object found for right attack position"); }
        else { rightAttackTransform = rightAttack.GetComponent<Transform>(); }
    }

    override public void Attack()
    {
        bool facingLeft;
        Collider2D[] collisions;
        if (playerTransform.position.x > gameObject.transform.position.x)
        { facingLeft = false; }
        else { facingLeft = true; }
        FaceLeft(facingLeft);

        AIRigid.velocity = new Vector2(0, AIRigid.velocity.y);
        
        if (facingLeft)
        {
            collisions = Physics2D.OverlapCircleAll(leftAttackTransform.position, attackRadius, playerLayer);
        } else
        {
            collisions = Physics2D.OverlapCircleAll(rightAttackTransform.position, attackRadius, playerLayer);
        }

        if (collisions.Length > 0) { collisions[0].GetComponentInParent<PlayerData>().takeDamage(damage); }

        //Debug.Log("Attack");
    }
    override public void ChaseWithinRange()
    {

    }
    override public void Chase()
    {
        if (playerTransform.position.x > gameObject.transform.position.x)
        {
            FaceLeft(false);
            AIRigid.velocity = new Vector2(3.0f, AIRigid.velocity.y);
        }
        else
        {
            FaceLeft(true);
            AIRigid.velocity = new Vector2(-3.0f, AIRigid.velocity.y);
        }
        //Debug.Log("Chase");
    }
    override public void Wander()
    {
        //Debug.Log("Wander");
    }
    override public void FaceLeft(bool faceLeft)
    {
        if (faceLeft) { gameObject.transform.rotation = Quaternion.Euler(180, 0, 180); }
        else { gameObject.transform.rotation = Quaternion.Euler(0, 0, 0); }
    }
}
