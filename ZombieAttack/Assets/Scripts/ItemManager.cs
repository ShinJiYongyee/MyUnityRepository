using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private void Start()
    {
        CheckForDuplicate();
    }

    void Update()
    {
        transform.Rotate(0, Time.deltaTime % 360 * 60, 0);
    }

    private void CheckForDuplicate()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 0.1f);
        foreach (Collider col in colliders)
        {
            if (col.gameObject != gameObject && col.gameObject.layer == LayerMask.NameToLayer("Item"))
            {
                Destroy(gameObject);
                return;
            }
        }
    }

    private void OnTriggerEnter(Collider other) // Trigger 충돌 감지
    {
        if (other.CompareTag("Player"))
        {
            PlayerManager playerManager = other.GetComponent<PlayerManager>();
            if (playerManager != null)
            {
                playerManager.hasM4Item = true;
                playerManager.weaponIconObj.SetActive(true);
                playerManager.totalBullet += 30;
                Destroy(gameObject);
            }
        }
    }
}
