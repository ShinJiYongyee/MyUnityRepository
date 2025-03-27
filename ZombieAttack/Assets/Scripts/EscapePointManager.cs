using UnityEngine;

public class EscapePointManager : MonoBehaviour
{
    public GameObject chopperPrefab;
    public Transform chopperSpawnPoint;
    private bool isChopperSpawned = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isChopperSpawned)
        {
            SpawnChopper();
        }
    }

    private void SpawnChopper()
    {
        GameObject chopper = Instantiate(chopperPrefab, chopperSpawnPoint.position, Quaternion.identity);
        isChopperSpawned=true;
        ChopperManager chopperManager = chopper.GetComponent<ChopperManager>();
        if (chopperManager != null)
        {
            chopperManager.StartDescent();
        }
    }
}
