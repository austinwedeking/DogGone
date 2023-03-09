using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAI : BaseAI
{
    private Rigidbody2D AIRigid;
    private Transform playerTransform;
    private SpriteRenderer AIRenderer;
    private Animator enemyAnimator;

    [Header("Attack Management")]
    [SerializeField] private GameObject rightAttack;
    private Transform attackTransform;
    [SerializeField] private float attackOverlapRadius;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private int damage;

    [Space(10)]
    [Header("Movement Info")]
    [SerializeField] private float moveSpeed;
    private bool isMoving;
    private int movementDirection;


    private void Start()
    {
        playerTransform = FindObjectOfType<PlayerMovement>().gameObject.transform;
        AIRigid = gameObject.GetComponent<Rigidbody2D>();
        if (AIRigid == null) { Debug.Log("no Rigid Body 2D component found"); }
        AIRenderer = gameObject.GetComponent<SpriteRenderer>();
        if (AIRenderer == null) { Debug.Log("no Sprite Renderer component found"); }
        if (rightAttack == null) { Debug.Log("no object found for right attack position"); }
        else { attackTransform = rightAttack.GetComponent<Transform>(); }
        isMoving = false;
        movementDirection = 1;
        enemyAnimator = gameObject.GetComponent<Animator>();
    }

    override public void Attack()
    {
        isMoving = false;
        bool facingLeft;
        Collider2D collision;
        if (playerTransform.position.x > gameObject.transform.position.x)
        { facingLeft = false; }
        else { facingLeft = true; }
        FaceLeft(facingLeft);

        enemyAnimator.SetBool("isAttacking", true);
        AIRigid.velocity = new Vector2(0, AIRigid.velocity.y);

        collision = Physics2D.OverlapCircle(attackTransform.position, attackOverlapRadius, playerLayer);

        if (collision != null) { collision.GetComponentInParent<PlayerData>().takeDamage(damage); }

    }

    override public void ChaseWithinRange()
    {

    }

    override public void Chase()
    {
        if (playerTransform.position.x > gameObject.transform.position.x)
        {
            FaceLeft(false);
            movementDirection = 1;
            isMoving = true;

            //AIRigid.velocity = new Vector2(3.0f, AIRigid.velocity.y);
        }
        else
        {
            FaceLeft(true);
            movementDirection = -1;
            isMoving = true;
            //AIRigid.velocity = new Vector2(-3.0f, AIRigid.velocity.y);
        }
        enemyAnimator.SetBool("isAttacking", false);
        //Debug.Log("Chase");
    }

    override public void Wander()
    {
        isMoving = false;
        enemyAnimator.SetBool("isAttacking", false);
        //Debug.Log("Wander");
    }

    private void Update()
    {
        if (isMoving) { AIRigid.velocity = new Vector2(moveSpeed * movementDirection, AIRigid.velocity.y); }
    }

    override public void FaceLeft(bool faceLeft)
    {
        if (faceLeft) { gameObject.transform.rotation = Quaternion.Euler(180, 0, 180); }
        else { gameObject.transform.rotation = Quaternion.Euler(0, 0, 0); }
    }
}
