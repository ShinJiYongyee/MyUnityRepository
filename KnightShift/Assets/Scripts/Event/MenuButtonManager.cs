using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuButtonManager : MonoBehaviour
{
    [Header("Main Buttons")]
    public Button startButton;
    public Button exitToMenuButton;
    public Button optionButton;

    [Header("Option Panel")]
    public GameObject optionPanel;
    public Slider bgmSlider;
    public Slider sfxSlider;
    public TextMeshProUGUI bgmValue;
    public TextMeshProUGUI sfxValue;
    public Button optionPanelCloseButton;

    void Start()
    {
        // SceneTransitionManager�� �� ���� �� ���� �����Ǹ� DontDestroyOnLoad �������� �����ǹǷ�,
        // ��ư�� �����ϴ� �� �� ������Ʈ�� ��Ÿ�� �� ��ü�Ǹ� ���� ���
        // ���� Start() �ܰ迡�� ����ִ� SceneTransitionManager�� �ڵ�� ������ �����ؾ� �Ѵ�
        startButton.onClick.AddListener(() => SceneTransitionManager.instance.StartSceneTransition("Stage1"));
        exitToMenuButton.onClick.AddListener(() => ExitGame());
        optionButton.onClick.AddListener(SwitchOptionPanelActivation);
        optionPanelCloseButton.onClick.AddListener(SwitchOptionPanelActivation);

        // �ʱ�ȭ: �ɼ�â ����
        if (optionPanel != null)
        {
            optionPanel.SetActive(false);
        }

        // �����̴� �ʱⰪ ����
        if (bgmSlider != null)
        {
            bgmSlider.value = SoundManager.Instance.bgmSource.volume;
            bgmSlider.onValueChanged.AddListener(UpdateBGMVolume);
        }

        if (sfxSlider != null)
        {
            sfxSlider.value = SoundManager.Instance.sfxSource.volume;
            sfxSlider.onValueChanged.AddListener(UpdateSFXVolume);
        }

        // �ؽ�Ʈ �ʱ�ȭ
        UpdateVolumeTexts();
    }

    void Update()
    {
        // �ؽ�Ʈ �ǽð� �ݿ� (�����̴� �̺�Ʈ�ε� ���������, ������ ����)
        UpdateVolumeTexts();
    }

    void SwitchOptionPanelActivation()
    {
        if (optionPanel != null)
        {
            optionPanel.SetActive(!optionPanel.activeSelf);
        }
    }

    void UpdateBGMVolume(float value)
    {
        SoundManager.Instance.bgmSource.volume = value;
        UpdateVolumeTexts();
    }

    void UpdateSFXVolume(float value)
    {
        SoundManager.Instance.sfxSource.volume = value;
        UpdateVolumeTexts();
    }

    void UpdateVolumeTexts()
    {
        if (bgmValue != null)
        {
            bgmValue.text = Mathf.RoundToInt(bgmSlider.value * 100).ToString();
        }

        if (sfxValue != null)
        {
            sfxValue.text = Mathf.RoundToInt(sfxSlider.value * 100).ToString();
        }
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // ���ø����̼� ����
#endif        
    }
}
