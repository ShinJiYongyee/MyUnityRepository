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

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerAttack"))
        {
            SpawnHitEffect(); // �ǰ� ����Ʈ ����
            PlayHitSound();
            GetDamage(collision);
            StartCoroutine(HitEffect());
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
            PlayerAttack playerAttack = collider2D.GetComponentInParent<PlayerAttack>();;
            enemyHealth -= playerAttack.playerDamage;
            Debug.Log($"Damage : {playerAttack.playerDamage} \nHealth left : {enemyHealth} ");
        }

    }
}
