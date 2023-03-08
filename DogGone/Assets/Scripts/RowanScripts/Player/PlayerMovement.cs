using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //TODO: Negate friction when running into walls, ASK ABOUT THIS

    //TODO: Make jump so it can be heald to go higher, increase gravity when falling

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
    [SerializeField] private GameObject attackPosition;
    [SerializeField] private float attackRadius;
    [SerializeField] private int damage;
    [SerializeField] private LayerMask enemyLayer;
    private bool canAttack;
    private bool grounded;
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
        canAttack = true;
    }

    void Update()
    {
        inputManager.GetInputs();

        // for debuging
        //Debug.Log(rigid.velocity.x);
    }

    public void processRightDown()
    {
        if (rigid.velocity.x < maxXVelocity)
        {
            ProcessMovement(0);
        }
        else { ClampXVelocity(1); }
        ChangeDirection(true);
    }

    public void processLeftDown()
    {
        if (rigid.velocity.x > -maxXVelocity)
        {
            ProcessMovement(1);
        }
        else { ClampXVelocity(-1); }
        ChangeDirection(false);
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

    public void processAttackUp()
    {
        ProcessAttack(1);
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
                break;
            case 2:
                rigid.AddForce(new Vector2(0, jumpForce));
                break;
            case 3:
                rigid.velocity = new Vector2(0, rigid.velocity.y);
                break;
            default:
                break;
        }
    }

    private void ProcessAttack(int val)
    { // 0 = attack, 1 = reset attack
        switch(val)
        {
            case 0:
                Debug.Log("geebs");
                Collider2D collisions = Physics2D.OverlapCircle(attackPosition.transform.position, attackRadius, enemyLayer);
                if (collisions != null) { collisions.GetComponent<EnemyData>().takeDamage(damage); }
                canAttack = false;
                break;
            case 1:
                canAttack = true;
                break;
            default:
                break;
        }
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
}
