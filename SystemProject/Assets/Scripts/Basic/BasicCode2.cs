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
    /// UI�� ������Ʈ Text�� ������Ʈ text�� arg0�� ���
    /// </summary>
    private void inputText(string arg0) 
    {
        text.text = arg0;
    }
}
