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
        ////    // InventoryService�κ��� �����͸� ��� ����

        ////    // 60���� ������ ����
        ////    // �� ���
        ////    // �� ������
        ////    _infiniteScroll.InsertData(new InfiniteScrollData());
        ////}

        //foreach(UserInventoryData item in inventoryService.Items)
        //{
        //    // SerialNumber
        //    // itemId

        //    // 1. Itemrepository x => ���յ� ���
        //    // 2. id �̿� ���� x => �ڵ� �ߺ�

        //    // hint : ���� inventoryService ���� �̿��ϴ� ��

        //    // Item

        //}

        // InventoryService�� �̿��ؼ� UI ����

        // InviniteScrollItem�� ��� ���� InventoryItemSlot ��ũ��Ʈ �ʿ�

        // InventoryItemSlot�� �����͸� ������ InventoryItemSlotData�� �ʿ�.
        // �� InfiniteScrollData�� ���
    }

}
