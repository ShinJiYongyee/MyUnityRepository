using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TypingScript : MonoBehaviour
{
    public Text message; //Ÿ������(����Ƽ ȭ�鿡�� �����) �� �ؽ�Ʈ
    [SerializeField] private float delay = 0.1f;    //�д� �ӵ�
    public Queue<string> dialogQueue = new Queue<string>();
    public Dialog dialogData;

    void Start()
    {
        foreach (var item in dialogData.list)
        {
            dialogQueue.Enqueue(item);
        }
    }
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


}
