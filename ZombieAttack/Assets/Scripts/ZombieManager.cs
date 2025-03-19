using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;
using UnityEngine.InputSystem.Android;

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
    public float zombieHP = 100.0f;
    private float distanceToTarget; //Target과의 거리 계산 값
    private bool isWaiting = false; //상태 전환 후 대기 상태 여부
    public float ZombieIdleTime = 2.0f; //각 상태 전환 후 대기 시간
    private Coroutine stateRoutine;

    Animator animator;

    private NavMeshAgent agent;

    private bool isAlive = true;

    public AudioSource audioSource;
    public AudioClip audioClipDamage;

    public GameObject hand;

    private void Start()
    {
        animator = GetComponent<Animator>();
        currentState = EZombieState.ZombieIdle;
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        distanceToTarget = Vector3.Distance(transform.position, target.position);
        ChangeCurrentIEnumeratorState();
    }

    void ChangeCurrentIEnumeratorState()
    {
        if (isAlive)
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
        else
        {
            ChanageState(EZombieState.Die);
            Invoke("RemoveCorpse", 8.0f); //별도의 float변수를 할당할 경우 상정한 것보다 빠른 시간 내 발동된다
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

                //이동 알고리즘
                //Transform targetPoint = patrolPoints[currentPoint];
                //Vector3 direction = (targetPoint.position - transform.position).normalized;
                //transform.position += direction * moveSpeed * Time.deltaTime;
                //transform.LookAt(targetPoint.transform);
                //if (Vector3.Distance(transform.position, targetPoint.position) < 0.3f)
                //{
                //    currentPoint = (currentPoint + 1) % patrolPoints.Length;
                //}

                //NavmeshAgent를 활용한 이동 알고리즘
                agent.speed = moveSpeed;    //ai의 속력 설정
                agent.isStopped = false;    //ai의 정지 여부 설정
                agent.destination = patrolPoints[currentPoint].position;  //ai의 목적지를 설정
                if (Vector3.Distance(transform.position, agent.destination) < 0.5f)
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
            animator.SetBool("IsWalking", true);

            //이동 알고리즘
            //float distance = Vector3.Distance(transform.position, target.position);
            //Vector3 direction = (target.position - transform.position).normalized;
            //transform.position += direction * moveSpeed * Time.deltaTime;
            //transform.LookAt(target.position);

            //NavmeshAgent를 활용한 이동 알고리즘
            agent.speed = moveSpeed;    //ai의 속력 설정
            agent.isStopped = false;    //ai의 정지 여부 설정
            agent.destination = target.position;  //ai의 목적지를 설정


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
                yield break;
            }

            if (!isAttacking)
            {

                isAttacking = true;
                Debug.Log("isAttacking : " + isAttacking);
                animator.SetTrigger("Attack");
                Debug.Log(gameObject.name + " : 공격중");

                //타겟 바라보기
                //transform.LookAt(target.position);

                //ai 제어로 타겟 바라보기
                agent.isStopped = true;
                agent.destination = target.position;

                audioSource.PlayOneShot(audioClipDamage);


            }
            yield return new WaitForSeconds(animationLength); // 공격 간격 (딜레이)
            isAttacking = false;
            Debug.Log("isAttacking : " + isAttacking);
            agent.isStopped = false;

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
        //animator.SetTrigger("Damage");
        zombieHP -= damage;
        Debug.Log(gameObject.name + $" {damage} 데미지 받음, HP : {zombieHP}");
        if (zombieHP <= 0)
        {
            Debug.Log(gameObject.name + " 죽음");
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
        if (!isAlive)
        {
            yield break;
        }
        Debug.Log(gameObject.name + " 사망 처리");
        animator.SetTrigger("Die");
        agent.isStopped = true;
        isAlive = false;
        yield return null;

    }
    private void RemoveCorpse()
    {
        Debug.Log("시체 제거");
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