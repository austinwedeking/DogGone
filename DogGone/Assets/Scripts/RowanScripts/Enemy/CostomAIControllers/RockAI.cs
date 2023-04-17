using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockAI : BaseAI
{
    private Rigidbody2D AIRigid;
    private Transform playerTransform;
    private SpriteRenderer AIRenderer;
    private Animator animator;

    [Header("Attack Management")]
    [SerializeField] private GameObject rightAttack;
    private Transform attackTransform;
    [SerializeField] private GameObject projectile;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float attackDelay;

    [Space(10)]
    [Header("Movement Info")]
    [SerializeField] private float moveSpeed;
    private bool isMoving;
    private int movementDirection;
    private bool facingRight;

    private GameObject projectileRefference;

    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        if (animator == null) { Debug.LogError("no animator for some reason"); }
        facingRight = true;
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

    public override void Attack()
    {
        isMoving = false;
        bool facingLeft;
        //Collider2D collision;
        if (playerTransform.position.x > gameObject.transform.position.x)
        { facingLeft = false; }
        else { facingLeft = true; }
        FaceLeft(facingLeft);

        AIRigid.velocity = new Vector2(0, AIRigid.velocity.y);

        StartCoroutine(AttackCoroutine());
    }

    private IEnumerator AttackCoroutine()
    {
        yield return new WaitForSeconds(attackDelay);
        // preform attack here
        if (facingRight)
        {
            projectileRefference = Instantiate(projectile, attackTransform.position, Quaternion.Euler(0, 0, 0));
            projectileRefference.GetComponent<ProjectileScript>().SetOwner(this.gameObject);
        }
        else if (!facingRight)
        {
            projectileRefference = Instantiate(projectile, attackTransform.position, Quaternion.Euler(180, 0, 180));
            projectileRefference.GetComponent<ProjectileScript>().SetOwner(this.gameObject);
        }
    }

    public override void ChaseWithinRange()
    {
        throw new System.NotImplementedException();
    }

    public override void Chase()
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
    }

    public override void Wander()
    {
        isMoving = false;
    }

    private void Update()
    {
        if (isMoving) { AIRigid.velocity = new Vector2(moveSpeed * movementDirection, AIRigid.velocity.y); }
    }

    public override void FaceLeft(bool faceLeft)
    {
        if (faceLeft) { gameObject.transform.rotation = Quaternion.Euler(180, 0, 180); facingRight = false; }
        else { gameObject.transform.rotation = Quaternion.Euler(0, 0, 0); facingRight = true; }
    }

    public bool IsFacingRight()
    {
        return facingRight;
    }

    public override void Die()
    {
        //TODO: implement this
        DropBones(-movementDirection);
        Destroy(gameObject);
    }
}
