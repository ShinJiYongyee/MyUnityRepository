using UnityEngine;

public class FenceGateKeyManager : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerManager playerManager = other.GetComponent<PlayerManager>();
            if (playerManager != null)
            {
                playerManager.hasFenceGateKey = true;
                Debug.Log("Fence Gate Key Acquired!");
                Destroy(gameObject); // 키를 얻으면 제거
            }
        }
    }
}
