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

    public EZombieState currentState = EZombieState.Idle;   //���� ���� ������ ����(�⺻: ���)
    public Transform target;
    public float attackRange = 1.0f;        //���� ����
    public float attackDelay = 2.0f;        //���� ������
    private float nextAttackTime = 0.0f;    //���� ���� �ð�
    public Transform[] patrolPoints;        //���� ����
    public float moveSpeed = 2.0f;          //�̵� �ӵ�
    private int currentPoint = 0;           //���� ���� ����
    private float trackingRange = 3.0f;     //���� ����
    private bool isAttack = false;          //���� ������ ����
    private float evadeRange = 5.0f;        //ȸ�� �Ÿ�
    private float zombieHP = 10.0f;
    private float distanceToTarget;         //ǥ������ �Ÿ�
    private bool isWaiting = false;         //���� ��ȯ �� ��� ����
    private float idleTime = 2.0f;          //���� ��ȯ �� ��� �ð�

    private void Start()
    {

    }
    private void Update()
    {
        CheckAlive();

        //ǥ������ �Ÿ� Ž��
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
        //�÷��̾� �浹�� ���� ���
        if (playerManager != null)
        {
            playerManager.audioSource.PlayOneShot(playerManager.audioClipFire);
        }
        //�÷��̾� �浹�� �ǰ� �ִϸ��̼� ���(���̾� �켱���� ����)
        if (playerAnimator != null)
        {
            playerAnimator.SetLayerWeight(1, 1);
            playerAnimator.SetTrigger("GettingHit");
            Invoke("ResetLayerWeight", 2.0f);
        }

        //�÷��̾� �浹�� Ư�� ��ǥ�� �̵�
        if (other.gameObject.tag == "Player")
        {
            //player��ġ�� �ٷ�� �Լ� ��Ȱ��ȭ
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
        if (zombieHP < 0) zombieHP = 0; // HP�� ������ ���� �ʵ��� ����
        //("remain HP : " + zombieHP);
    }
    void ChangeCurrentState()
    {
        if (distanceToTarget < trackingRange && distanceToTarget > attackRange)       //����
        {
            currentState = EZombieState.Chase;
        }
        else if (distanceToTarget < attackRange)    //����
        {
            currentState = EZombieState.Attack;
        }
        else                                        //����
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
            Debug.Log("������");
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
        if (distanceToTarget < trackingRange)       //����
        {
            Debug.Log("����");
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position = transform.position + direction * moveSpeed * Time.deltaTime;
            transform.forward = (target.position - transform.position).normalized;
        }
    }
    void Evade()
    {
        Debug.Log("����");
    }
    void Idle()
    {
        Debug.Log("���");
    }
    void Attack()
    {
        Debug.Log("����");
    }
}