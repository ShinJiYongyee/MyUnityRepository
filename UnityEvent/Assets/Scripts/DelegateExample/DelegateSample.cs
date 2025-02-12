using UnityEngine;

public class DelegateSample : MonoBehaviour
{
    delegate void DelegateTest();
    void Start()
    {
        //델리게이트 사용
        //델리게이트명 변수명 = new 델리게이트명(함수명);
        DelegateTest dt = new DelegateTest(Attack);

        //함수처럼 호출
        dt();               //공격

        //내용 변경
        //변수명 = 함수명;
        dt = Guard;
        dt();               //방어
        //델리게이트 체인
        dt += Attack;
        dt += Attack;
        dt += Attack;
        dt += Attack;
        dt -= Guard;        //방어 제거
        dt();               //공격 4번
    }
    void Attack()
    {
        Debug.Log("공격!");
    }
    void Guard()
    {
        Debug.Log("방어");
    }

}
