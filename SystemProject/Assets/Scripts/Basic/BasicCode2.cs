using System;
using UnityEngine;
using UnityEngine.UI;

public class BasicCode2 : MonoBehaviour
{
    public InputField inputField;
    public Text text;

    private void Start()
    {
        inputField.onValueChanged.AddListener(inputText);
    }

    /// <summary>
    /// UI의 오브젝트 Text의 컴포넌트 text에 arg0를 등록
    /// </summary>
    private void inputText(string arg0) 
    {
        text.text = arg0;
    }
}
