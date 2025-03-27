using UnityEngine;

public class FenceGateManager : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerManager playerManager = other.GetComponent<PlayerManager>();
            if (playerManager != null && playerManager.hasFenceGateKey)
            {
                animator.SetTrigger("Open");
                Debug.Log("Fence Gate Opened!");
            }
        }
    }
}
