using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    //������ ��� ���̺�
    public DropTable DropTable;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            Dead();
        }
    }

    private void Dead()
    {
        //��� ���̺� �� �������� �������� ����
        GameObject dropItemPrefab = 
            DropTable.drop_table[Random.Range(0,DropTable.drop_table.Count)];

        Instantiate(dropItemPrefab, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}
