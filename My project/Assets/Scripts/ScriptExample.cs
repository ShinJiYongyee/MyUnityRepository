using UnityEngine;

/// <summary>
/// Unity Attribute
/// 유니티에서는 에디터에 맞게 스크립트를 커스텀할 수 있음
/// </summary>
/// 

[AddComponentMenu("CustomUtility/ScriptExample")]
public class ScriptExample : MonoBehaviour
{
    [Range(1, 99)]
    public int level;

    [Range(0,100)]
    public int volume;

    [Header("플레이어의 이름")]
    public string player_name;
    public string player_description;

    [TextArea]
    public string talk01;

    [TextArea(1,3)]
    public string talk02;

    [TextArea(5,7)]
    public string talk03;

    //bool=조건문을 설정하는 변수
    //에어점프 조건 등에 작용
    [Tooltip("체크되면 죽은 상태임을 의미합니다.")]
    public bool isDead = true;

    //함수(Function)
    //클래스 내부 함수=메소드(method)
    //함수는 특정한 단일 기능을 수행하기 위해 작성된 명령문 집합체
    //코드 내에서 설계된 함수는 원하는 작업 위치에서 호출해 사용
    //자료형 함수명(매개변수){함수 호출시 실행할 명령문}
    //함수명(인자);
    //매개변수: 함수 호출 시 전달 받을 데이터 
    //인자: 함수를 호출할 때 전달할 값

    [ContextMenu("HelloEveryone")]
    void HelloEveryone()
    {
        Debug.Log("여러분들 모두 안녕하세요!!");
    }

    void HelloSomeone(string who) 
    { 
        Debug.Log($"{who}님 안녕하세요!");
    }

}
