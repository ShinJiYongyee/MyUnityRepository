using UnityEngine;

public class ZombieManager : MonoBehaviour
{
    PlayerManager playerManager;
    Animator playerAnimator;

    public enum EZombieState
    {
        Patrol,
        Chase,
        Attack,
        Evade,
        Damage,
        Idle,
        Die
    }

    public EZombieState currentState = EZombieState.Idle;   //좀비 상태 열거형 저장(기본: 대기)
    public Transform target;
    public float attackRange = 1.0f;        //공격 범위
    public float attackDelay = 2.0f;        //공격 딜레이
    private float nextAttackTime = 0.0f;    //다음 공격 시간
    public Transform[] patrolPoints;        //순찰 지점
    public float moveSpeed = 2.0f;          //이동 속도
    private int currentPoint = 0;           //현재 순찰 지점
    private float trackingRange = 3.0f;     //추적 범위
    private bool isAttack = false;          //공격 중인지 여부
    private float evadeRange = 5.0f;        //회피 거리
    private float zombieHP = 10.0f;
    private float distanceToTarget;         //표적과의 거리
    private bool isWaiting = false;         //상태 전환 후 대기 여부
    private float idleTime = 2.0f;          //상태 전환 후 대기 시간

    private void Start()
    {

    }
    private void Update()
    {
        CheckAlive();

        //표적과의 거리 탐지
        distanceToTarget = Vector3.Distance(transform.position, target.position);
        Debug.Log("distance to target : " + distanceToTarget);

        ChangeCurrentState();

        switch (currentState)
        {
            case EZombieState.Idle:
                Idle(); break;
            case EZombieState.Attack:
                Attack(); break;
            case EZombieState.Evade:
                Evade(); break;
            case EZombieState.Patrol:
                Patrol(); break;
            case EZombieState.Chase:
                Chase(target); break;
            case EZombieState.Die:
                break;
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        //(collision.gameObject.name);
    }
    private void OnTriggerEnter(Collider other)
    {
        playerManager = other.GetComponent<PlayerManager>();
        playerAnimator = other.GetComponent<Animator>();
        //플레이어 충돌시 사운드 재생
        if (playerManager != null)
        {
            playerManager.audioSource.PlayOneShot(playerManager.audioClipFire);
        }
        //플레이어 충돌시 피격 애니메이션 재생(레이어 우선순위 조절)
        if (playerAnimator != null)
        {
            playerAnimator.SetLayerWeight(1, 1);
            playerAnimator.SetTrigger("GettingHit");
            Invoke("ResetLayerWeight", 2.0f);
        }

        //플레이어 충돌시 특정 좌표로 이동
        if (other.gameObject.tag == "Player")
        {
            //player위치를 다루는 함수 비활성화
            playerManager.characterController.enabled = false;
            other.gameObject.transform.position = new Vector3(0, 0, 0);
            playerManager.characterController.enabled = true;
        }
        else
        {
            other.GetComponent<MeshRenderer>().material.color = Color.red;
        }

    }
    void CheckAlive()
    {
        if (zombieHP == 0)
        {
            //("target destroyed");
            Destroy(gameObject);
        }
    }
    public void TakeDamage(float damage)
    {
        zombieHP -= damage;
        if (zombieHP < 0) zombieHP = 0; // HP가 음수가 되지 않도록 제한
        //("remain HP : " + zombieHP);
    }
    void ChangeCurrentState()
    {
        if (distanceToTarget < trackingRange && distanceToTarget > attackRange)       //추적
        {
            currentState = EZombieState.Chase;
        }
        else if (distanceToTarget < attackRange)    //공격
        {
            currentState = EZombieState.Attack;
        }
        else                                        //순찰
        {
            if (patrolPoints.Length > 0)
            {
                currentState = EZombieState.Patrol;
            }
            else
            {
                currentState = EZombieState.Idle;
            }
        }

    }
    void Patrol()
    {
        if (patrolPoints.Length > 0)
        {
            Debug.Log("순찰중");
            Transform targetPoint = patrolPoints[currentPoint];
            Vector3 direction = (targetPoint.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
            transform.LookAt(targetPoint.position);

            if (Vector3.Distance(transform.position, targetPoint.position) < 0.3f)
            {
                currentPoint = (currentPoint + 1) % patrolPoints.Length;
            }
        }
    }
    void Chase(Transform target)
    {
        if (distanceToTarget < trackingRange)       //추적
        {
            Debug.Log("추적");
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position = transform.position + direction * moveSpeed * Time.deltaTime;
            transform.forward = (target.position - transform.position).normalized;
        }
    }
    void Evade()
    {
        Debug.Log("도주");
    }
    void Idle()
    {
        Debug.Log("대기");
    }
    void Attack()
    {
        Debug.Log("공격");
    }
}