using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("UI Elements")]
    public GameObject gameOverSprite;
    public GameObject gameClearSprite;
    public GameObject retryButton;
    public GameObject nextStageButton;
    public Text timerText;  // Ÿ�̸� �ؽ�Ʈ (UI�� ǥ��)
    public Text StageText;
    public Text ScoreText;

    [Header("Game Variables")]
    public int timeLeft = 60; // ���� ���� �ð�
    private float timePassed;

    [Header("Game Status")]
    public bool isGameOver = false;
    public bool isGameClear = false;

    [Header("Penalty")]
    public float penalty;
    public float maxPenalty=4;

    void Awake()
    {
        // �̱��� ���� ����
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(gameObject);
    }

    void Start()
    {
        // UI ��� �ʱ�ȭ
        gameOverSprite.SetActive(false);
        gameClearSprite.SetActive(false);
        retryButton.SetActive(false);
        nextStageButton.SetActive(false);
        StageText.text = $"Stage {ScoreManager.currentStage}";
        UpdateTimerUI();
        InvokeRepeating(nameof(DecreaseTime), 1f, 1f); // �� �ʸ��� Ÿ�̸� ����

        penalty = 0;
    }

    void Update()
    {
        CheckPlayerDied();
        ScoreText.text = "Kill Count = " + ScoreManager.currentScore;
        if (isGameOver) return; // ���� ���� �� Ÿ�̸� ������Ʈ ����

        timePassed += Time.deltaTime;
    }

    void DecreaseTime()
    {
        if (isGameOver || isGameClear) return;

        timeLeft--;
        UpdateTimerUI();

        if (timeLeft <= 0)
        {
            GameClear();
        }
    }

    void UpdateTimerUI()
    {
        timerText.text = "Time: " + Mathf.Max(timeLeft, 0).ToString();
    }

    public void CheckPlayerDied()
    {
        gameOverSprite.SetActive(isGameOver);
        retryButton.SetActive(isGameOver);
    }

    void GameClear()
    {
        isGameClear = true;
        gameClearSprite.SetActive(isGameClear);
        nextStageButton.SetActive(isGameClear);
    }
    public void Retry()
    {
        ScoreManager.currentStage = 1;
        ScoreManager.currentScore = 0;
        penalty = 0;
        SceneManager.LoadScene("Stage1");
    }
    public void NextStage()
    {
        ScoreManager.currentStage++;
        penalty = 0;
        SceneManager.LoadScene("Stage1");
    }
}
