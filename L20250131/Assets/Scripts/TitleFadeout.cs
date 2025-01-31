using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TitleFadeout : MonoBehaviour
{
    //���� ��ư���� ���̵� ��/�ƿ� ����
    public Text title;
    public float fadeSpeed = 1.0f; // �ӵ��� �� �ڿ������� ����
    private bool isFaded = false;  // ���� ���� (true: ����, false: ����)
    private bool isFading = false; // �ߺ� ���� ����

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
        isFading = true; // �ߺ� ���� ����

        float startAlpha = title.color.a;
        float targetAlpha = isFaded ? 0.0f : 1.0f; // ���� ���� �ݴ�� ����
        float elapsedTime = 0f;

        while (elapsedTime < fadeSpeed)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeSpeed);
            title.color = new Color(title.color.r, title.color.g, title.color.b, newAlpha);
            yield return null;
        }

        title.color = new Color(title.color.r, title.color.g, title.color.b, targetAlpha);
        isFaded = !isFaded; // ���� ����
        isFading = false; // �ִϸ��̼� ���� �� �ٽ� ���� ����
    }
}
