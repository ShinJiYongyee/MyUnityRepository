using UnityEngine;


/// <summary>
/// 처음으로 만들어본 유니티의 스크립트
/// </summary>
public class Test : MonoBehaviour
//MonoBehaviour는 클래스에 연결했을 경우
//유니티 씬에 존재하는 오브젝트에 스크립트를 연결할 수 있게 해줌
//추가적으로 유니티에서 제공해주는 기능을 사용할 때 사용
{
    /// <summary>
    /// 공격하는 기능
    /// </summary>
    /// <param name="damage">공격력</param>
    void attack(int damage)
    {

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Hello, World!");    //콘솔창에 메세지 작성
    }

    int count = 0;

    // Update is called once per frame
    void Update()
    {
        Debug.Log($"{count} 좌우 반복 뛰기");
        count++;
    }
}
