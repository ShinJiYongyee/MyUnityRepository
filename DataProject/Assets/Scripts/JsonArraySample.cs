using JetBrains.Annotations;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Item
{
    //개별 아이템 표현
    public string item_name;        //json파일에서 사용하는 이름
    public int item_count;
}

[Serializable]
public class Inventory
{
    //아이템 묶음 표현
    public List<Item> inventory;    //json파일에서 사용하는 이름
    //public Item[] inventory;        //위와 동일한 코드
}
public class JsonArraySample : MonoBehaviour
{

    void Start()
    {
        //텍스트 에셋으로 가져왔으므로 확장자명 불필요
        TextAsset textAsset = Resources.Load<TextAsset>("ItemInventory");

        //json파일을 가져와 클래스로 변환
        Inventory inventory = JsonUtility.FromJson<Inventory>(textAsset.text);

        int total = 0;  //아이템 수

        //foreach(타입 변수 in 배열/리스트)
        //배열/리스트 내 데이터 개수만큼 반복하는 전용 문법
        foreach (Item item in inventory.inventory)
        {
            total += item.item_count;
        }

/*      위와 동일한 코드  
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
