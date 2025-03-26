using System.Collections;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;


public class ZombieManager : MonoBehaviour
{
    public EZombieState currentState;
    //public GameObject target; //���� ������ ����� PlayerManager.Instance�� ��ü
    public float attackRange = 1.0f; //���ݹ���
    public float attackDelay = 2.0f; //���ݵ�����
    //private float nextAttackTime = 0.0f; //���� ���� �ð�����
    public Transform[] patrolPoints; //���� ��� ������
    private int currentPoint = 0; //���� ���� ��� ���� �ε���
    public float moveSpeed = 2.0f;
    public float trackingRange = 3.0f; //���� ���� ����
    private bool isAttacking = false; //���� ����
    //private float evadeRange = 5.0f; //���� ���� ȸ�� �Ÿ�
    public float zombieHP = 100.0f;
    private float distanceToTarget; //Target���� �Ÿ� ��� ��
    //private bool isWaiting = false; //���� ��ȯ �� ��� ���� ����
    //public float ZombieIdleTime = 2.0f; //�� ���� ��ȯ �� ��� �ð�
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
            //Error("PlayerManager.Instance�� null�Դϴ�. PlayerManager�� ����� �ʱ�ȭ���� �ʾ��� ���ɼ��� �ֽ��ϴ�.");
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
            Invoke("RemoveCorpse", 8.0f); //������ float������ �Ҵ��� ��� ������ �ͺ��� ���� �ð� �� �ߵ��ȴ�
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
        //(gameObject.name + " : �����");
        animator.Play("ZombieIdle");

        yield return null;
    }


    private IEnumerator Patrol()
    {
        //(gameObject.name + " : ������");

        while (currentState == EZombieState.Patrol)
        {
            if (patrolPoints.Length > 0)
            {
                animator.SetBool("IsWalking", true);

                //NavmeshAgent�� Ȱ���� �̵� �˰���
                agent.speed = moveSpeed;    //ai�� �ӷ� ����
                agent.isStopped = false;    //ai�� ���� ���� ����
                agent.destination = patrolPoints[currentPoint].position;  //ai�� �������� ����

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
        //(gameObject.name + " ������");

        while (currentState == EZombieState.Chase)
        {
            animator.SetBool("IsWalking", true);


            //NavmeshAgent�� Ȱ���� �̵� �˰���
            agent.speed = moveSpeed;    //ai�� �ӷ� ����
            agent.isStopped = false;    //ai�� ���� ���� ����
            agent.destination = PlayerManager.Instance.transform.position;  //ai�� �������� ����

            yield return null;
        }
        yield return null;
    }

    private IEnumerator Attack()
    {
        if(PlayerManager.Instance.isAlive)
        {
            //(gameObject.name + " : ���� ����");
            float animationLength = attackDelay;

            while (currentState == EZombieState.Attack)
            {
                distanceToTarget = Vector3.Distance(transform.position, PlayerManager.Instance.transform.position);

                if (distanceToTarget > attackRange) // ���� ���� ����� ���� ���·�
                {
                    ChanageState(EZombieState.Chase);
                    yield break;
                }

                if (!isAttacking)
                {

                    isAttacking = true;
                    animator.SetTrigger("Attack");
                    //(gameObject.name + " : ������");

                    //ai ����� Ÿ�� �ٶ󺸱�
                    agent.isStopped = true;
                    agent.destination = PlayerManager.Instance.transform.position;

                }
                yield return new WaitForSeconds(animationLength); // ���� ���� (������)
                isAttacking = false;
                //("isAttacking : " + isAttacking);
                agent.isStopped = false;

                //yield return null; // ���� �����ӱ��� ���
            }

        }

    }

    public void GiveDamage()
    {
        // ���� ���� ���
        audioSource.PlayOneShot(audioClipAttack);

        // hand ������Ʈ �ֺ��� �浹 ���� (Trigger�� ������ Collider �ʿ�)
        Collider[] hitColliders = Physics.OverlapSphere(hand.transform.position, 0.5f);

        foreach (Collider hit in hitColliders)
        {
            // PlayerManager�� ���� ������Ʈ���� Ȯ��
            if (hit.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                PlayerManager player = hit.GetComponent<PlayerManager>();

                if (player != null)
                {
                    player.playerHP -= zombieDamage;
                    //($"�÷��̾ {zombieDamage}�� ���ظ� �Ծ����ϴ�. ���� HP: {player.playerHP}");

                    // HP�� 0 ���϶�� ��� ó�� (�ʿ� �� �߰� ����)
                    if (player.playerHP <= 0)
                    {
                        //("�÷��̾� ���");
                        // �÷��̾� ��� ó�� �Լ� ȣ�� ���� (��: player.Die();)
                    }
                }
            }
        }
    }


    public IEnumerator TakeDamage(float damage)
    {
        //animator.SetTrigger("Damage");
        zombieHP -= damage;
        //(gameObject.name + $" {damage} ������ ����, HP : {zombieHP}");
        audioSource.PlayOneShot(audioClipDamage);
        if (zombieHP <= 0)
        {
            //(gameObject.name + " ����");
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
        //(gameObject.name + " ��� ó��");
        audioSource.PlayOneShot(audioClipDie);
        animator.SetTrigger("Die");
        agent.isStopped = true;
        agent.enabled = false;
        isAlive = false;
        yield return null;

    }
    private void RemoveCorpse()
    {
        //("��ü ����");
        gameObject.SetActive(false);
    }

    private IEnumerator JumpAcrossLink()
    {
        //(gameObject.name + " ���� ����");

        isJumping = true;

        agent.isStopped = true;

        //NavMeshLink�� ���۰� �� ��ǥ�� ��������
        OffMeshLinkData linkData = agent.currentOffMeshLinkData;
        Vector3 startPos = linkData.startPos;
        Vector3 endPos = linkData.endPos;

        //���� ��� ���(�������� �׸��� ����)
        float elapsedTime = 0;
        while(elapsedTime < jumpDuration)
        {
            float t = elapsedTime/jumpDuration;
            Vector3 currentPosition = Vector3.Lerp(startPos, endPos, t);
            currentPosition.y += Mathf.Sin(t * Mathf.PI) * jumpHeight; //������ ���
            transform.position = currentPosition;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        //�������� ��ġ
        transform.position = endPos;

        //NavMeshAgent ��� �簳
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