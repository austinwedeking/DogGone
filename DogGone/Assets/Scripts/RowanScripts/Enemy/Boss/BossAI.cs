using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : BaseAI
{
    [Header("Special Boss Options")]
    [SerializeField] private BossData bossData;
    [SerializeField] private attackState bossAttackState;
    private attackState currentAttackState;

    private Rigidbody2D AIRigid;
    private Transform playerTransform;
    private SpriteRenderer AIRenderer;
    private Animator animator;

    [Header("Attack Management")]
    [SerializeField] private GameObject kickAttack;
    [SerializeField] private GameObject throwOrigin;
    [SerializeField] private GameObject fistAttack;
    [SerializeField] private GameObject lazerOrigin;
    [SerializeField] private GameObject bottle;
    private GameObject currentOrigin;

    [SerializeField] private float kickOverlapRadius;
    [SerializeField] private float fistOverlapRadius;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private int kickDamage;
    [SerializeField] private int fistDamage;
    [SerializeField] private float kickDelay;
    [SerializeField] private float bottleDelay;
    [SerializeField] private float fistDelay;
    [SerializeField] private float lazerDelay;

    [Space(10)]
    [Header("Movement Info")]
    [SerializeField] private float moveSpeed;
    private bool isMoving;
    private int movementDirection;

    [Space(10)]
    [Header("Death Info")]
    [SerializeField] private float deathTimer;
    [SerializeField] private string deathAnimName;

    private bool isSecondPhase;
    private bool hasHitPlayer;
    private bool isFacingRight;

    //TODO: add more states for variations of attacks
    private enum attackState
    {
        Kick,
        Bottle,
        Fist,
        Lazer,
        Idol
    }

    private void Start()
    {
        bossAttackState = attackState.Idol;
        isSecondPhase = false;
        hasHitPlayer = false;
        AIRigid = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        playerTransform = FindObjectOfType<PlayerData>().gameObject.transform;
        currentAttackState = bossAttackState;
        animator = gameObject.GetComponent<Animator>();
        Debug.Log($"the bool is {hasHitPlayer}");
    }
    
    private void Update()
    {
        Debug.Log(currentAttackState);
        Debug.Log(hasHitPlayer);
        if (!hasHitPlayer)
        {
            // kick collision
            if (currentAttackState == attackState.Kick)
            {
                Collider2D collision = Physics2D.OverlapCircle(kickAttack.transform.position, kickOverlapRadius, playerLayer);
                if (collision != null)
                {
                    playerTransform.gameObject.GetComponent<PlayerData>().takeDamage(kickDamage, 20f, 5f);
                    hasHitPlayer = true;
                }
            }
        }
    }

    public override void Attack()
    {
        hasHitPlayer = false;

        bool facingLeft;
        if (playerTransform.position.x > gameObject.transform.position.x)
        { facingLeft = false; }
        else { facingLeft = true; }
        FaceLeft(facingLeft);

        switch (bossAttackState)
        {
            case attackState.Kick:
                //
                bossData.attackTimer = bossData.kickTimer;
                Debug.Log($"Kicking, {bossData.attackTimer}");

                // do attack things
                currentOrigin = kickAttack;
                AIRigid.velocity = new Vector2(0, 0);

                StartCoroutine(kickCoroutine());

                //AIRigid.velocity = new Vector2(6 * movementDirection, AIRigid.velocity.y);

                animator.Play("KickAnimation");

                // stop doing attack things

                ChooseNewState();
                break;
            case attackState.Bottle:
                currentAttackState = attackState.Bottle;
                bossData.attackTimer = bossData.bottleThrowTimer;
                Debug.Log($"Throwing Bottle, {bossData.attackTimer}");

                // do attack things
                currentOrigin = kickAttack;
                AIRigid.velocity = new Vector2(0, AIRigid.velocity.y);

                StartCoroutine(bottleCoroutine());

                animator.Play("BottleAnimation");

                // stop doing attack things

                ChooseNewState();
                break;
            case attackState.Fist:
                currentAttackState = attackState.Fist;
                bossData.attackTimer = bossData.fistTimer;
                Debug.Log($"Fisting, {bossData.attackTimer}");

                ChooseNewState();
                break;
            case attackState.Lazer:
                currentAttackState = attackState.Lazer;
                bossData.attackTimer = bossData.lazerTimer;
                Debug.Log($"Lazering, {bossData.attackTimer}");

                ChooseNewState();
                break;
            case attackState.Idol:
                currentAttackState = attackState.Idol;
                bossData.attackTimer = bossData.idolTimer;
                Debug.Log($"Idoling, {bossData.attackTimer}");

                animator.Play("IdolAnimation");

                ChooseNewState();
                break;
        }
    }

    private IEnumerator kickCoroutine()
    {
        yield return new WaitForSeconds(kickDelay);
        currentAttackState = attackState.Kick;
        AIRigid.velocity = new Vector2(6 * movementDirection, AIRigid.velocity.y);
    }

    private IEnumerator bottleCoroutine()
    {
        yield return new WaitForSeconds(bottleDelay);
        GameObject bottleInstance = Instantiate(bottle, throwOrigin.transform.position, Quaternion.Euler(0, 0, 0));
        bottleInstance.GetComponent<ProjectileScript>().SetOwner(this.gameObject);
        bottleInstance.GetComponent<Rigidbody2D>().gravityScale = 0.1f;

        bottleInstance = Instantiate(bottle, throwOrigin.transform.position, Quaternion.Euler(0, 0, 0));
        bottleInstance.GetComponent<ProjectileScript>().SetOwner(this.gameObject);
        bottleInstance.GetComponent<Rigidbody2D>().gravityScale = 0.5f;

        bottleInstance = Instantiate(bottle, throwOrigin.transform.position, Quaternion.Euler(0, 0, 0));
        bottleInstance.GetComponent<ProjectileScript>().SetOwner(this.gameObject);
        bottleInstance.GetComponent<Rigidbody2D>().gravityScale = 1f;
    }

    private void ChooseNewState(){
        int Chance = Random.Range(1,101); //All of them are % chance to happen
        switch (bossAttackState){
            case attackState.Kick:
                if (Chance <= 50){
                    bossAttackState = attackState.Idol;
                }else if(Chance <= 85){
                    bossAttackState = attackState.Bottle;
                }else{
                    bossAttackState = attackState.Kick;
                }break;
            case attackState.Bottle:
                if (Chance <= 50){
                    bossAttackState = attackState.Idol;
                }else if (Chance <= 85){
                    bossAttackState = attackState.Kick;
                }else{
                    bossAttackState = attackState.Bottle;
                }break;
            case attackState.Fist:
                bossAttackState = attackState.Lazer;

                break;
            case attackState.Lazer:
                bossAttackState = attackState.Idol;

                break;
            case attackState.Idol:
                if (Chance <= 40 && isSecondPhase == false){
                    bossAttackState = attackState.Kick;
                }else if (Chance <= 80 && isSecondPhase == false){
                    bossAttackState = attackState.Bottle;
                }else{
                    bossAttackState = attackState.Idol;
                }break;
        }
    }

    public override void Chase()
    {
        throw new System.NotImplementedException();
    }

    public override void ChaseWithinRange()
    {
        throw new System.NotImplementedException();
    }

    public override void Wander()
    {
        
    }

    public override void FaceLeft(bool faceLeft)
    {
        if (faceLeft) { gameObject.transform.rotation = Quaternion.Euler(180, 0, 180); movementDirection = -1; isFacingRight = false; }
        else { gameObject.transform.rotation = Quaternion.Euler(0, 0, 0); movementDirection = 1; isFacingRight = true; }
    }

    public override IEnumerator Die()
    {
        throw new System.NotImplementedException();
    }

    public bool IsFacingRight()
    {
        return isFacingRight;
    }
}