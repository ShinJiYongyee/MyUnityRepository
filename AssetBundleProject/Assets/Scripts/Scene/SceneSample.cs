using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSample : MonoBehaviour
{
    //Ȱ��ȭ ������ ���
    private void OnEnable()
    {
        Debug.Log("OnSceneLoaded�� ��ϵǾ����ϴ�");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    //��Ȱ��ȭ ������ ���
    private void OnDisable()
    {
        Debug.Log("OnSceneLoaded�� �����Ǿ����ϴ�");
        SceneManager.sceneLoaded -= OnSceneLoaded;  
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"���� {scene.name} �ε��");
    }

    //Ű�� ������ �� ��ȯ
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            SceneManager.LoadScene("BRP Sample Scene");
            //���� �� ��带 �������� ������ LoadSceneMode�� Single�� ó����
            //Single -> ���� ����Ÿ��
            //Additive -> ���� �� ���� �� ���� �ߺ��ؼ� �ε�
            //Main Camera, Direction Light � ���� �ε��ϹǷ�, �� ���� ����
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            SceneManager.LoadScene("BRP Sample Scene",LoadSceneMode.Additive);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            StartCoroutine("LoadSceneCoroutine");
            //�񵿱���(async)�ε�
            //���� �ε��� �ٵ� ������ �ٸ� ��ҵ��� �۵����� ����
            //�������� �۾� �ʿ�
            //�۾��� �������� �� �ִ� �ڷ�ƾ �ʿ�
        }
    }

    IEnumerator LoadSceneCoroutine()
    {
        yield return SceneManager.LoadSceneAsync("BRP Sample Scene",LoadSceneMode.Additive); 

    }
}
