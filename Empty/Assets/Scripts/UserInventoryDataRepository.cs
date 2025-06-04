
// Repository : 객체의 persistency
// ㄴ 영속성 메모리에 객체 저장
// ㄴ 영속성 메모리로부터 객체 복원

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

[Serializable]
class UserInventoryDataModel
{
    public long serial_number;
    public int item_id;

    public static UserInventoryDataModel From(UserInventoryData data)
    {
        return new UserInventoryDataModel()
        {
            serial_number = data.SerialNumber,
            item_id = data.ItemId
        };
    }
    public static UserInventoryData ToDomain(UserInventoryDataModel model)
    {
        return new UserInventoryData(model.serial_number, model.item_id);
    }
}
[Serializable]
class UserInventoryDataModelList
{
    public List<UserInventoryData> data { get; internal set; }
}

public interface IUserInventoryDataRepository
{
    // 인벤토리 기능 x
    // 데이터 저장
    // 데이터로부터 내용을 복원
    IReadOnlyList<UserInventoryData> FindAll();
    void Save();
}

public class UserInventoryDataRepository : IUserInventoryDataRepository
{
    private List<UserInventoryData> _items;

    // 생성자에서 할 일 : 파일 입력을 통해 메모리로 파일 내용 로드
    public UserInventoryDataRepository(string path)
    {
        // json 텍스트 로드
        string json = File.ReadAllText(path);
        // json 파싱
        var modelList = JsonUtility.FromJson<UserInventoryDataModelList>(json);

        //_items = modelList.data.
        //    Select(model => UserInventoryDataModel.ToDomain(model)).ToList();
    }
    public IReadOnlyList<UserInventoryData> FindAll()
    {
        throw new NotImplementedException();
    }

    public void Save()
    {
        throw new NotImplementedException();
    }
}

// 테스트 데이터를 위한 레포지토리
public class TestUserInventoryDataRepository : IUserInventoryDataRepository
{
    private List<UserInventoryData> _items;
    private string _path;

    public TestUserInventoryDataRepository(List<UserInventoryData> items)
    {
        _items = items;
    }
    // 임시 객체를 반환하도록 만든다면?
    public IReadOnlyList<UserInventoryData> FindAll() => _items.AsReadOnly();

    public void Save()
    {
        // 1. json으로 변환
        var modelList = new List<UserInventoryData>();
        foreach (var item in _items)
        {
            //modelList.Add(new UserInventoryDataModel()
            //{
            //    serial_number = item.SerialNumber,
            //    item_id = item.ItemId,
            //});

            var dto = new UserInventoryDataModelList()
            {
                data = modelList
            };
            var json = JsonUtility.ToJson(dto);
        }

        // 2. 파일입출력 라이브러리 사용해서 저장
        using (var stream = File.OpenWrite(_path))
        {

        }
    }
}
