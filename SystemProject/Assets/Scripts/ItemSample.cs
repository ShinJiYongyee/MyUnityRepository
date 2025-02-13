using System;
using UnityEngine;

public class ItemSample : MonoBehaviour
{
    //������ ���
    public Item item;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ItemInfo();
        }
        
    }

    private void ItemInfo()
    {
        Debug.Log(item.name);   //��ũ���ͺ� ������Ʈ�� ���� �� �ٿ��� �̸�
        Debug.Log(item.id);     //������ ����
        Debug.Log(item.description);
        Debug.Log(item.price);

    }
}
