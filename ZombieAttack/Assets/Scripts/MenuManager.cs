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

    // Start 버튼 클릭 시 Stage1 씬으로 이동
    public void StartGame()
    {
        SceneManager.LoadScene("Stage1");
        //SoundManager.Instance.SetSFXVolume(0.5f);
        //SoundManager.Instance.PlaySFX("EquipGun");
    }

    // Exit 버튼 클릭 시 게임 종료 (에디터 & 빌드 둘 다 가능)
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // 에디터 모드에서는 플레이 중지
#else
            Application.Quit(); // 빌드된 게임에서는 게임 종료
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
