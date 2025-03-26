using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{
    //�ε� �����̴� UI�� �������� ����
    public Slider loadingSlider;
    string nextSceneName;
    //�ε� �ڷ�ƾ�� �����ϴ� �Լ�
    public void StartLoading(string sceneName)
    {
        nextSceneName = sceneName;
        StartCoroutine(LoadLoadingSceneAndNextScene());
    }

    //LoadingScene�� �ε��ϰ� NextScene�� �ε�� ������ ����ϴ� �ڷ�ƾ
    IEnumerator LoadLoadingSceneAndNextScene()
    {
        //LoadingScene�� �񵿱������� �ε�(�ε� ����ǥ�ÿ����� ����ϴ� Scene)
        //Additive: ���� Scene�� Scene�� �߰��ϴ� ���(���� Scene ����)
        AsyncOperation loadingSceneOp = SceneManager.LoadSceneAsync("LoadingScene", LoadSceneMode.Additive);
        
        //�ڵ����� Scene�� ��ȯ���� �ʵ��� ����
        loadingSceneOp.allowSceneActivation = false;

        while (!loadingSceneOp.isDone) //LoadingScene�� �ε�� ������ ���
        {
            if (loadingSceneOp.progress >= 0.9f) //LoadingScene�� ���� �� �ε�� ������ ���
            {
                loadingSceneOp.allowSceneActivation = true; //LoadingScene�غ� �Ϸ�Ǹ� SceneȰ��ȭ
            }
            yield return null;

        }
        //LoadingScene���� �ε� Slider�� ã�ƿ���
        FindLoadingSliderInScene(); 

        //NextScene�� �񵿱������� �ε�
        AsyncOperation nextSceneOp = SceneManager.LoadSceneAsync(nextSceneName);
        
        //���� Scene�ε尡 �Ϸ�� ������ ����ϸ鼭 �ε� ������� Slider�� ǥ��
        while (!nextSceneOp.isDone)
        {
            //�ε����൵ ������Ʈ
            loadingSlider.value = nextSceneOp.progress;
            yield return null;
        }
        //NextScene�� ������ �ε��� �� LoadingScene ��ε�
        SceneManager.UnloadSceneAsync("LoadingScene");

    }

    void FindLoadingSliderInScene()
    {
        loadingSlider = GameObject.Find("LoadingSlider").GetComponent<Slider>();
    }
}
