using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //TODO: Make jump so it can be heald to go higher, increase gravity when falling

    private Animator playerAnimator;
    private Inventory inventory;

    private string[] abilityKeys = { "FireAbility", "test2" };   

    [Header("Movement Controls")]
    [SerializeField] private float moveSpeed = 8.0f;
    [SerializeField] private float jumpForce = 10.0f;

    [Tooltip("Sets the max horizontal velocity")]
    [SerializeField] private float maxXVelocity = 10.0f;

    [Tooltip("Larger numbers mean longer time to reach max velocity, DO NOT USE ZERO")]
    [SerializeField] private float accelerationMultiplier = 1.0f;

    private Rigidbody2D rigid;
    private SpriteRenderer spriteRend;
    private PlayerInputManager inputManager;

    [Header("Player Attack Options")]
    public GameObject attackPosition;
    [SerializeField] private float attackRadius;
    [SerializeField] private int damage;
    [SerializeField] private LayerMask enemyLayer;

    [Space(10)]
    [Header("Player Attack Cooldowns")]
    [SerializeField] private float attackCooldownTimer;
    [SerializeField] private float ability1CooldownTimer; // this os going to need to swap out for reading this value from the 
    [SerializeField] private float ability2CooldownTimer; // array of abilities
    [SerializeField] private float ability3CooldownTimer;
    [SerializeField] private float ability4CooldownTimer;

    // ability bools
    private bool canAttack;
    private bool canUseAbility1;
    private bool canUseAbility2;
    private bool canUseAbility3;
    private bool canUseAbility4;

    private bool grounded;
    private bool isHittingRightWall;
    private bool isHittingLeftWall;
    private bool isHittingWall;
    //private bool touchingRight;
    //private bool touchingLeft;

    void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
        if (rigid == null) { Debug.LogError("No component of type Rigidbody2D attached to game object"); }
        spriteRend = gameObject.GetComponent<SpriteRenderer>();
        if (spriteRend == null) { Debug.LogError("No component of type SpriteRenderer attached to game object"); }
        //detection = gameObject.GetComponentInChildren<GroundDetection>();
        inputManager = GetComponent<PlayerInputManager>();
        if (inputManager == null) { Debug.LogError("No component of type PlayerInputManager attached to game object"); }
        //facingRight = false;
        if (attackPosition == null) { Debug.LogError("No attack position object attached to game object"); }
        playerAnimator = gameObject.GetComponent<Animator>();
        inventory = FindObjectOfType<Inventory>();
        if (inventory == null) { Debug.Log("Could not find inventory in game manager"); }

        canAttack = true;
        canUseAbility1 = true;
        canUseAbility2 = true;
        canUseAbility3 = true;
        canUseAbility4 = true;

        isHittingLeftWall = false;
        isHittingRightWall = false;
        isHittingWall = false;
    }

    void Update()
    {
        inputManager.GetInputs();

        // for debuging
        //Debug.Log(rigid.velocity.x);
    }

    public void processRightDown()
    {
        ChangeDirection(true);
        if (!isHittingWall)
        {
            if (rigid.velocity.x < maxXVelocity)
            {
                ProcessMovement(0);
            }
            else { ClampXVelocity(1); }
            //ChangeDirection(true);
        } else { rigid.velocity = new Vector2(0f, rigid.velocity.y); }
    }

    public void processLeftDown()
    {
        ChangeDirection(false);
        if (!isHittingWall)
        {
            if (rigid.velocity.x > -maxXVelocity)
            {
                ProcessMovement(1);
            }
            else { ClampXVelocity(-1); }
            //ChangeDirection(false);
        } else { rigid.velocity = new Vector2(0f, rigid.velocity.y); }
    }

    public void processRightUp()
    {
        if (rigid.velocity.x > 0)
        {
            ProcessMovement(3);
        }
    }

    public void processLeftUp()
    {
        if (rigid.velocity.x < 0)
       {
           ProcessMovement(3);
       } 
    }

    public void processJump()
    {
        if (grounded)
        {
            ProcessMovement(2);
        }
    }

    public void processAttackDown()
    {
        ProcessAttack(0);
    }

    public void processAbilityDown(int abilityNum)
    {
        ProcessAttack(abilityNum);
    }

    private void ProcessMovement(int val)
    { // 0 = right, 1 = left, 2 = jump, 3 = stop movement
        float velocityNonZeroCheck = 0;
        switch (val)
        {
            case 0:
                if (rigid.velocity.x != 0)
                {
                    velocityNonZeroCheck = Mathf.Ceil(rigid.velocity.x * accelerationMultiplier);
                    if (velocityNonZeroCheck < 1.0f)
                    { velocityNonZeroCheck = 1.0f; }
                    rigid.AddForce(new Vector2(moveSpeed * (maxXVelocity / velocityNonZeroCheck), 0));
                }
                else
                { rigid.AddForce(new Vector2(moveSpeed, 0)); }
                playerAnimator.SetBool("isWalking", true); 
                break;
            case 1:
                if (rigid.velocity.x != 0)
                {
                    velocityNonZeroCheck = Mathf.Ceil(-rigid.velocity.x * accelerationMultiplier);
                    if (velocityNonZeroCheck < 1.0f)
                    { velocityNonZeroCheck = 1.0f; }
                    rigid.AddForce(new Vector2(-moveSpeed * (maxXVelocity / velocityNonZeroCheck), 0));
                }
                else
                { rigid.AddForce(new Vector2(-moveSpeed, 0)); }
                playerAnimator.SetBool("isWalking", true); 
                break;
            case 2:
                rigid.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                break;
            case 3:
                rigid.velocity = new Vector2(0, rigid.velocity.y);
                playerAnimator.SetBool("isWalking", false);
                break;
            default:
                break;
        }
    }

    private void ProcessAttack(int val)
    { // 0 = basic attack, 1 = ability 1
        switch(val)
        {
            case 0:
                if (canAttack)
                {
                    Debug.Log("Player used base attack");
                    Collider2D collisions = Physics2D.OverlapCircle(attackPosition.transform.position, attackRadius, enemyLayer);
                    if (collisions != null) { collisions.GetComponent<EnemyData>().takeDamage(damage); }
                    attackPosition.GetComponent<Animator>().Play("AttackAnimation");
                    StartCoroutine(AttackCooldown());
                }
                break;
            case 1:
                GameObject temp = inventory.find(abilityKeys[0]);
                if (temp != null)
                {
                    temp.GetComponent<BaseAbility>().Cast();
                } else { Debug.Log("1 was pressed"); }
                break;
            case 2:
                GameObject temp2 = inventory.find(abilityKeys[1]);
                if (temp2 != null)
                {
                    temp2.GetComponent<BaseAbility>().Cast();
                }
                else { Debug.Log("2 was pressed"); }
                break;
            case 3:
                GameObject temp3 = inventory.find(abilityKeys[2]);
                if (temp3 != null)
                {
                    temp3.GetComponent<BaseAbility>().Cast();
                }
                else { Debug.Log("3 was pressed"); }
                break;
            case 4:
                GameObject temp4 = inventory.find(abilityKeys[3]);
                if (temp4 != null)
                {
                    temp4.GetComponent<BaseAbility>().Cast();
                }
                else { Debug.Log("4 was pressed"); }
                break;
            default:
                break;
        }
    }

    // Timers for attack cool-downs
    private IEnumerator AttackCooldown() 
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldownTimer);
        canAttack = true;
    }

    private void ClampXVelocity(int direction)
    { // 1 = right, -1 = left
        rigid.velocity = new Vector2(maxXVelocity * direction, rigid.velocity.y);
    }

    private void ChangeDirection(bool faceRight)
    { // true = right, false = left
        if (!faceRight) { gameObject.transform.rotation = Quaternion.Euler(180, 0, 180); }
        else { gameObject.transform.rotation = Quaternion.Euler(0, 0, 0); }
    }

    public void toggleGrounded(bool val)
    {
        grounded = val;
    }

    public void toggleIsHittingLeftWall(bool val)
    {
        isHittingLeftWall = val;
    }

    public void toggleIsHittingRightWall(bool val)
    {
        isHittingRightWall = val;
    }

    public void toggleIsHittingWall(bool val)
    {
        isHittingWall = val;
    }
}
