using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2AI : BaseAI
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
    [SerializeField] private GameObject fistAttack;
    [SerializeField] private GameObject lazerOrigin;
    [SerializeField] private GameObject lazer;
    private GameObject currentOrigin;

    [SerializeField] private float fistOverlapRadius;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private int fistDamage;
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

    private bool hasHitPlayer;
    private bool isFacingRight;

    //TODO: add more states for variations of attacks
    private enum attackState
    {
        Fist,
        Lazer,
        Idol,
        Spawn
    }

    private void Start()
    {
        bossAttackState = attackState.Spawn;
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
            if (currentAttackState == attackState.Fist)
            {
                Collider2D collision = Physics2D.OverlapCircle(fistAttack.transform.position, fistOverlapRadius, playerLayer);
                if (collision != null)
                {
                    playerTransform.gameObject.GetComponent<PlayerData>().takeDamage(fistDamage, 20f, 5f);
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
            case attackState.Fist:
                bossData.attackTimer = bossData.fistTimer;
                Debug.LogWarning($"Kicking, {bossData.attackTimer}");

                // do attack things
                currentOrigin = fistAttack;
                AIRigid.velocity = new Vector2(0, 0);

                StartCoroutine(fistCoroutine());

                //AIRigid.velocity = new Vector2(6 * movementDirection, AIRigid.velocity.y);

                animator.Play("FistAnimation");

                // stop doing attack things

                ChooseNewState();
                break;
            case attackState.Lazer:
                currentAttackState = attackState.Lazer;
                bossData.attackTimer = bossData.lazerTimer;
                Debug.Log($"Throwing Bottle, {bossData.attackTimer}");

                // do attack things
                // currentOrigin
                AIRigid.velocity = new Vector2(0, AIRigid.velocity.y);

                StartCoroutine(lazerCoroutine());

                animator.Play("LazerAnimation");

                // stop doing attack things

                ChooseNewState();
                break;
            case attackState.Idol:
                currentAttackState = attackState.Idol;
                bossData.attackTimer = bossData.idolTimer;
                Debug.Log($"Idoling, {bossData.attackTimer}");

                AIRigid.velocity = new Vector2(0, 0);

                animator.Play("IdolAnimation");

                ChooseNewState();
                break;
            case attackState.Spawn:
                currentAttackState = attackState.Spawn;
                bossData.attackTimer = bossData.SpawnTimer;
                Debug.Log($"Spawning, {bossData.attackTimer}");

                AIRigid.velocity = new Vector2(0, 0);

                animator.Play("Transformation1");

                ChooseNewState();
                break;
        }
    }

    private IEnumerator fistCoroutine()
    {
        yield return new WaitForSeconds(fistDelay);
        currentAttackState = attackState.Fist;
        AIRigid.velocity = new Vector2(8 * movementDirection, AIRigid.velocity.y);
    }

    private IEnumerator lazerCoroutine()
    {
        yield return new WaitForSeconds(lazerDelay);
        //GameObject lazerInstance = Instantiate(lazer, lazerOrigin.transform.position, Quaternion.Euler(0, 0, 0));
        if (isFacingRight) {
            Instantiate(lazer, lazerOrigin.transform.position, Quaternion.Euler(180, 0, 180));
        } else
        {
            Instantiate(lazer, lazerOrigin.transform.position, Quaternion.Euler(0, 0, 0));
        }
    }

    private void ChooseNewState()
    {
        int Chance = Random.Range(1, 101); //All of them are % chance to happen
        switch (bossAttackState)
        { //In an attack its a 50% to idle, 35% to do the other attack, and 15% to repeat attack
            case attackState.Fist:
                if (Chance <= 50)
                {
                    bossAttackState = attackState.Idol;
                }
                else if (Chance <= 85)
                {
                    bossAttackState = attackState.Lazer;
                }
                else
                {
                    bossAttackState = attackState.Fist;
                }
                break;
            case attackState.Lazer:
                if (Chance <= 50)
                {
                    bossAttackState = attackState.Idol;
                }
                else if (Chance <= 85)
                {
                    bossAttackState = attackState.Fist;
                }
                else
                {
                    bossAttackState = attackState.Lazer;
                }
                break;
            case attackState.Idol: //In idle then 45% to do ranged attack, 45% to do melee attack, 10% to idle again
                if (Chance <= 45)
                {
                    bossAttackState = attackState.Fist;
                }
                else if (Chance <= 90)
                {
                    bossAttackState = attackState.Lazer;
                }
                else
                {
                    bossAttackState = attackState.Idol;
                }
                break;
            case attackState.Spawn:
                bossAttackState = attackState.Idol;
                break;
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
        AIRigid.velocity = new Vector2(0f, 0f);
        animator.Play(deathAnimName);
        yield return new WaitForSeconds(deathTimer); // this is so all paths return something-
        Destroy(gameObject);
    }

    public bool IsFacingRight()
    {
        return isFacingRight;
    }
}
