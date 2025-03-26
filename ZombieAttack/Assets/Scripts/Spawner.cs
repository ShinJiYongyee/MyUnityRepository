using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject spawnPrefab; // 생성할 프리팹
    public float spawnInterval = 3f; // 좀비 생성 간격 (초)
    public Transform[] spawnPoints; // 좀비가 생성될 위치 배열
    public GameObject parent;
    private bool spawnAvaliable;

    void Start()
    {
        spawnAvaliable = true;
        StartCoroutine(SpawnZombies());
    }

    IEnumerator SpawnZombies()
    {
        while (spawnAvaliable)
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnZombie();
        }
    }

    void SpawnZombie()
    {
        if (spawnPrefab == null || spawnPoints.Length == 0)
        {
            Debug.LogWarning("프리팹 또는 스폰 포인트가 설정되지 않았습니다.");
            return;
        }

        int randomIndex = Random.Range(0, spawnPoints.Length);
        Instantiate(spawnPrefab, spawnPoints[randomIndex].position, Quaternion.identity, parent.transform);
        Debug.Log(spawnPrefab.name + "을/를 생성하였습니다.");
    }


}
