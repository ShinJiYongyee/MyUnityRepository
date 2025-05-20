using UnityEngine;
using UnityEngine.UI;

public class UIEventCallbacks : MonoBehaviour
{
    [SerializeField] private Toggle toggle;
    //private void Awake()
    //{
    //    toggle.onValueChanged.AddListener(OnToggled);
    //}
    public void OnClickButton()
    {
        Debug.Log("button Clicked");
    }
    public void OnToggled(bool isOn)  // toggle�� ���� ����(üũ�� ��, üũ ������ ����)
    {
        Debug.Log($"Toggle : {isOn}");
    }
}
