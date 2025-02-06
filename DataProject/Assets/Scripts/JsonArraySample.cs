using JetBrains.Annotations;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Item
{
    //���� ������ ǥ��
    public string item_name;        //json���Ͽ��� ����ϴ� �̸�
    public int item_count;
}

[Serializable]
public class Inventory
{
    //������ ���� ǥ��
    public List<Item> inventory;    //json���Ͽ��� ����ϴ� �̸�
    //public Item[] inventory;        //���� ������ �ڵ�
}
public class JsonArraySample : MonoBehaviour
{

    void Start()
    {
        //�ؽ�Ʈ �������� ���������Ƿ� Ȯ���ڸ� ���ʿ�
        TextAsset textAsset = Resources.Load<TextAsset>("ItemInventory");

        //json������ ������ Ŭ������ ��ȯ
        Inventory inventory = JsonUtility.FromJson<Inventory>(textAsset.text);

        int total = 0;  //������ ��

        //foreach(Ÿ�� ���� in �迭/����Ʈ)
        //�迭/����Ʈ �� ������ ������ŭ �ݺ��ϴ� ���� ����
        foreach (Item item in inventory.inventory)
        {
            total += item.item_count;
        }

/*      ���� ������ �ڵ�  
        for (int i = 0; i < inventory.inventory.Count; i++)
        {
            total += inventory.inventory[i].item_count;
        }
*/

        Debug.Log(total);


    }

    void Update()
    {
        
    }

}
