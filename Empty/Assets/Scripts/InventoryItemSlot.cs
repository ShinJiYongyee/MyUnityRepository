using Gpm.Ui;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemSlotData : InfiniteScrollData // Upcasting�� ���� ���
{
    public Sprite GradeBackgroundSprite { get; }
    public Sprite ItemIconSprite { get; }

}
public class InventoryItemSlot : InfiniteScrollItem
{
    // Inventory �� Item�� �����Ǿ� �ҽ� �̹����� ����
    [SerializeField] private Image _gradeBackground;
    [SerializeField] private Image _itemIcon;

    public override void UpdateData(InfiniteScrollData scrollData)
    {
        base.UpdateData(scrollData);

        InventoryItemSlotData data = scrollData as InventoryItemSlotData;

        // _gradeBackground�� _itemIcon�� �̹����� �����ϰ� �ٲ��ش�.
        // �� scrollData�� ������ �̹���, ������ �̹���
        _gradeBackground.sprite = data.GradeBackgroundSprite;
        _itemIcon.sprite = data.ItemIconSprite;
    }
}
