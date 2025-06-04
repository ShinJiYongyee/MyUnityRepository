using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;



public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        List<UserInventoryData> items = new()
        {
            UserInventoryData.Acquire(11001),
            UserInventoryData.Acquire(12001),
            UserInventoryData.Acquire(13001),
            UserInventoryData.Acquire(14001),
            UserInventoryData.Acquire(15001)
        };

        string path = Path.Combine(Application.persistentDataPath, "UserInventoryData.json");
        
        IUserInventoryDataRepository repo = new TestUserInventoryDataRepository(items);

        InventoryService inventoryService = new InventoryService(repo);

        for (int i = 0; i < items.Count; i++)
        {
            Debug.Log($"{items}");
        }

        repo.Save();
    }

}