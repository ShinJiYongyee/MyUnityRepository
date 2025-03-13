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
        //("distance to target : " + distanceToTarget);

        if (distanceToTarget < trackingRange)       //����
        {
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position = transform.position + direction * moveSpeed * Time.deltaTime;
            transform.forward = (target.position - transform.position).normalized;
            //("����");
        }
        else if (distanceToTarget < attackRange)    //����
        {
            //("����");
        }
        else                                        //����
        {
            if (patrolPoints.Length > 0) 
            {
                //("������");
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
    void ResetLayerWeight()
    {
        playerAnimator.SetLayerWeight(1, 0);
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

}


