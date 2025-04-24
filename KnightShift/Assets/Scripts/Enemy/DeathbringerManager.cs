using UnityEngine;
using UnityEngine.UI;

public class DeathbringerManager : MonoBehaviour
{
    [Header("Deathbringer Settings")]
    public GameObject deathbringerPrefab;
    public Transform spawnPoint;

    [Header("UI")]
    public Slider summonSlider;
    public Image fillImage; // Fill �̹��� ����

    [Header("Summon Settings")]
    public float initialSummonTime = 100f;
    private float remainingTime;

    private bool hasSummoned = false;
    private GameObject spawnedDeathbringer;

    [Header("Colors")]
    public Color timerColor = Color.cyan; // Ÿ�̸ӿ� �Ķ���
    public Color healthColor = Color.red; // ü�¹ٿ� ������

    EnemyHealth deathbringerHealth;

    void Start()
    {
        remainingTime = initialSummonTime;
        UpdateSlider();
        SetFillColor(timerColor); // ���� �� Ÿ�̸� ����
    }

    void Update()
    {

        remainingTime -= Time.deltaTime;
        UpdateSlider();

        if (remainingTime <= 0f && !hasSummoned)
        {
            SummonDeathbringer();
        }
    }

    void UpdateSlider()
    {
        if (summonSlider != null && !hasSummoned)
        {
            summonSlider.value = remainingTime;
        }
        else
        {
            summonSlider.value = deathbringerHealth.enemyHealth;
        }
    }

    public void OnSlimeDied()
    {
        if (!hasSummoned)
        {
            remainingTime -= 1f;
        }
    }

    void SummonDeathbringer()
    {
        hasSummoned = true;
        spawnedDeathbringer = Instantiate(deathbringerPrefab, spawnPoint.position, Quaternion.identity);

        // �����̴� ������ ü�¹ٷ� ��ȯ
        deathbringerHealth = spawnedDeathbringer.GetComponent<EnemyHealth>();
        if (deathbringerHealth != null && summonSlider != null)
        {
            SetFillColor(healthColor); // ���� ��ȯ

            summonSlider.value = deathbringerHealth.enemyHealth;

        }
    }

    void SetFillColor(Color color)
    {
        if (fillImage != null)
        {
            fillImage.color = color;
        }
    }
}
