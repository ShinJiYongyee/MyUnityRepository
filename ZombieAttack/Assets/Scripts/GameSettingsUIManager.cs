using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameSettingsUIManager : MonoBehaviour
{
    public GameObject SettingsObj;

    public TextMeshProUGUI resolutionText;
    public TextMeshProUGUI volumeText;
    public TextMeshProUGUI screenSetupText;
    public TextMeshProUGUI graphicsQualityText;

    private int resolutionIndex = 0;
    private int graphicsQualityIndex = 0;

    private string[] resolutions = { "1280x720", "1920x1080", "2560x1440", "3840x2160" };
    private string[] graphicsQualityOptions = { "Low", "Medium", "High" };

    private bool isFullScreen = true;

    // 오디오 관련
    public AudioSource audioSource;
    public AudioClip buttonClickSound;

    private float volumeLevel = 1.0f; // 초기 볼륨 (최대)
    private float volumeStep = 0.1f; // 볼륨 조절 단위

    void Start()
    {
        SettingsObj.SetActive(false);
        LoadSettings();
        UpdateResolutionText();
        UpdateGraphicsQualityText();
        UpdateFullScreenText();
        UpdateVolumeText();
    }

    // 버튼 클릭 효과음 재생
    private void PlayButtonClickSound()
    {
        if (audioSource != null && buttonClickSound != null)
        {
            audioSource.PlayOneShot(buttonClickSound);
        }
    }

    public void DecreaseResolution()
    {
        PlayButtonClickSound();
        resolutionIndex = Mathf.Max(0, resolutionIndex - 1);
        UpdateResolutionText();
    }

    public void IncreaseResolution()
    {
        PlayButtonClickSound();
        resolutionIndex = Mathf.Min(resolutions.Length - 1, resolutionIndex + 1);
        UpdateResolutionText();
    }

    public void DecreaseGraphics()
    {
        PlayButtonClickSound();
        graphicsQualityIndex = Mathf.Max(0, graphicsQualityIndex - 1);
        UpdateGraphicsQualityText();
    }

    public void IncreaseGraphics()
    {
        PlayButtonClickSound();
        graphicsQualityIndex = Mathf.Min(graphicsQualityOptions.Length - 1, graphicsQualityIndex + 1);
        UpdateGraphicsQualityText();
    }

    public void OnFullScreenToggleClick()
    {
        PlayButtonClickSound();
        isFullScreen = !isFullScreen;
        UpdateFullScreenText();
    }

    public void OnApplySettingsClick()
    {
        PlayButtonClickSound();
        ApplySettings();
        SaveSettings();
    }

    private void ApplySettings()
    {
        string[] res = resolutions[resolutionIndex].Split('x');
        int width = int.Parse(res[0]);
        int height = int.Parse(res[1]);
        Screen.SetResolution(width, height, isFullScreen);
        QualitySettings.SetQualityLevel(graphicsQualityIndex);
        AudioListener.volume = volumeLevel;
    }

    private void SaveSettings()
    {
        PlayerPrefs.SetInt("ResolutionIndex", resolutionIndex);
        PlayerPrefs.SetInt("GraphicsQualityIndex", graphicsQualityIndex);
        PlayerPrefs.SetInt("FullScreen", isFullScreen ? 1 : 0);
        PlayerPrefs.SetFloat("VolumeLevel", volumeLevel);
        PlayerPrefs.Save();
    }

    private void LoadSettings()
    {
        resolutionIndex = PlayerPrefs.GetInt("ResolutionIndex", 1);
        graphicsQualityIndex = PlayerPrefs.GetInt("GraphicsQualityIndex", 1);
        isFullScreen = PlayerPrefs.GetInt("FullScreen", 1) == 1;
        volumeLevel = PlayerPrefs.GetFloat("VolumeLevel", 1.0f);
        AudioListener.volume = volumeLevel;
    }

    private void UpdateResolutionText()
    {
        resolutionText.text = resolutions[resolutionIndex];
    }

    private void UpdateGraphicsQualityText()
    {
        graphicsQualityText.text = graphicsQualityOptions[graphicsQualityIndex];
    }

    private void UpdateFullScreenText()
    {
        screenSetupText.text = isFullScreen ? "On" : "Off";
    }

    private void UpdateVolumeText()
    {
        volumeText.text = $"Volume: {(int)(volumeLevel * 100)}%";
    }

    // 마스터 볼륨 조절 기능 추가
    public void IncreaseVolume()
    {
        PlayButtonClickSound();
        volumeLevel = Mathf.Min(1.0f, volumeLevel + volumeStep);
        AudioListener.volume = volumeLevel;
        UpdateVolumeText();
    }

    public void DecreaseVolume()
    {
        PlayButtonClickSound();
        volumeLevel = Mathf.Max(0.0f, volumeLevel - volumeStep);
        AudioListener.volume = volumeLevel;
        UpdateVolumeText();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SettingsObj.SetActive(false);
        }
    }

    public void OnSettings()
    {
        PlayButtonClickSound();
        SettingsObj.SetActive(true);
    }
}
