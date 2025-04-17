using UnityEngine;

/// <summary>
/// �������� ���� �÷��̾� ���� ��ũ��Ʈ
/// </summary>
public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;

    [Header("�÷��̾� �ɷ�ġ")]
    public int maxHP = 100;
    public int currentHP;
    public float damage = 10.0f;
    public float attackSpeed = 1.0f;
    public float moveSpeed = 3.0f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
        currentHP = maxHP;
    }
    void Start()
    {
        GameManager.Instance.LoadPlayerState(this);
    }

    public void TakeDamage(int amount)
    {
        SoundManager.Instance.PlaySFX(SFXType.PlayerDamaged);
        currentHP -= amount;
        if (currentHP <= 0)
        {
            //����
        }
    }

    public void Heal(int amount)
    {
        currentHP += amount;
        if(currentHP > maxHP)
        {
            currentHP = maxHP;
        }
    }

    public void Die()
    {
        //Gameover â ����
    }

    public float GetDamage() => damage;

    public float GetAttackSpeed() => attackSpeed;

    public void UpgradeDamage(float amount)
    {
        damage += amount;
        GameManager.Instance.SavePlayerStats(this);
    }
    public void UpgradeAttackSpeed(int amount)
    {
        attackSpeed += amount;
        GameManager.Instance.SavePlayerStats(this);
    }
    public void UpgradeHP(int amount)
    {
        maxHP += amount;
        GameManager.Instance.SavePlayerStats(this);
    }
    public void UpgradeMoveSpeed(int amount)
    {
        moveSpeed += amount;
        GameManager.Instance.SavePlayerStats(this);
    }
}
