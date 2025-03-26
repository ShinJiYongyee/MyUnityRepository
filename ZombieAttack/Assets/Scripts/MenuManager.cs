using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject menu;

    public Image panel;
    public float fadeDuration = 1.0f;
    public string nextSceneName;
    private bool isFading;

    // Start ��ư Ŭ�� �� Stage1 ������ �̵�
    public void StartGame()
    {
        SceneManager.LoadScene("Stage1");
        //SoundManager.Instance.SetSFXVolume(0.5f);
        //SoundManager.Instance.PlaySFX("EquipGun");
    }

    // Exit ��ư Ŭ�� �� ���� ���� (������ & ���� �� �� ����)
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // ������ ��忡���� �÷��� ����
#else
            Application.Quit(); // ����� ���ӿ����� ���� ����
#endif
    }
    public void ResumeGame()
    {
        PlayerManager.Instance.isPaused = false;
        menu.SetActive(false);
    }

    IEnumerator FadeInAndLoadScene()
    {
        isFading = true;

        yield return StartCoroutine(FadeImage(0, 1, fadeDuration));


        yield return StartCoroutine(FadeImage(1, 0, fadeDuration));

        isFading = false;
    }

    IEnumerator FadeImage(float startAlpha, float endAlpha, float duration)
    {
        float elapsedTime = 0.0f;

        Color panelColor = panel.color;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
            panelColor.a = newAlpha;
            panel.color = panelColor;
            yield return null;
        }

        panelColor.a = endAlpha;
        panel.color = panelColor;

        if (isFading)
        {
            SceneManager.LoadScene(nextSceneName);

        }
    }
}
