using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private Color originalColor;
    private SpriteRenderer spriteRenderer;
    public float colorChangeDuration = 0.1f;

    public GameObject hitEffectPrefab; // �ǰ� ����Ʈ ������ �߰�

    public float enemyHealth = 100.0f;

    public Animator animator;
    public bool isAlive = true;

    private CollisionDamage collisionDamage;

    // ���� ����� ����Ǵ� delegate ��� �̺�Ʈ
    public delegate void DeathDelegate();
    public event DeathDelegate OnDeath;

    private DeathbringerManager deathbringerManager;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        if (animator != null)
        {
            animator = GetComponent<Animator>();
        }
        collisionDamage = GetComponentInChildren<CollisionDamage>();
        deathbringerManager = GameObject.FindAnyObjectByType<DeathbringerManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerAttack"))
        {
            SpawnHitEffect(); // �ǰ� ����Ʈ ����
            PlayHitSound();
            GetDamage(collision);
        }
    }

    IEnumerator HitEffect()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(colorChangeDuration);
        spriteRenderer.color = originalColor;
    }

    void SpawnHitEffect()
    {
        if (hitEffectPrefab != null)
        {
            GameObject effect = Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);
            Destroy(effect, 0.5f); // 0.5�� �� �ڵ� ����
        }
    }

    void PlayHitSound()
    {
        SoundManager.Instance.PlaySFX(SFXType.MonsterDamaged);
    }

    void GetDamage(Collider2D collider2D)
    {
        if (collider2D != null)
        {
            PlayerAttack playerAttack = collider2D.GetComponentInParent<PlayerAttack>(); ;
            enemyHealth -= playerAttack.playerDamage;
            //Debug.Log($"Damage : {playerAttack.playerDamage} \nHealth left : {enemyHealth} ");
            
            if(enemyHealth <= 0)
            {
                Die();
            }
            else
            {
                StartCoroutine(HitEffect());
            }

        }

    }

    // ��� ���� �� 
    // 1. ��Ʈ�ڽ� ��Ȱ��ȭ
    // 2. ��� �ִϸ��̼� ���
    // 3. �ִϸ��̼��� ����ϱ� ����� �ð��� ���� �� ��ü ����
    private void Die()
    {
        // ���� ���� ��� ��� + ������ ���� �ߺ� ���� ����
        if (!isAlive)
        {
            return;
        }

        isAlive = false;
        collisionDamage.isActive = false;
        animator.SetTrigger("Die");

        // �̺�Ʈ �߻�
        if (OnDeath != null)
        {
            OnDeath.Invoke();   // �̺�Ʈ �߻� �� ��ϵ� ��� �Լ� �����
            OnDeath = null;     // �̺�Ʈ�� �� ���� ȣ��ǵ���
        }

        // �������� ���� ��� �����긵�� ���� ���ð� ����
        if(gameObject.tag == "Slime" && deathbringerManager != null)
        {
            deathbringerManager.OnSlimeDied();
        }

        if (gameObject.tag == "Deathbringer" && deathbringerManager != null)
        {
            VictoryManager.instance.IsVictory();
        }

        Destroy(gameObject, 1.0f);
    }

}
