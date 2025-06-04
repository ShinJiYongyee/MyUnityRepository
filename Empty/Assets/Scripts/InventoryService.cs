using System.Collections.Generic;

/// <summary>
/// 인벤토리의 기능
/// </summary>
public class InventoryService 
{
    // repo를 활용해 기능 구현
    private IUserInventoryDataRepository _userInventoryDataRepository; 
    // 의존 -> 의존성 주입 필요
    // 의존성 주입
    // 생성자/setter/method 사용

    /// <summary>
    /// 유저가 획득한 아이템
    /// </summary>
    /// UserInventoryDataRepository.FindAll()
    public IReadOnlyList<UserInventoryData> Items() => _userInventoryDataRepository.FindAll();

    public void Save() => _userInventoryDataRepository.Save();
    // 생성자를 통한 의존성 주입
    public InventoryService(IUserInventoryDataRepository userInventoryDataRepository)
    {
        _userInventoryDataRepository = userInventoryDataRepository;
    }
}