using UnityEngine;

public class EscapePointManager : MonoBehaviour
{
    public GameObject chopperPrefab;
    public Transform chopperSpawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SpawnChopper();
        }
    }

    private void SpawnChopper()
    {
        GameObject chopper = Instantiate(chopperPrefab, chopperSpawnPoint.position, Quaternion.identity);
        ChopperManager chopperManager = chopper.GetComponent<ChopperManager>();
        if (chopperManager != null)
        {
            chopperManager.StartDescent();
        }
    }
}
