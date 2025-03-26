using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject spawnPrefab; // ������ ������
    public float spawnInterval = 3f; // ���� ���� ���� (��)
    public Transform[] spawnPoints; // ���� ������ ��ġ �迭
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
            Debug.LogWarning("������ �Ǵ� ���� ����Ʈ�� �������� �ʾҽ��ϴ�.");
            return;
        }

        int randomIndex = Random.Range(0, spawnPoints.Length);
        Instantiate(spawnPrefab, spawnPoints[randomIndex].position, Quaternion.identity, parent.transform);
        Debug.Log(spawnPrefab.name + "��/�� �����Ͽ����ϴ�.");
    }


}
