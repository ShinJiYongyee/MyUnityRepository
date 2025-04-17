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

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        if (animator != null)
        {
            animator = GetComponent<Animator>();
        }
        collisionDamage = GetComponentInChildren<CollisionDamage>();
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
            Debug.Log($"Damage : {playerAttack.playerDamage} \nHealth left : {enemyHealth} ");
            
            // ��� ���� �� 
            // 1. ��Ʈ�ڽ� ��Ȱ��ȭ
            // 2. ��� �ִϸ��̼� ���
            // 3. �ִϸ��̼��� ����ϱ� ����� �ð��� ���� �� ��ü ����
            if(enemyHealth <= 0)
            {
                isAlive = false;
                collisionDamage.isActive = false;
                animator.SetTrigger("Die");
                Destroy(gameObject, 1.0f);
            }

            StartCoroutine(HitEffect());

        }

    }

}
