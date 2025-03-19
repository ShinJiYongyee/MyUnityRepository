using System.Collections;
using UnityEngine;

public class ZombieManager : MonoBehaviour
{
    public EZombieState currentState;
    public Transform target;
    public float attackRange = 1.0f; //���ݹ���
    public float attackDelay = 2.0f; //���ݵ�����
    private float nextAttackTime = 0.0f; //���� ���� �ð�����
    public Transform[] patrolPoints; //���� ��� ������
    private int currentPoint = 0; //���� ���� ��� ���� �ε���
    public float moveSpeed = 2.0f;
    private float trackingRange = 3.0f; //���� ���� ����
    private bool isAttacking = false; //���� ����
    private float evadeRange = 5.0f; //���� ���� ȸ�� �Ÿ�
    private float zombieHP = 100.0f;
    private float distanceToTarget; //Target���� �Ÿ� ��� ��
    private bool isWaiting = false; //���� ��ȯ �� ��� ���� ����
    public float ZombieIdleTime = 2.0f; //�� ���� ��ȯ �� ��� �ð�
    private Coroutine stateRoutine;

    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        currentState = EZombieState.ZombieIdle;

    }

    void Update()
    {
        distanceToTarget = Vector3.Distance(transform.position, target.position);
        ChangeCurrentIEnumeratorState();
        //ChanageState(currentState);
    }

    void ChangeCurrentIEnumeratorState()
    {
        if (distanceToTarget < trackingRange && distanceToTarget > attackRange)
        {
            if (currentState != EZombieState.Chase)
            {
                ChanageState(EZombieState.Chase);
            }
        }
        else if (distanceToTarget < attackRange)
        {
            if (currentState != EZombieState.Attack)
            {
                ChanageState(EZombieState.Attack);
            }
        }
        else
        {
            if (patrolPoints.Length > 0 && currentState != EZombieState.Patrol)
            {
                ChanageState(EZombieState.Patrol);
            }
            else if (patrolPoints.Length == 0 && currentState != EZombieState.ZombieIdle)
            {
                ChanageState(EZombieState.ZombieIdle);
            }
        }
    }

    public void ChanageState(EZombieState newState)
    {

        if (stateRoutine != null)
        {
            StopCoroutine(stateRoutine);
        }

        currentState = newState;

        switch (currentState)
        {
            case EZombieState.ZombieIdle:
                stateRoutine = StartCoroutine(ZombieIdle());
                break;
            case EZombieState.Patrol:
                stateRoutine = StartCoroutine(Patrol());
                break;
            case EZombieState.Chase:
                stateRoutine = StartCoroutine(Chase());
                break;
            case EZombieState.Attack:
                stateRoutine = StartCoroutine(Attack());
                break;
            //case EZombieState.Evade:
            //    stateRoutine = StartCoroutine(Evade());
            //    break;
            case EZombieState.Die:
                stateRoutine = StartCoroutine(Die());
                break;
        }
    }

    private IEnumerator ZombieIdle()
    {
        Debug.Log(gameObject.name + " : �����");
        animator.Play("ZombieIdle");

        //while (currentState == EZombieState.ZombieIdle)
        //{
        //    float distance = Vector3.Distance(transform.position, target.position);

        //    if (distance < trackingRange && distance > attackRange)
        //    {
        //        ChanageState(EZombieState.Chase);
        //    }
        //    else if (distance < attackRange)
        //    {
        //        ChanageState(EZombieState.Attack);
        //    }

        //    yield return null;
        //}
        yield return null;
    }


    private IEnumerator Patrol()
    {
        Debug.Log(gameObject.name + " : ������");

        while (currentState == EZombieState.Patrol)
        {
            if (patrolPoints.Length > 0)
            {
                animator.SetBool("IsWalking", true);
                Transform targetPoint = patrolPoints[currentPoint];
                Vector3 direction = (targetPoint.position - transform.position).normalized;
                transform.position += direction * moveSpeed * Time.deltaTime;
                transform.LookAt(targetPoint.transform);

                if (Vector3.Distance(transform.position, targetPoint.position) < 0.3f)
                {
                    currentPoint = (currentPoint + 1) % patrolPoints.Length;
                }

                //float distance = Vector3.Distance(transform.position, target.position);
                //if (distance < trackingRange && distance > attackRange)
                //{
                //    ChanageState(EZombieState.Chase);
                //}
                //else if (distance < attackRange)
                //{
                //    ChanageState(EZombieState.Attack);
                //}
            }
            yield return null;
        }
        yield return null;
    }


    private IEnumerator Chase()
    {
        Debug.Log(gameObject.name + " ������");

        while (currentState == EZombieState.Chase)
        {
            float distance = Vector3.Distance(transform.position, target.position);

            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
            transform.LookAt(target.position);
            animator.SetBool("IsWalking", true);

            //if (distance < attackRange)
            //{
            //    ChanageState(EZombieState.Attack);
            //}
            //else if (distance < evadeRange)
            //{
            //    ChanageState(EZombieState.Patrol);
            //}

            yield return null;
        }
        yield return null;
    }

    private IEnumerator Attack()
    {
        Debug.Log(gameObject.name + " : ���� ����");
        float animationLength = attackDelay;

        while (currentState == EZombieState.Attack)
        {
            distanceToTarget = Vector3.Distance(transform.position, target.position);

            if (distanceToTarget > attackRange) // ���� ���� ����� ���� ���·�
            {
                ChanageState(EZombieState.Chase);
            }

            if (!isAttacking)
            {
                isAttacking = true;
                Debug.Log("isAttacking : " + isAttacking);
                animator.SetTrigger("Attack");
                Debug.Log(gameObject.name + " : ������");
                transform.LookAt(target.position); // Ÿ�� �ٶ󺸱�

                //AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
                //animationLength = stateInfo.length;

            }
            yield return new WaitForSeconds(animationLength); // ���� ���� (������)
            isAttacking = false;
            Debug.Log("isAttacking : " + isAttacking);

            //yield return null; // ���� �����ӱ��� ���
        }


        //distanceToTarget = Vector3.Distance(transform.position, target.position);
        //if (distanceToTarget > attackRange && distanceToTarget < trackingRange)
        //{
        //    ChanageState(EZombieState.Chase);
        //}
        //else if (distanceToTarget < attackRange)
        //{
        //    ChanageState(EZombieState.Attack);
        //}
        //else
        //{
        //    ChanageState(EZombieState.Patrol);
        //}

    }

    //private IEnumerator Evade()
    //{
    //    Debug.Log(gameObject.name + " : ������");
    //    animator.SetBool("IsWalking", true);
    //    Vector3 evadeDirection = (transform.position - target.position).normalized;
    //    float evadeTime = 3.0f;
    //    float timer = 0.0f;

    //    Quaternion targetRotation = Quaternion.LookRotation(evadeDirection);
    //    transform.rotation = targetRotation;

    //    while (currentState == EZombieState.Evade && timer < evadeTime)
    //    {
    //        transform.position += evadeDirection * moveSpeed * Time.deltaTime;
    //        timer += Time.deltaTime;
    //        yield return null;
    //    }

    //    ChanageState(EZombieState.ZombieIdle);
    //}
    public IEnumerator TakeDamage(float damage)
    {
        animator.SetTrigger("Damage");
        zombieHP -= damage;
        Debug.Log(gameObject.name + $" {damage} ������ ����, HP : {zombieHP}");
        if (zombieHP <= 0)
        {
            ChanageState(EZombieState.Die);
        }
        else
        {
            ChanageState(EZombieState.Chase);
        }
        yield return null;
    }
    private IEnumerator Die()
    {
        Debug.Log(gameObject.name + " ���");
        animator.SetTrigger("Die");
        yield return new WaitForSeconds(2.0f);
        gameObject.SetActive(false);
    }
}

public enum EZombieState
{
    Patrol,
    Chase,
    Attack,
    ZombieIdle,
    Evade,
    TakeDamage,
    Die,

}