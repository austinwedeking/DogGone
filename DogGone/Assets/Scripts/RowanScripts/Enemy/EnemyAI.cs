using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private EnemyData enemy;
    [SerializeField] private BaseAI AIController;

    [SerializeField] private state AIState;

    private CircleCollider2D detectCollider;
    [SerializeField] private float detectRadius;
    private CircleCollider2D attackCollider;
    [SerializeField] private float attackRadius;
    private int collisionLayer;

    private enum state
    {
        Attack,
        ChaseWithinRange,
        Chase,
        Wander
    }

    private Rigidbody2D AIRigid;

    private void Awake()
    {
        if (AIController == null) { Debug.LogError("no AI state controller found, this must be refferenced in the inspector manually"); }
        enemy = gameObject.GetComponent<EnemyData>();
        if (enemy == null) { Debug.LogError("no Enemy Data found for this object found"); }

        CircleCollider2D[] colliders;
        colliders = gameObject.GetComponentsInChildren<CircleCollider2D>();
        if (colliders.Length < 2) { Debug.LogError("There was an issue grabbing all nessisary colliders"); }
        else
        {
            detectCollider = colliders[0];
            attackCollider = colliders[1];
        }

        AIRigid = gameObject.GetComponent<Rigidbody2D>();
        if (AIRigid == null) { Debug.LogError("No component of type Rigid Body 2D found"); }
    }

    private void Start()
    {
        detectCollider.radius = detectRadius; 
        attackCollider.radius = attackRadius;

        collisionLayer = 0;
        manageState();
    }

    private void manageState()
    {
        if (collisionLayer == 2)
        {
            StartCoroutine(Attack());
        } else if (collisionLayer == 1)
        {
            StartCoroutine(Chase());
        } else
        {
            StartCoroutine(Wander());
        }
    }

    private IEnumerator Attack()
    {
        AIController.Attack();
        AIState = state.Attack;
        yield return new WaitForSeconds(enemy.attackTimer);
        manageState();
    }

    private IEnumerator Chase()
    {
        AIController.Chase();
        AIState = state.Chase;
        yield return new WaitForSeconds(enemy.chaseTimer);
        manageState();
    }

    private IEnumerator Wander()
    {
        AIController.Wander();
        AIState = state.Wander;
        yield return new WaitForSeconds(enemy.wanderTimer);
        manageState();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") { collisionLayer++; }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player") { collisionLayer--; }
    }

    // things that affect enemy data
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    /*private void Update() // this is bad, change
    {
        switch(AIState)
        {
            case state.Attack:
                // do thing
                break;
            case state.ChaseWithinRange:
                chaseWithinRange();
                break;
            case state.Chase:
                // do thing
                break;
            case state.Wander:
                // do thing
                break;
            default:
                break;
        }
    }*/

    // make these template functions so it is costomizable for each enemy

    /*private void chaseWithinRange()
    {
        if (playerTransform.position.x > gameObject.transform.position.x)
        {
            AIRigid.velocity = new Vector2(3.0f, AIRigid.velocity.y);
        } else
        {
            AIRigid.velocity = new Vector2(-3.0f, AIRigid.velocity.y);
        }
    }*/
}
