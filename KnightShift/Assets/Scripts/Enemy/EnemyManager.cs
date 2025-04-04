using System.Collections;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private Color originalColor;
    private SpriteRenderer spriteRenderer;
    public float colorChangeDuration = 0.1f;

    public GameObject hitEffectPrefab; // �ǰ� ����Ʈ ������ �߰�

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerAttack"))
        {
            StartCoroutine(HitEffect());
            SpawnHitEffect(); // �ǰ� ����Ʈ ����
            PlayHitSound();
            Debug.Log("Player hits the enemy");
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
}
