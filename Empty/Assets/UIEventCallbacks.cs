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
    public void OnToggled(bool isOn)  // toggle의 현재 상태(체크시 참, 체크 해제시 거짓)
    {
        Debug.Log($"Toggle : {isOn}");
    }
}
