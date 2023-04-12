using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAI : BaseAI
{
    private Rigidbody2D AIRigid;
    private Transform playerTransform;
    private SpriteRenderer AIRenderer;
    private Animator animator;

    [Header("Attack Management")]
    [SerializeField] private GameObject rightAttack;
    private Transform attackTransform;
    [SerializeField] private float attackOverlapRadius;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private int damage;
    [SerializeField] private float attackDelay;

    [Space(10)]
    [Header("Movement Info")]
    [SerializeField] private float moveSpeed;
    private bool isMoving;
    private int movementDirection;

    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        if (animator == null) { Debug.LogError("no animator for some reason"); }
    }

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
    }

    override public void Attack()
    {
        isMoving = false;
        bool facingLeft;
        //Collider2D collision;
        if (playerTransform.position.x > gameObject.transform.position.x)
        { facingLeft = false; }
        else { facingLeft = true; }
        FaceLeft(facingLeft);
        animator.SetBool("IsWalking", false);
        animator.SetBool("IsAttacking", true);

        AIRigid.velocity = new Vector2(0, AIRigid.velocity.y);

        //collision = Physics2D.OverlapCircle(attackTransform.position, attackOverlapRadius, playerLayer);

        //if (collision != null) { collision.GetComponentInParent<PlayerData>().takeDamage(damage); }

        StartCoroutine(AttackCoroutine());
        FindObjectOfType<AudioManager>().Play("MaskBonk");
    }

    private IEnumerator AttackCoroutine()
    {
        yield return new WaitForSeconds(attackDelay);
        Collider2D collision = Physics2D.OverlapCircle(attackTransform.position, attackOverlapRadius, playerLayer);
        if (collision != null) { collision.GetComponentInParent<PlayerData>().takeDamage(damage, 5 * movementDirection, 6); }
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
        //Debug.Log("Chase");
        animator.SetBool("IsAttacking", false);
        animator.SetBool("IsWalking", true);
    }

    override public void Wander()
    {
        isMoving = false;
        animator.SetBool("IsAttacking", false);
        animator.SetBool("IsWalking", false);
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

    override public void Die()
    {
        //TODO: implement this
        Destroy(gameObject);
    }
}
