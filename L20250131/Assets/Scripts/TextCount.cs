using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TextCount : MonoBehaviour
{
    //텍스트에 카운트를 출력하는 기능 구현
    //카운트는 초마다 1씩 증가
    public Text countText;
    private int count;
    void Start()
    {
        StartCoroutine("CountPlus");
    }

    IEnumerator CountPlus()
    {
        while (true) 
        {
            count++;
            countText.text = count.ToString("N0");
            //텍스트를 적는 칸 이름이 text
            //ToString을 이용해 수를 문자열로 변형
            //N0는 숫자 3자리 간격으로 ,를 표시하는 format ex)1,000
            yield return null;
            //다음 동작으로 넘어가는 스크립트
        }
    }
}
