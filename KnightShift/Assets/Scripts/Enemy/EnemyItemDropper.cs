using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DropItem
{
    public GameObject itemPrefab; // ����� ������ ������

    [Range(0f, 1f)] 
    public float dropRate;        // ���� ������ ��� Ȯ�� (0~1)
}

public class EnemyItemDropper : MonoBehaviour
{
    [Header("Drop Settings")]
    //[Range(0f, 1f)]
    //public float dropChance = 0.5f;         // ������ ��� Ȯ��

    public DropItem[] dropItems;           // ��� ������ ������ ���

    private EnemyHealth enemyHealth;

    private GameObject itemWillDrop;
    void Start()
    {
        enemyHealth = GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.OnDeath += HandleDrop;  // ���� ��� �̺�Ʈ ����
        }
    }

    void HandleDrop()
    {
        //if (Random.value > dropChance) return;

        // ��� ������ �ĺ� ����Ʈ ����
        List<DropItem> possibleDrops = new List<DropItem>();

        foreach (var dropItem in dropItems)
        {
            if (dropItem.itemPrefab != null && Random.value <= dropItem.dropRate)
            {
                possibleDrops.Add(dropItem);
            }
        }

        // �ĺ��� �����ϸ� �������� �ϳ� ������ ���
        if (possibleDrops.Count > 0)
        {
            itemWillDrop = possibleDrops[Random.Range(0, possibleDrops.Count)].itemPrefab;
            Instantiate(itemWillDrop, transform.position, Quaternion.identity);

        }
    }

    void OnDestroy()
    {
        if (enemyHealth != null)
        {
            enemyHealth.OnDeath -= HandleDrop; // �̺�Ʈ ����
        }
    }
}
