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
        // 식별자 규칙 : 획득한 시점의 y m d 난수 4자리
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
