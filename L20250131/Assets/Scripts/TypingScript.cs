using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class TypingScript : MonoBehaviour
{
    public Text message; //타이핑할(유니티 화면에서 출력할) 빈 텍스트
    [SerializeField] [TextArea] private string content; //message로부터 채워넣어 출력할 내용
    [SerializeField] private float delay = 0.2f;    //읽는 속도
    void Start()
    {
        
    }
    public void OnMessageBottonClick()
    {
        StartCoroutine("Typing");
    }
    /// <summary>
    /// 2배속 기능
    /// </summary>
    public void ByTwo()
    {
        if (delay == 0.2f)
            delay = 0.1f;
        else 
            delay = 0.2f;
    }

    IEnumerator Typing()
    {
        message.text = ""; //현재 화면의 메세지를 지우겠습니다.

        int typing_count = 0;   //타이핑 카운트를 0으로 설정

        //현재 카운트가 컨텐츠의 길이와 다르다면 
        while (typing_count != content.Length)
        {
            if (typing_count < content.Length) 
            {
                message.text += content[typing_count].ToString();
                //현재 카운트에 해당하는 단어 하나를 메세지 텍스트 UI에 전달
                typing_count++;
                //카운트를 1 증가
            }
            yield return new WaitForSeconds(delay);
            //현재 딜레이만큼 대기
        }

    }

}
