using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    private PlayerHealth playerHealth;

    public TextMeshProUGUI healthCountUI;
    public TextMeshProUGUI shieldCountUI;

    public GameObject PauseMenu;
    public bool isPaused = false;
    public TextMeshProUGUI pauseText;
    public GameObject resumeButton;

    public Slider healthBar;

    // onclick()��SceneTransitionManager�� �������� �Ҵ��ϱ� ���� �ʵ�
    public Button restartButton;
    public Button exitToMenuButton;

    public string restartSceneName;

    private void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
        healthBar.maxValue = playerHealth.health;
        healthBar.value = playerHealth.health;
        healthBar.minValue = 0;
        isPaused = false;
        // SceneTransitionManager�� �� ���� �� ���� �����Ǹ� DontDestroyOnLoad �������� �����ǹǷ�,
        // ��ư�� �����ϴ� �� �� ������Ʈ�� ��Ÿ�� �� ��ü�Ǹ� ���� ���
        // ���� Start() �ܰ迡�� ����ִ� SceneTransitionManager�� �ڵ�� ������ �����ؾ� �Ѵ�
        restartButton.onClick.AddListener(() => SceneTransitionManager.instance.StartSceneTransition(restartSceneName));
        exitToMenuButton.onClick.AddListener(() => SceneTransitionManager.instance.StartSceneTransition("Menu"));
    }
    public void CheckStatus()
    {
        CheckHealth();
        ChechShieldCount();
        SwitchPause();
    }

    private void SwitchPause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SwitchIsPaused();
        }

        PauseMenu.SetActive(isPaused);

        if (isPaused)
        {
            Time.timeScale = 0.0f;
        }
        else
        {
            Time.timeScale = 1.0f;
        }
    }

    public void SwitchIsPaused()
    {
        isPaused = !isPaused;
    }

    // ������ unpause�� �� �ֵ��� �޼��� �߰�
    public void ForceUnpause()
    {
        isPaused = false;
        Time.timeScale = 1f;
        PauseMenu.SetActive(false);
    }


    private void CheckHealth()
    {
        if (playerHealth != null)
        {
            if (!playerHealth.isAlive)
            {
                pauseText.text = "You Died";
                resumeButton.SetActive(false);
            }
            healthCountUI.text = $"{playerHealth.health.ToString()}/100";
            healthBar.value = playerHealth.health;
        }
        else
        {
            Debug.Log("no Playerhealth script");
        }
    }
    private void ChechShieldCount()
    {
        if (playerHealth != null)
        {
            shieldCountUI.text = playerHealth.shieldCount.ToString();
        }
        else
        {
            Debug.Log("no Playerhealth script");
        }
    }


}
