using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject menu;
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
}
