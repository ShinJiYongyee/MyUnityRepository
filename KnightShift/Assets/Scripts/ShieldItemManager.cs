using UnityEngine;

public class ShieldItemManager : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            playerHealth.shieldCount++;
            SoundManager.Instance.PlaySFX(SFXType.Blocked);
            Destroy(gameObject);

        }
    }
}
