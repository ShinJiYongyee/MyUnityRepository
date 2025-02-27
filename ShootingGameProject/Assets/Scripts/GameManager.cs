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
    public Text timerText;  // 타이머 텍스트 (UI에 표시)
    public Text StageText;
    public Text ScoreText;

    [Header("Game Variables")]
    public int timeLeft = 60; // 게임 제한 시간
    private float timePassed;

    [Header("Game Status")]
    public bool isGameOver = false;
    public bool isGameClear = false;

    [Header("Penalty")]
    public float penalty;
    public float maxPenalty=4;

    void Awake()
    {
        // 싱글톤 패턴 적용
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(gameObject);
    }

    void Start()
    {
        // UI 요소 초기화
        gameOverSprite.SetActive(false);
        gameClearSprite.SetActive(false);
        retryButton.SetActive(false);
        nextStageButton.SetActive(false);
        StageText.text = $"Stage {ScoreManager.currentStage}";
        UpdateTimerUI();
        InvokeRepeating(nameof(DecreaseTime), 1f, 1f); // 매 초마다 타이머 감소

        penalty = 0;
    }

    void Update()
    {
        CheckPlayerDied();
        ScoreText.text = "Kill Count = " + ScoreManager.currentScore;
        if (isGameOver) return; // 게임 종료 후 타이머 업데이트 중지

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
