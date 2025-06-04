using Gpm.Ui;
using System.IO;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    //public int Count = 1000;

    [SerializeField] private InfiniteScroll _infiniteScroll;

    void Start()
    {
        string path = Path.Combine(Application.persistentDataPath, "UserInventoryData.json");

        //IUserInventoryDataRepository repo = new TestUserInventoryDataRepository(path);

        //InventoryService inventoryService = new InventoryService(repo);

        ////for (int i = 0; i < Count; i++)
        ////{
        ////    // InventoryService로부터 데이터를 들고 오기

        ////    // 60개의 아이템 정보
        ////    // ㄴ 등급
        ////    // ㄴ 아이콘
        ////    _infiniteScroll.InsertData(new InfiniteScrollData());
        ////}

        //foreach(UserInventoryData item in inventoryService.Items)
        //{
        //    // SerialNumber
        //    // itemId

        //    // 1. Itemrepository x => 결합도 상승
        //    // 2. id 이용 세팅 x => 코드 중복

        //    // hint : 오직 inventoryService 만을 이용하는 것

        //    // Item

        //}

        // InventoryService를 이용해서 UI 구성

        // InviniteScrollItem을 상속 받은 InventoryItemSlot 스크립트 필요

        // InventoryItemSlot의 데이터를 정의한 InventoryItemSlotData가 필요.
        // ㄴ InfiniteScrollData를 상속
    }

}
