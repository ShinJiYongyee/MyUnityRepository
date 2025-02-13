using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Conversation : MonoBehaviour
{
    public Text message; //타이핑할(유니티 화면에서 출력할) 빈 텍스트
    [SerializeField] private float delay = 0.1f;    //읽는 속도
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
            Debug.LogWarning("대화 큐가 비어 있습니다.");
            yield break;
        }
        string currentMessage = dialogQueue.Dequeue(); // 먼저 문장을 가져오기
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
        Debug.Log(dialogData1 + "를 등록했습니다");
    }
    public void SetDialog2()
    {
        dialogQueue.Clear();
        foreach (var item in dialogData2.list)
        {
            dialogQueue.Enqueue(item);
        }
        Debug.Log(dialogData2 + "를 등록했습니다");
    }
}
