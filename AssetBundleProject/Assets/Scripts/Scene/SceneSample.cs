using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSample : MonoBehaviour
{
    //활성화 상태일 경우
    private void OnEnable()
    {
        Debug.Log("OnSceneLoaded가 등록되었습니다");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    //비활성화 상태일 경우
    private void OnDisable()
    {
        Debug.Log("OnSceneLoaded가 해제되었습니다");
        SceneManager.sceneLoaded -= OnSceneLoaded;  
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"현재 {scene.name} 로드됨");
    }

    //키를 누르면 씬 전환
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            SceneManager.LoadScene("BRP Sample Scene");
            //따로 씬 모드를 설정하지 않으면 LoadSceneMode는 Single로 처리됨
            //Single -> 씬을 갈아타기
            //Additive -> 기존 씬 위에 새 씬을 중복해서 로드
            //Main Camera, Direction Light 등도 전부 로드하므로, 잘 쓰지 않음
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            SceneManager.LoadScene("BRP Sample Scene",LoadSceneMode.Additive);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            StartCoroutine("LoadSceneCoroutine");
            //비동기적(async)로드
            //씬에 로딩이 다될 때까지 다른 요소들은 작동하지 않음
            //지속적인 작업 필요
            //작업을 빠져나갈 수 있는 코루틴 필요
        }
    }

    IEnumerator LoadSceneCoroutine()
    {
        yield return SceneManager.LoadSceneAsync("BRP Sample Scene",LoadSceneMode.Additive); 

    }
}
