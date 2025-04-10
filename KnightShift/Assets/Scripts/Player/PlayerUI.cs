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
    private bool isPaused = false;

    public Slider healthBar;

    // onclick()��SceneTransitionManager�� �������� �Ҵ��ϱ� ���� �ʵ�
    public Button restartButton;
    public Button exitToMenuButton;

    private void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
        healthBar.maxValue = playerHealth.health;
        healthBar.value = playerHealth.health;
        healthBar.minValue = 0;

        // SceneTransitionManager�� �� ���� �� ���� �����Ǹ� DontDestroyOnLoad �������� �����ǹǷ�,
        // ��ư�� �����ϴ� �� �� ������Ʈ�� ��Ÿ�� �� ��ü�Ǹ� ���� ���
        // ���� Start() �ܰ迡�� ����ִ� SceneTransitionManager�� �ڵ�� ������ �����ؾ� �Ѵ�
        restartButton.onClick.AddListener(() => SceneTransitionManager.instance.StartSceneTransition("Stage1"));
        exitToMenuButton.onClick.AddListener(() => SceneTransitionManager.instance.StartSceneTransition("Menu"));
    }
    public void CheckStatus()
    {
        ChechHealth();
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

    private void ChechHealth()
    {
        if (playerHealth != null)
        {
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
