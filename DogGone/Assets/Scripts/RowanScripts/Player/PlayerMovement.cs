using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    //TODO: Make jump so it can be heald to go higher, increase gravity when falling

    private Animator playerAnimator;
    private Inventory inventory;

    private string[] abilityKeys = { "FireAbility" };
    //private int abilityArraySize = 3;
    //private int currArrayPos;

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

    [Header("Player Dash Options")]
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashLength;
    private bool isDashing; public bool GetIsDashing() { return isDashing; }

    [Space(10)]
    [Header("Player Attack Cooldowns")]
    [SerializeField] private float attackCooldownTimer;
    //[SerializeField] private float ability1CooldownTimer; // this os going to need to swap out for reading this value from the 
    //[SerializeField] private float ability2CooldownTimer; // array of abilities
    //[SerializeField] private float ability3CooldownTimer;
    //[SerializeField] private float ability4CooldownTimer;

    // ability bools
    private bool canAttack;
    private bool canUseAbility1; public bool GetAbility1Use() { return canUseAbility1; }
    private bool canUseAbility2; public bool GetAbility2Use() { return canUseAbility2; }
    private bool canUseAbility3; public bool GetAbility3Use() { return canUseAbility3; }
    private bool canUseAbility4; public bool GetAbility4Use() { return canUseAbility4; }

    LevelChange levelChange;
    Inventory inventoryScript;

    private bool grounded;
    private bool isHittingRightWall;
    private bool isHittingLeftWall;
    private bool isHittingWall;
    //private bool touchingRight;
    //private bool touchingLeft;

    public bool isFacingRight;
    private bool canMove;

    void Start()
    {
        abilityKeys = new string[3];

        rigid = gameObject.GetComponent<Rigidbody2D>();
        if (rigid == null) { Debug.LogError("No component of type Rigidbody2D attached to game object"); }
        spriteRend = gameObject.GetComponent<SpriteRenderer>();
        if (spriteRend == null) { Debug.LogError("No component of type SpriteRenderer attached to game object"); }
        inputManager = GetComponent<PlayerInputManager>();
        if (inputManager == null) { Debug.LogError("No component of type PlayerInputManager attached to game object"); }
        if (attackPosition == null) { Debug.LogError("No attack position object attached to game object"); }
        playerAnimator = gameObject.GetComponent<Animator>();
        levelChange = FindObjectOfType<LevelChange>();
        inventoryScript = FindObjectOfType<Inventory>();
        Debug.LogWarning($"inventory script is {inventoryScript}");
        //inventory = FindObjectOfType<Inventory>();
        //if (inventory == null) { Debug.Log("Could not find inventory in game manager"); }

        canAttack = true;
        canUseAbility1 = true;
        canUseAbility2 = true;
        canUseAbility3 = true;
        canUseAbility4 = true;

        isHittingLeftWall = false;
        isHittingRightWall = false;
        isHittingWall = false;

        isFacingRight = true;
        canMove = true;

        isDashing = false;

        //currArrayPos = 0;
    }

    public void ToggleMovement()
    {
        canMove = !canMove;
    }

    void Update()
    {
        if (canMove)
        {
            if (!CheckDash()) { inputManager.GetInputs(); }
        }

        // for debuging
        //Debug.Log(rigid.velocity.x);

        if (canUseAbility1 && (inventoryScript.find("FireAbility") != null))
        {
            levelChange.fireUI.GetComponent<Image>().color = new Color(255, 255, 255, 1f);
            levelChange.fireText.GetComponent<Text>().color = new Color(255, 255, 255, 1f);
        }
        else if (!canUseAbility1 && (inventoryScript.find("FireAbility") != null))
        {
            levelChange.fireUI.GetComponent<Image>().color = new Color(255, 255, 255, 0.2f);
            levelChange.fireText.GetComponent<Text>().color = new Color(255, 255, 255, 0.2f);
        }
        else if (!canUseAbility1 && (inventoryScript.find("FireAbility") == null))
        {
            levelChange.fireUI.GetComponent<Image>().color = new Color(255, 255, 255, 0f);
            levelChange.fireText.GetComponent<Text>().color = new Color(255, 255, 255, 0f);
        }

        if (canUseAbility2 && (inventoryScript.find("DashAbility") != null))
        {
            levelChange.airUI.GetComponent<Image>().color = new Color(255, 255, 255, 1f);
            levelChange.airText.GetComponent<Text>().color = new Color(255, 255, 255, 1f);
        }
        else if (!canUseAbility2 && (inventoryScript.find("DashAbility") != null))
        {
            levelChange.airUI.GetComponent<Image>().color = new Color(255, 255, 255, 0.2f);
            levelChange.airText.GetComponent<Text>().color = new Color(255, 255, 255, 0.2f);
        }
        else if (!canUseAbility2 && (inventoryScript.find("DashAbility") == null))
        {
            levelChange.airUI.GetComponent<Image>().color = new Color(255, 255, 255, 0f);
            levelChange.airText.GetComponent<Text>().color = new Color(255, 255, 255, 0f);
        }

        if (canUseAbility3 && (inventoryScript.find("WaterAbility") != null))
        {
            levelChange.waterUI.GetComponent<Image>().color = new Color(255, 255, 255, 1f);
            levelChange.waterText.GetComponent<Text>().color = new Color(255, 255, 255, 1f);
        }
        else if (!canUseAbility3 && (inventoryScript.find("WaterAbility") != null))
        {
            levelChange.waterUI.GetComponent<Image>().color = new Color(255, 255, 255, 0.2f);
            levelChange.waterText.GetComponent<Text>().color = new Color(255, 255, 255, 0.2f);
        }
        else if (!canUseAbility3 && (inventoryScript.find("WaterAbility") == null))
        {
            levelChange.waterUI.GetComponent<Image>().color = new Color(255, 255, 255, 0f);
            levelChange.waterText.GetComponent<Text>().color = new Color(255, 255, 255, 0f);
        }

        if (canUseAbility4 && (inventoryScript.find("EarthAbility") != null))
        {
            levelChange.earthUI.GetComponent<Image>().color = new Color(255, 255, 255, 1f);
            levelChange.earthText.GetComponent<Text>().color = new Color(255, 255, 255, 1f);
        }
        else if (!canUseAbility4 && (inventoryScript.find("EarthAbility") != null))
        {
            levelChange.earthUI.GetComponent<Image>().color = new Color(255, 255, 255, 0.2f);
            levelChange.earthText.GetComponent<Text>().color = new Color(255, 255, 255, 0.2f);
        }
        else if (!canUseAbility4 && (inventoryScript.find("EarthAbility") == null))
        {
            levelChange.earthUI.GetComponent<Image>().color = new Color(255, 255, 255, 0f);
            levelChange.earthText.GetComponent<Text>().color = new Color(255, 255, 255, 0f);
        }
    }

    //public void addAbility(string name)
    //{
    //    if (currArrayPos! > abilityArraySize)
    //    {
    //        abilityKeys[currArrayPos] = name;
    //        currArrayPos++;
    //    }
    //}

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
            FindObjectOfType<AudioManager>().Play("DogJump");
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
        if (!isDashing) {
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

                    if (canAttack)
                    {
                        playerAnimator.SetBool("isIdol", false);
                        playerAnimator.SetBool("isWalking", true);
                    }
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
                    if (canAttack)
                    {
                        playerAnimator.SetBool("isIdol", false);
                        playerAnimator.SetBool("isWalking", true);
                    }
                    break;
                case 2:
                    rigid.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                    break;
                case 3:
                    rigid.velocity = new Vector2(0, rigid.velocity.y);
                    if (canAttack)
                    {
                        playerAnimator.SetBool("isIdol", true);
                        playerAnimator.SetBool("isWalking", false);
                    }
                    break;
                default:
                    break;
            }
        }
    }

    private void ProcessAttack(int val)
    { // 0 = basic attack, 1 = ability 1
        if (!isDashing)
        {
            switch (val)
            {
                case 0:
                    if (canAttack)
                    {
                        float direction;
                        Debug.Log("Player used base attack");

                        playerAnimator.SetBool("isWalking", false);
                        playerAnimator.SetBool("isIdol", false);

                        Collider2D collisions = Physics2D.OverlapCircle(attackPosition.transform.position, attackRadius, enemyLayer);
                        if (isFacingRight) { direction = 1; } else { direction = -1; }
                        if (collisions != null) { collisions.GetComponent<EnemyData>().takeDamage(damage, 3f * direction, 5f); }
                        //attackPosition.GetComponent<Animator>().Play("AttackAnimation");
                        playerAnimator.Play("NewAttackAnimation");

                        //playerAnimator.SetBool("isWalking", true);
                        //playerAnimator.SetBool("isIdol", true);

                        StartCoroutine(AttackCooldown());
                        StartCoroutine(AnimationDelay());
                        FindObjectOfType<AudioManager>().Play("DogBite");
                        //FindObjectOfType<AudioManager>().Play("DogBark");
                    }
                    break;
                case 1:
                    GameObject temp = inventoryScript.find("FireAbility");
                    if (temp != null)
                    {
                        if (canUseAbility1)
                        {
                            temp.GetComponent<BaseAbility>().Cast();
                            StartCoroutine(Ability1Cooldown(temp.GetComponent<BaseAbility>().GetCooldown()));
                        }
                    }
                    else { Debug.Log("1 was pressed but there is no ability"); }
                    break;
                case 2:
                    if (inventoryScript.find("DashAbility") != null)
                    {
                        if (canUseAbility2)
                        {
                            inventoryScript.find("DashAbility").GetComponent<BaseAbility>().Cast();

                            // special case for dash ability
                            StartCoroutine(DashCooldwn());
                            FindObjectOfType<AudioManager>().Play("DogDash");
                            StartCoroutine(Ability2Cooldown(inventoryScript.find("DashAbility").GetComponent<BaseAbility>().GetCooldown()));
                        }
                    }
                    else { Debug.Log("2 was pressed but there is no ability"); }
                    break;
                case 3:
                    GameObject temp3 = inventoryScript.find("WaterAbility");
                    if (temp3 != null)
                    {
                        if (canUseAbility3)
                        {
                            temp3.GetComponent<BaseAbility>().Cast();
                            StartCoroutine(Ability3Cooldown(temp3.GetComponent<BaseAbility>().GetCooldown()));
                        }
                    }
                    else { Debug.Log("3 was pressed but there is no ability"); }
                    break;
                case 4:
                    GameObject temp4 = inventoryScript.find("EarthAbility");
                    if (temp4 != null)
                    {
                        if (canUseAbility4)
                        {
                            temp4.GetComponent<BaseAbility>().Cast();
                            StartCoroutine(Ability4Cooldown(temp4.GetComponent<BaseAbility>().GetCooldown()));
                        }
                    }
                    else { Debug.Log("4 was pressed but there is no ability"); }
                    break;
                default:
                    break;
            }
        }
    }

    // functions for player dash
    private IEnumerator DashCooldwn()
    {
        isDashing = true;
        playerAnimator.Play("DashAnimation");
        yield return new WaitForSeconds(dashLength);
        isDashing = false;
    }

    private bool CheckDash()
    {
        if (isDashing)
        {
            int direction;
            if (isFacingRight) { direction = 1; } else { direction = -1; }
            rigid.velocity = new Vector2(dashSpeed * direction, 0);
            return true;
        }
        return false;
    }


    // Timers for attack cool-downs
    private IEnumerator AttackCooldown() 
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldownTimer);
        canAttack = true;
    }

    private IEnumerator AnimationDelay()
    {
        yield return new WaitForSeconds(0.2f);
        playerAnimator.SetBool("isIdol", true);
        playerAnimator.Play("IdolAnimation");
    }

    private IEnumerator Ability1Cooldown(float cooldown)
    {
        canUseAbility1 = false;
        yield return new WaitForSeconds(cooldown);
        canUseAbility1 = true;
    }

    private IEnumerator Ability2Cooldown(float cooldown)
    {
        canUseAbility2 = false;
        yield return new WaitForSeconds(cooldown);
        canUseAbility2 = true;
    }

    private IEnumerator Ability3Cooldown(float cooldown)
    {
        canUseAbility3 = false;
        yield return new WaitForSeconds(cooldown);
        canUseAbility3 = true;
    }

    private IEnumerator Ability4Cooldown(float cooldown)
    {
        canUseAbility4 = false;
        yield return new WaitForSeconds(cooldown);
        canUseAbility4 = true;
    }

    private void ClampXVelocity(int direction)
    { // 1 = right, -1 = left
        rigid.velocity = new Vector2(maxXVelocity * direction, rigid.velocity.y);
    }

    private void ChangeDirection(bool faceRight)
    { // true = right, false = left
        if (!faceRight) { gameObject.transform.rotation = Quaternion.Euler(180, 0, 180); isFacingRight = false; }
        else { gameObject.transform.rotation = Quaternion.Euler(0, 0, 0); isFacingRight = true; }
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
