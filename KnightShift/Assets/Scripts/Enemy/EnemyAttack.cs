using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public GameObject attackHitbox;
    public Animator animator;
    private GameObject player;

    public float attackRange = 2.5f; // ���� �ߵ� �Ÿ�
    public float attackCooldown = 2.0f; // ���� �� ��Ÿ��
    private float lastAttackTime;

    private void Start()
    {
        if (animator == null)
            animator = GetComponent<Animator>();

        // Player �±׷� ��� ã��
        player = GameObject.FindWithTag("Player");
        lastAttackTime = Time.time;
        attackHitbox.SetActive(false);
    }

    private void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= attackRange && Time.time >= lastAttackTime + attackCooldown)
        {
            animator.SetTrigger("Attack");
            lastAttackTime = Time.time;
        }
    }

    // �ִϸ��̼� �̺�Ʈ���� ȣ��
    public void StartAttack()
    {
        if (attackHitbox != null)
        {
            SoundManager.Instance.PlaySFX(SFXType.SwordSwing);
            attackHitbox.SetActive(true);
            Debug.Log("Deathbringer attack start");
        }
    }

    // �ִϸ��̼� �̺�Ʈ���� ȣ��
    public void StopAttack()
    {
        if (attackHitbox != null)
        {
            attackHitbox.SetActive(false);
            Debug.Log("Deathbringer attack stop");
        }
    }

    void OnDrawGizmosSelected()
    {
        // ���� ���� �ð�ȭ
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
