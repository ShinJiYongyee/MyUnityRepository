using System;
using UnityEngine;
using Random = System.Random;

public class UserInventoryData 
{
    private static readonly Random random = new Random();

    // getter
    public long SerialNumber { get; }
    public int ItemId { get; }

    public static UserInventoryData Acquire(int itemId)
    {
        // �ĺ��� ��Ģ : ȹ���� ������ y m d ���� 4�ڸ�
        long serialNumber = long.Parse(DateTime.Now.ToString("yyyymmdd") + random.Next(10000).ToString("D4"));
        
        return new UserInventoryData(serialNumber, itemId);
    }

    public UserInventoryData(long serialNumber, int itemId)
    {
        SerialNumber = serialNumber;
        ItemId = itemId;
    }

    public override string ToString()
    {
        return $"Inven Data : {SerialNumber}, {ItemId}";
    }
}
