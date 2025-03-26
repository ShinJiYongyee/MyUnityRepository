using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{
    //로딩 슬라이더 UI를 가져오는 변수
    public Slider loadingSlider;
    string nextSceneName;
    //로딩 코루틴을 시작하는 함수
    public void StartLoading(string sceneName)
    {
        nextSceneName = sceneName;
        StartCoroutine(LoadLoadingSceneAndNextScene());
    }

    //LoadingScene을 로드하고 NextScene이 로드될 때까지 대기하는 코루틴
    IEnumerator LoadLoadingSceneAndNextScene()
    {
        //LoadingScene을 비동기적으로 로드(로딩 상태표시용으로 사용하는 Scene)
        //Additive: 현재 Scene에 Scene을 추가하는 방식(기존 Scene 유지)
        AsyncOperation loadingSceneOp = SceneManager.LoadSceneAsync("LoadingScene", LoadSceneMode.Additive);
        
        //자동으로 Scene을 전환하지 않도록 설정
        loadingSceneOp.allowSceneActivation = false;

        while (!loadingSceneOp.isDone) //LoadingScene이 로드될 때까지 대기
        {
            if (loadingSceneOp.progress >= 0.9f) //LoadingScene이 거의 다 로드될 때까지 대기
            {
                loadingSceneOp.allowSceneActivation = true; //LoadingScene준비 완료되면 Scene활성화
            }
            yield return null;

        }
        //LoadingScene에서 로딩 Slider를 찾아오기
        FindLoadingSliderInScene(); 

        //NextScene을 비동기적으로 로드
        AsyncOperation nextSceneOp = SceneManager.LoadSceneAsync(nextSceneName);
        
        //다음 Scene로드가 완료될 때까지 대기하면서 로딩 진행률을 Slider에 표시
        while (!nextSceneOp.isDone)
        {
            //로딩진행도 업데이트
            loadingSlider.value = nextSceneOp.progress;
            yield return null;
        }
        //NextScene을 완전히 로드한 후 LoadingScene 언로드
        SceneManager.UnloadSceneAsync("LoadingScene");

    }

    void FindLoadingSliderInScene()
    {
        loadingSlider = GameObject.Find("LoadingSlider").GetComponent<Slider>();
    }
}
