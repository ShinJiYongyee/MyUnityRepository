using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{


    // Start ��ư Ŭ�� �� Stage1 ������ �̵�
    public void StartGame()
    {
        SceneManager.LoadScene("Stage1");
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
}
