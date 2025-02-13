using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    //몬스터의 드랍 테이블
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
        //드랍 테이블 내 아이템을 랜덤으로 선택
        GameObject dropItemPrefab = 
            DropTable.drop_table[Random.Range(0,DropTable.drop_table.Count)];

        Instantiate(dropItemPrefab, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}
