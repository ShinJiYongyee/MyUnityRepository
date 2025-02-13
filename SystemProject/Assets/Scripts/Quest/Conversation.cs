using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Conversation : MonoBehaviour
{
    public Text message; //Ÿ������(����Ƽ ȭ�鿡�� �����) �� �ؽ�Ʈ
    [SerializeField] private float delay = 0.1f;    //�д� �ӵ�
    public Queue<string> dialogQueue = new Queue<string>();
    public Dialog dialogData1;
    public Dialog dialogData2;

    public void OnMessageBottonClick()
    {
        StartCoroutine("Typing");

    }

    IEnumerator Typing()
    {
        if (dialogQueue.Count == 0)
        {
            Debug.LogWarning("��ȭ ť�� ��� �ֽ��ϴ�.");
            yield break;
        }
        string currentMessage = dialogQueue.Dequeue(); // ���� ������ ��������
        message.text = "";

        foreach (char letter in currentMessage)
        {
            message.text += letter;
            yield return new WaitForSeconds(delay);
        }
    }

    public void SetDialog1()
    {
        dialogQueue.Clear();
        foreach (var item in dialogData1.list)
        {
            dialogQueue.Enqueue(item);
        }
        Debug.Log(dialogData1 + "�� ����߽��ϴ�");
    }
    public void SetDialog2()
    {
        dialogQueue.Clear();
        foreach (var item in dialogData2.list)
        {
            dialogQueue.Enqueue(item);
        }
        Debug.Log(dialogData2 + "�� ����߽��ϴ�");
    }
}
