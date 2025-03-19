using System.Collections;
using UnityEngine;

public class ZombieManager : MonoBehaviour
{
    public EZombieState currentState;
    public Transform target;
    public float attackRange = 1.0f; //공격범위
    public float attackDelay = 2.0f; //공격딜레이
    private float nextAttackTime = 0.0f; //다음 공격 시간관리
    public Transform[] patrolPoints; //순찰 경로 지점들
    private int currentPoint = 0; //현재 순찰 경로 지점 인덱스
    public float moveSpeed = 2.0f;
    private float trackingRange = 3.0f; //추적 범위 설정
    private bool isAttacking = false; //공격 상태
    private float evadeRange = 5.0f; //도망 상태 회피 거리
    private float zombieHP = 100.0f;
    private float distanceToTarget; //Target과의 거리 계산 값
    private bool isWaiting = false; //상태 전환 후 대기 상태 여부
    public float ZombieIdleTime = 2.0f; //각 상태 전환 후 대기 시간
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
        Debug.Log(gameObject.name + " : 대기중");
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
        Debug.Log(gameObject.name + " : 순찰중");

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
        Debug.Log(gameObject.name + " 추적중");

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
        Debug.Log(gameObject.name + " : 공격 시작");
        float animationLength = attackDelay;

        while (currentState == EZombieState.Attack)
        {
            distanceToTarget = Vector3.Distance(transform.position, target.position);

            if (distanceToTarget > attackRange) // 공격 범위 벗어나면 추적 상태로
            {
                ChanageState(EZombieState.Chase);
            }

            if (!isAttacking)
            {
                isAttacking = true;
                Debug.Log("isAttacking : " + isAttacking);
                animator.SetTrigger("Attack");
                Debug.Log(gameObject.name + " : 공격중");
                transform.LookAt(target.position); // 타겟 바라보기

                //AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
                //animationLength = stateInfo.length;

            }
            yield return new WaitForSeconds(animationLength); // 공격 간격 (딜레이)
            isAttacking = false;
            Debug.Log("isAttacking : " + isAttacking);

            //yield return null; // 다음 프레임까지 대기
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
    //    Debug.Log(gameObject.name + " : 도망중");
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
        Debug.Log(gameObject.name + $" {damage} 데미지 받음, HP : {zombieHP}");
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
        Debug.Log(gameObject.name + " 사망");
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