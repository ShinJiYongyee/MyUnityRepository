using System.Collections;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;


public class ZombieManager : MonoBehaviour
{
    public EZombieState currentState;
    //public GameObject target; //전역 변수로 선언된 PlayerManager.Instance로 대체
    public float attackRange = 1.0f; //공격범위
    public float attackDelay = 2.0f; //공격딜레이
    //private float nextAttackTime = 0.0f; //다음 공격 시간관리
    public Transform[] patrolPoints; //순찰 경로 지점들
    private int currentPoint = 0; //현재 순찰 경로 지점 인덱스
    public float moveSpeed = 2.0f;
    public float trackingRange = 3.0f; //추적 범위 설정
    private bool isAttacking = false; //공격 상태
    //private float evadeRange = 5.0f; //도망 상태 회피 거리
    public float zombieHP = 100.0f;
    private float distanceToTarget; //Target과의 거리 계산 값
    //private bool isWaiting = false; //상태 전환 후 대기 상태 여부
    //public float ZombieIdleTime = 2.0f; //각 상태 전환 후 대기 시간
    private Coroutine stateRoutine;

    Animator animator;

    private NavMeshAgent agent;

    private bool isAlive = true;

    public AudioSource audioSource;
    public AudioClip audioClipDamage;

    public GameObject hand;

    private bool isJumping = false;
    private Rigidbody rb;
    public float jumpHeight = 2.0f;
    public float jumpDuration = 1.0f;
    private NavMeshLink[] navMeshLinks;

    public AudioClip audioClipDamaged;
    public AudioClip audioClipAttack;
    public AudioClip audioClipDie;

    public float zombieDamage = 20.0f;

