using System.Linq;
using Unity.Behavior;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public Transform[] patrolPoints; // 순찰 지점
    public float patrolSpeed = 2f;
    public float chaseSpeed = 2f;

    public Transform player;
    public float detectionRange = 5f;

    private int currentPatrolIndex = 0;
    private Rigidbody2D rb;

    private EnemyHealth enemyHealth;
    public Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyHealth = GetComponent<EnemyHealth>();
        if(animator != null)
        {
            animator = GetComponent<Animator>();
        }
    }

    void Update()
    {
        if(enemyHealth.isAlive) 
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            if (distanceToPlayer < detectionRange)
            {
                ChasePlayer();
            }
            else
            {
                Patrol();
            }
        }
    }

    void Patrol()
    {
        if(patrolPoints.Length > 0)
        {
            Transform targetPoint = patrolPoints[currentPatrolIndex];
            Vector2 direction = (targetPoint.position - transform.position).normalized;
            rb.linearVelocity = new Vector2(direction.x * patrolSpeed, rb.linearVelocity.y);

            // 지점에 도달했는지 확인
            if (Vector2.Distance(transform.position, targetPoint.position) < 0.5f)
            {
                currentPatrolIndex = ++currentPatrolIndex % patrolPoints.Length;           
            }
            animator.SetBool("isRunning", true);

        }
        else
        {
            animator.SetBool("isRunning", false);
            return;
        }
    }

    void ChasePlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.linearVelocity = new Vector2(direction.x * chaseSpeed, rb.linearVelocity.y);
        animator.SetBool("isRunning", true );
    }

    void OnDrawGizmosSelected()
    {
        // 감지 범위 시각화
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}

