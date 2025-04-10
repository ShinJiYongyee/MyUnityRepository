using UnityEngine;
using UnityEngine.UI;

public class MenuButtonManager : MonoBehaviour
{
    // onclick()��SceneTransitionManager�� �������� �Ҵ��ϱ� ���� �ʵ�
    public Button startButton;
    public Button exitToMenuButton;
    void Start()
    {
        // SceneTransitionManager�� �� ���� �� ���� �����Ǹ� DontDestroyOnLoad �������� �����ǹǷ�,
        // ��ư�� �����ϴ� �� �� ������Ʈ�� ��Ÿ�� �� ��ü�Ǹ� ���� ���
        // ���� Start() �ܰ迡�� ����ִ� SceneTransitionManager�� �ڵ�� ������ �����ؾ� �Ѵ�
        startButton.onClick.AddListener(() => SceneTransitionManager.instance.StartSceneTransition("Stage1"));
        exitToMenuButton.onClick.AddListener(() => ExitGame());

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // ���ø����̼� ����
#endif        
    }
}