    [System.Obsolete]
    private void Start()
    {
        animator = GetComponent<Animator>();
        currentState = EZombieState.ZombieIdle;
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }
        rb.isKinematic = true;
        navMeshLinks = FindObjectsOfType<NavMeshLink>();
    }

    void Update()
    {
        if (PlayerManager.Instance == null)
        {
            //Error("PlayerManager.Instance가 null입니다. PlayerManager가 제대로 초기화되지 않았을 가능성이 있습니다.");
            return;
        }
        distanceToTarget = Vector3.Distance(transform.position, PlayerManager.Instance.transform.position);
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
        if(isJumping) return;

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
            case EZombieState.Die:
                stateRoutine = StartCoroutine(Die());
                break;
        }
    }

    private IEnumerator ZombieIdle()
    {
        //(gameObject.name + " : 대기중");
        animator.Play("ZombieIdle");

        yield return null;
    }


    private IEnumerator Patrol()
    {
        //(gameObject.name + " : 순찰중");

        while (currentState == EZombieState.Patrol)
        {
            if (patrolPoints.Length > 0)
            {
                animator.SetBool("IsWalking", true);

                //NavmeshAgent를 활용한 이동 알고리즘
                agent.speed = moveSpeed;    //ai의 속력 설정
                agent.isStopped = false;    //ai의 정지 여부 설정
                agent.destination = patrolPoints[currentPoint].position;  //ai의 목적지를 설정

                if (agent.isOnOffMeshLink)
                {
                    StartCoroutine(JumpAcrossLink());
                }
                if (Vector3.Distance(transform.position, agent.destination) < 0.5f)
                {
                    currentPoint = (currentPoint + 1) % patrolPoints.Length;
                }

            }
            yield return null;
        }
        yield return null;
    }
    private IEnumerator Chase()
    {
        //(gameObject.name + " 추적중");

        while (currentState == EZombieState.Chase)
        {
            animator.SetBool("IsWalking", true);


            //NavmeshAgent를 활용한 이동 알고리즘
            agent.speed = moveSpeed;    //ai의 속력 설정
            agent.isStopped = false;    //ai의 정지 여부 설정
            agent.destination = PlayerManager.Instance.transform.position;  //ai의 목적지를 설정

            yield return null;
        }
        yield return null;
    }

    private IEnumerator Attack()
    {
        if(PlayerManager.Instance.isAlive)
        {
            //(gameObject.name + " : 공격 시작");
            float animationLength = attackDelay;

            while (currentState == EZombieState.Attack)
            {
                distanceToTarget = Vector3.Distance(transform.position, PlayerManager.Instance.transform.position);

                if (distanceToTarget > attackRange) // 공격 범위 벗어나면 추적 상태로
                {
                    ChanageState(EZombieState.Chase);
                    yield break;
                }

                if (!isAttacking)
                {

                    isAttacking = true;
                    animator.SetTrigger("Attack");
                    //(gameObject.name + " : 공격중");

                    //ai 제어로 타겟 바라보기
                    agent.isStopped = true;
                    agent.destination = PlayerManager.Instance.transform.position;

                }
                yield return new WaitForSeconds(animationLength); // 공격 간격 (딜레이)
                isAttacking = false;
                //("isAttacking : " + isAttacking);
                agent.isStopped = false;

                //yield return null; // 다음 프레임까지 대기
            }

        }

    }

    public void GiveDamage()
    {
        // 공격 사운드 재생
        audioSource.PlayOneShot(audioClipAttack);

        // hand 오브젝트 주변의 충돌 감지 (Trigger로 설정된 Collider 필요)
        Collider[] hitColliders = Physics.OverlapSphere(hand.transform.position, 0.5f);

        foreach (Collider hit in hitColliders)
        {
            // PlayerManager를 가진 오브젝트인지 확인
            if (hit.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                PlayerManager player = hit.GetComponent<PlayerManager>();

                if (player != null)
                {
                    player.playerHP -= zombieDamage;
                    //($"플레이어가 {zombieDamage}의 피해를 입었습니다. 남은 HP: {player.playerHP}");

                    // HP가 0 이하라면 사망 처리 (필요 시 추가 가능)
                    if (player.playerHP <= 0)
                    {
                        //("플레이어 사망");
                        // 플레이어 사망 처리 함수 호출 가능 (예: player.Die();)
                    }
                }
            }
        }
    }


    public IEnumerator TakeDamage(float damage)
    {
        //animator.SetTrigger("Damage");
        zombieHP -= damage;
        //(gameObject.name + $" {damage} 데미지 받음, HP : {zombieHP}");
        audioSource.PlayOneShot(audioClipDamage);
        if (zombieHP <= 0)
        {
            //(gameObject.name + " 죽음");
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
        //(gameObject.name + " 사망 처리");
        audioSource.PlayOneShot(audioClipDie);
        animator.SetTrigger("Die");
        agent.isStopped = true;
        agent.enabled = false;
        isAlive = false;
        yield return null;

    }
    private void RemoveCorpse()
    {
        //("시체 제거");
        gameObject.SetActive(false);
    }

    private IEnumerator JumpAcrossLink()
    {
        //(gameObject.name + " 좀비 점프");

        isJumping = true;

        agent.isStopped = true;

        //NavMeshLink의 시작과 끝 좌표를 가져오기
        OffMeshLinkData linkData = agent.currentOffMeshLinkData;
        Vector3 startPos = linkData.startPos;
        Vector3 endPos = linkData.endPos;

        //점프 경로 계산(포물선을 그리며 점프)
        float elapsedTime = 0;
        while(elapsedTime < jumpDuration)
        {
            float t = elapsedTime/jumpDuration;
            Vector3 currentPosition = Vector3.Lerp(startPos, endPos, t);
            currentPosition.y += Mathf.Sin(t * Mathf.PI) * jumpHeight; //포물선 경로
            transform.position = currentPosition;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        //도착점에 위치
        transform.position = endPos;

        //NavMeshAgent 경로 재개
        agent.CompleteOffMeshLink();
        agent.isStopped = false;
        isJumping = false;
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