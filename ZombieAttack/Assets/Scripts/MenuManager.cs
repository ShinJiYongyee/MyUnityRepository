using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject menu;
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
}
