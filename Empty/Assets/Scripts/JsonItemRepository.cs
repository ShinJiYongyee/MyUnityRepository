using NUnit.Framework;
using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    None,
    Weapon,
    Sheild,
    ChestArmor,
    Gloves,
    Boots,
    Accessary
}

public enum ItemGrade
{
    None,
    Common,
    Uncommon,
    Rare,
    Epic,
    Legendary
}

// �츮 ���ӿ��� ����� ������ ��ü => ������ ��ü / ����Ƽ(Entity)
public sealed class Item
{
    private int _id;
    private string _name;
    private ItemType _type;
    private ItemGrade _grade;
    private int _atk;
    private int _def;

    public Item(int id, string name, int atk, int def)
    {
        _id = id;
        _name = name;
        _atk = atk;
        _def = def;

        _type = GetType(id);
        _grade = GetGrade(id);
    }

    public override string ToString()
    {
        return $"Item(id: {_id}, name: {_name}, type: {_type}, grade: {_grade}, atk: {_atk}, def: {_def})";
    }
    // ������ �ĺ���(id)�� �޾� ������ ��� ����
    private ItemType GetType(int id)
    {
        int value = id / 10000;
        switch (value)
        {
            case 1:
                return ItemType.Weapon;
            case 2:
                return ItemType.Sheild;
            case 3:
                return ItemType.ChestArmor;
            case 4:
                return ItemType.Gloves;
            case 5:
                return ItemType.Boots;
            case 6:
                return ItemType.Accessary;
            default:
                return ItemType.None;
        }
    }

    private ItemGrade GetGrade(int id)
    {
        int value = id % 10000 / 1000;
        switch (value)
        {
            case 1:
                return ItemGrade.Common;
            case 2:
                return ItemGrade.Uncommon;
            case 3:
                return ItemGrade.Rare;
            case 4:
                return ItemGrade.Epic;
            case 5:
                return ItemGrade.Legendary;
            default:
                return ItemGrade.None;
        }
    }
}

public interface IItemRepository
{
    IReadOnlyList<Item> FindAll();
}

// Item ���� Persistency�� ����Ѵ�.
public class JsonItemRepository : IItemRepository
{
    private List<Item> _items;

    public JsonItemRepository()
    {
        LoadJson();
    }

    // ��ȯ Ÿ��
    public IReadOnlyList<Item> FindAll() => _items.AsReadOnly();

    // DTO; Data Transfer Object
    // �ܺο� �����ϱ� ���� ��ü. ����ȭ���� ���� ��ü. ������ ���۸��� ���� ��ü
    [Serializable]
    class ItemModel
    {
        public int item_id;
        public string item_name;
        public int attack_power;
        public int defense;
    }

    [Serializable]
    class ItemModelList
    {
        public ItemModel[] data;
    }

    void LoadJson()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("items");
        string json = jsonFile.text;
        ItemModelList itemModelList = JsonUtility.FromJson<ItemModelList>(json);

        _items = new List<Item>();
        foreach (ItemModel itemModel in itemModelList.data)
        {
            _items.Add(new Item(itemModel.item_id, itemModel.item_name, itemModel.attack_power, itemModel.defense));
        }
    }
}