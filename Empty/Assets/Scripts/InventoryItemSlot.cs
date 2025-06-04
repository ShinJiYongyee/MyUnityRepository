using Gpm.Ui;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemSlotData : InfiniteScrollData // Upcasting을 위한 상속
{
    public Sprite GradeBackgroundSprite { get; }
    public Sprite ItemIconSprite { get; }

}
public class InventoryItemSlot : InfiniteScrollItem
{
    // Inventory 내 Item에 부착되어 소스 이미지를 변경
    [SerializeField] private Image _gradeBackground;
    [SerializeField] private Image _itemIcon;

    public override void UpdateData(InfiniteScrollData scrollData)
    {
        base.UpdateData(scrollData);

        InventoryItemSlotData data = scrollData as InventoryItemSlotData;

        // _gradeBackground와 _itemIcon의 이미지를 적절하게 바꿔준다.
        // ㄴ scrollData에 프레임 이미지, 아이콘 이미지
        _gradeBackground.sprite = data.GradeBackgroundSprite;
        _itemIcon.sprite = data.ItemIconSprite;
    }
}
