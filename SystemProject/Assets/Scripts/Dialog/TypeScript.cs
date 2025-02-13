using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TypingScript : MonoBehaviour
{
    public Text message; //타이핑할(유니티 화면에서 출력할) 빈 텍스트
    [SerializeField] private float delay = 0.1f;    //읽는 속도
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


}
