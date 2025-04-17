using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �������� ���� ���� ��ũ��Ʈ
/// ������ �Ҹ��� ���� ��ȭ �� �ʱ�ȭ
/// </summary>
public class ShopManager : MonoBehaviour
{
    // ���׷��̵� ���
    public int baseDamageCost = 25;
    public int baseAttackSpeedCost = 27;
    public int baseMoveSpeedCost = 10;
    public int baseHPCost = 10;

    // ���׷��̵� ��ġ
    public int damageUpgradeAmount = 5;
    public float attackSpeedUpgradeAmount = 0.2f;
    public float moveSpeedUpgradeAmount = 0.3f;
    public int HPUpgradeAmount = 10;

    // ���׷��̵� Ƚ�� ����
    private int damageUpgradeCount = 0;
    private int attackSpeedUpgradeCount = 0;
    private int moveSpeedUpgradeCount = 0;
    private int HPUpgradeCount = 0;

    // ���� ��� ����
    private const int increaseThreashold = 3;       // ���� ���׷��̵� ������ �����ϴ� ���׷��̵� Ƚ��
    private const float priceIncreaseRate = 1.5f;   // ��� �����

    // �÷��̾� ���� �ʱⰪ
    private int defaultMaxHP = 100;
    private float defaultDamage = 10.0f;
    private float defaultAttackSpeed = 1.0f;
    private float defaultMoveSpeed = 3.0f;

    private PlayerStats playerStats;
    private Text damageText;
    private Text coinText;

    void Start()
    {
        playerStats = PlayerStats.Instance;
    }

    private int GetCost(int baseCost, int upgradeCost)
    {
        if(upgradeCost < increaseThreashold)
        {
            return baseCost;
        }
        return Mathf.FloorToInt(baseCost * priceIncreaseRate);
    }

    public void UpgradeDamage()
    {
        int cost = GetCost(baseDamageCost, damageUpgradeCount);
        if (GameManager.Instance.UseCoin(cost))
        {
            playerStats.UpgradeDamage(damageUpgradeAmount);
            damageUpgradeCount++;
        }
    }
    public void UpgradeAttackSpeed()
    {
        int cost = GetCost(baseAttackSpeedCost, damageUpgradeCount);
        if (GameManager.Instance.UseCoin(cost))
        {
            playerStats.UpgradeDamage(attackSpeedUpgradeAmount);
            attackSpeedUpgradeCount++;
        }
    }
    public void UpgradeMoveSpeed()
    {
        int cost = GetCost(baseMoveSpeedCost, damageUpgradeCount);
        if (GameManager.Instance.UseCoin(cost))
        {
            playerStats.UpgradeDamage(moveSpeedUpgradeAmount);
            moveSpeedUpgradeCount++;
        }
    }
    public void UpgradeHP()
    {
        int cost = GetCost(baseHPCost, damageUpgradeCount);
        if (GameManager.Instance.UseCoin(cost))
        {
            playerStats.UpgradeDamage(HPUpgradeAmount);
            HPUpgradeCount++;
        }
    }

    public void ResetStats()
    {
        if (GameManager.Instance.UseCoin(100))
        {
            playerStats.maxHP = defaultMaxHP;
            playerStats.damage = defaultDamage;
            playerStats.attackSpeed = defaultAttackSpeed;
            playerStats.moveSpeed = defaultMoveSpeed;

            GameManager.Instance.SavePlayerStats(playerStats);
        }
    }

    public void UpdateUI()
    {
        damageText.text = playerStats.damage.ToString();
        coinText.text = GameManager.Instance.coinCount.ToString();
    }
}
