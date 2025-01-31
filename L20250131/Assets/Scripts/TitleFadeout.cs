using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TitleFadeout : MonoBehaviour
{
    //단일 버튼으로 페이드 인/아웃 설정
    public Text title;
    public float fadeSpeed = 1.0f; // 속도를 더 자연스럽게 조정
    private bool isFaded = false;  // 현재 상태 (true: 보임, false: 숨김)
    private bool isFading = false; // 중복 실행 방지

    void Awake()
    {
        if (title == null)
            title = GetComponent<Text>();
    }

    public void FadeInOutButton()
    {
        if (!isFading)
            StartCoroutine(FadeInOut());
    }

    private IEnumerator FadeInOut()
    {
        isFading = true; // 중복 실행 방지

        float startAlpha = title.color.a;
        float targetAlpha = isFaded ? 0.0f : 1.0f; // 현재 상태 반대로 변경
        float elapsedTime = 0f;

        while (elapsedTime < fadeSpeed)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeSpeed);
            title.color = new Color(title.color.r, title.color.g, title.color.b, newAlpha);
            yield return null;
        }

        title.color = new Color(title.color.r, title.color.g, title.color.b, targetAlpha);
        isFaded = !isFaded; // 상태 변경
        isFading = false; // 애니메이션 종료 후 다시 실행 가능
    }
}
