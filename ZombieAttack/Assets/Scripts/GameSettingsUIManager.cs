using System;
using TMPro;
using Unity.VisualScripting;
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
    //private int volumeIndex = 0;
    //private int screenSetupIndex = 0;
    private int graphicsQualityIndex = 0;

    private string[] resolutions = { "1280x720", "1920x1080", "2560x1440", "3840x2160" };
    private string[] graphicsQualityOptions = { "Low", "Medium", "High" };

    bool isFullScreen = true;
    void Start()
    {
        SettingsObj.SetActive(false);
    }

    public void DecreaseResolution()
    {
        resolutionIndex = Mathf.Max(0, resolutionIndex - 1);
        UpdateResolutionText();

    }

    public void IncreaseResolution()
    {
        resolutionIndex = Mathf.Min(resolutions.Length - 1, resolutionIndex + 1);
        UpdateResolutionText();

    }

    public void DecreaseGraphics()
    {
        resolutionIndex = Mathf.Max(0, graphicsQualityIndex - 1);
        UpdateGraphicsQulityText();
    }

    public void IncreaseGraphics()
    {
        resolutionIndex = Mathf.Min(graphicsQualityOptions.Length - 1, graphicsQualityIndex + 1);
        UpdateGraphicsQulityText();
    }
    public void OnFullScreenToggleClick()
    {
        isFullScreen = !isFullScreen;
        UpdateFullScreenText();
    }
    public void UpdateScreenSetupText()
    {
        screenSetupText.text = isFullScreen ? "On" : "Off";
        UpdateFullScreenText();
    }
    public void OnApplySettingsClick()
    {
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
    }

    private void SaveSettings()
    {
        PlayerPrefs.SetInt("ResolutionIndex", resolutionIndex);
        PlayerPrefs.SetInt("GraphicsQualityIndex", graphicsQualityIndex);
        PlayerPrefs.SetInt("FullScreen", isFullScreen ? 1 : 0);
        PlayerPrefs.Save();
    }

    private void LoadSettings()
    {
        resolutionIndex = PlayerPrefs.GetInt("ResolutionIndex", 1);
        graphicsQualityIndex = PlayerPrefs.GetInt("GraphicsQualityIndex", 1);
    }
    private void UpdateResolutionText()
    {
        resolutionText.text = resolutions[resolutionIndex];
    }

    private void UpdateGraphicsQulityText()
    {
        graphicsQualityText.text = graphicsQualityOptions[graphicsQualityIndex];
    }

    private void UpdateFullScreenText()
    {
        screenSetupText.text = isFullScreen ? "On" : "Off";
    }


    void Update()
    {
        OffSettings();
    }

    public void OnSettings()
    {
        SettingsObj.SetActive(true);
    }

    void OffSettings()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SettingsObj.SetActive(false);
        }
    }
}
